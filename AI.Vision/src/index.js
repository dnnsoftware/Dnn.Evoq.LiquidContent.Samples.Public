const contentItemsService = require('./contentItemsService');
const computerVisionService = require('./computerVisionService');

module.exports = function (context, data) {
    context.log('Webhook was triggered!');
    context.log(data);

    const contentTypeId = data.currentValue.contentTypeId;
    const contentItemId = data.currentValue.id;

    // Hardcoded identifier of our Image Content Type (this will be different in your project)
    const imageContentTypeId = '15797409-d0e0-4e99-b4bb-3440caca7f72';

    // Ignore content item creation than are not Image Content Type
    if(contentTypeId !== imageContentTypeId) {
        context.done(null, {});
        return;
    }

    // Create variable to modify and update the content item to update once retrieved
    let contentItemToUpdate = null;

    // Retrive Content Item
    contentItemsService.getContentItem(contentItemId)
        .then(contentItem => {
            context.log('content item', contentItem);
            contentItemToUpdate = contentItem;
            if(!contentItem.details.image || contentItem.details.image.length === 0) {
                context.log('no image to process');
                context.done(null, {});
                return;
            }
            const imageUrl = contentItem.details.image[0].url;

            // Get Image description from the Vision API
            return computerVisionService.describeImage(imageUrl);
        })
        .then(imageDescription => {    
            context.log('image description', imageDescription);     
            const tags = imageDescription.tags.map(t => t.name);          
            const description = imageDescription.description.captions.map(c => c.text).toString();

            // Update the Content Item with the new metadata
            contentItemToUpdate.tags = tags;
            contentItemToUpdate.description = description;
            contentItemToUpdate.seoSettings = Object.assign(contentItemToUpdate.seoSettings || {}, {
                description,
                keywords: tags
            });
            context.log('content item to update', contentItemToUpdate);

            // Store the update and publish
            return contentItemsService.updateContentItem(contentItemToUpdate);
        })
        .then(() => {
            context.log('auto tagging completed');
            context.done(null, {});
        })
        .catch(error => {
            // Catch any error and return a http 500 response
            context.log(error);
            context.done(null, { status: 500 });
        });
}
const request = require('request');
const apiKey = 'b9214fb8c937dc5e4ffde2d706aaa8ea';

function getContentItem(contentItemId) {
    const options = {
        method: 'GET',
        url: 'https://dnnapi.com/content/api/contentitems/' + contentItemId,
        headers: {
            'Authorization': 'Bearer ' + apiKey
        }
    };
    return new Promise((resolve, reject) => {
        request(options, function(error, response, body){
            if(error) {
                reject(error);
                return;
            }
            const jsonBody = JSON.parse(body);
            resolve(jsonBody);
        });
    });
}

function updateContentItem(contentItem) {
    const options = {
        method: 'PUT',
        url: 'https://dnnapi.com/content/api/contentitems/' + contentItem.id + '?publish=true',
        body: JSON.stringify(contentItem),
        headers: {
            'Content-type': 'application/json',
            'Authorization': 'Bearer ' + apiKey
        }
    };
    return new Promise((resolve, reject) => {
        request(options, function(error, response, body){
            if(error) {
                reject(error);
                return;
            }
            const jsonBody = JSON.parse(body);
            resolve(jsonBody);
        });
    });
}

const contentItemsService = {
    getContentItem,
    updateContentItem
};
module.exports = contentItemsService;
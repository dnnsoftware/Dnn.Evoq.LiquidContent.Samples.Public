using Newtonsoft.Json;
using StructuredContent.Models.ContentTypeFields.ContentDescription;
using StructuredContent.Models.ContentTypeFields.ContentName;
using StructuredContent.Models.ContentTypeFields.ContentTags;

namespace StructuredContent.Models
{
    [JsonObject]
    public class ContentTypeProperties
    {
        public ContentNameField ContentName { get; set; } = new ContentNameField();
        public ContentTagsField ContentTags { get; set; } = new ContentTagsField();
        public ContentDescriptionField ContentDescription { get; set; } = new ContentDescriptionField();
    }
}

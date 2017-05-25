using Newtonsoft.Json;

namespace StructuredContent.Models.ContentTypeFields.ContentTags
{
    [JsonObject]
    public class ContentTagsField : ContentTypeField
    {
        public override Type Type { get; set; } = Type.ContentTags;
        public ContentTagsValidation Validation { get; set; } = new ContentTagsValidation();
    }
}

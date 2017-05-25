using Newtonsoft.Json;

namespace StructuredContent.Models.ContentTypeFields.ContentDescription
{
    [JsonObject]
    public class ContentDescriptionField : ContentTypeField
    { 
        public override Type Type { get; set; } = Type.ContentDescription;
        public ContentDescriptionValidation Validation { get; set; } = new ContentDescriptionValidation();
    }
}

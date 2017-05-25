using Newtonsoft.Json;

namespace StructuredContent.Models.ContentTypeFields.ContentName
{
    [JsonObject]
    public class ContentNameField : ContentTypeField
    { 
        public override Type Type { get; set; } = Type.ContentName;
        public ContentNameSettings Settings { get; set; } = new ContentNameSettings();
    }
}

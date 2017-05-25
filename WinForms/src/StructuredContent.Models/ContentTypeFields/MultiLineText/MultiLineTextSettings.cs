using Newtonsoft.Json;

namespace StructuredContent.Models.ContentTypeFields.MultiLineText
{
    [JsonObject]
    public class MultiLineTextSettings
    {
        public MultiLineTextFormat SubType { get; set; }
    }
}

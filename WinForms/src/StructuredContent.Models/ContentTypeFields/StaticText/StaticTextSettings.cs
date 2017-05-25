using Newtonsoft.Json;

namespace StructuredContent.Models.ContentTypeFields.StaticText
{
    [JsonObject]
    public class StaticTextSettings
    {
        public StaticTextFormat SubType { get; set; }
        public StaticTextHeadingFormat HeadingType { get; set; }
        public string HeadingContent { get; set; }
        public string ParagraphContent { get; set; }
    }
}

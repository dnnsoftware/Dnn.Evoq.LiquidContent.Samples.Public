using Newtonsoft.Json;

namespace StructuredContent.Models.ContentTypeFields.SingleLineText
{
    [JsonObject]
    public class SingleLineTextSettings
    {
        public SingleLineTextFormat SubType { get; set; }
        public PhoneNumberFormat? PhoneNumberFormat { get; set; }
    }
}

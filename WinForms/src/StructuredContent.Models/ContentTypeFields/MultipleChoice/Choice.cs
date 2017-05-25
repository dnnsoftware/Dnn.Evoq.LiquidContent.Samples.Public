using Newtonsoft.Json;

namespace StructuredContent.Models.ContentTypeFields.MultipleChoice
{
    [JsonObject]
    public class Choice
    {
        public string Label { get; set; } = string.Empty;
    }
}

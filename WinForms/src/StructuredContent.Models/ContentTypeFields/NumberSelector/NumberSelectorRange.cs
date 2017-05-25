using Newtonsoft.Json;

namespace StructuredContent.Models.ContentTypeFields.NumberSelector
{
    [JsonObject]
    public class NumberSelectorRange
    {
        public double? Minimum { get; set; }
        public double? Maximum { get; set; }
    }
}
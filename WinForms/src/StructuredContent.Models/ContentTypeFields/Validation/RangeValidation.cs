using Newtonsoft.Json;

namespace StructuredContent.Models.ContentTypeFields.Validation
{
    [JsonObject]
    public class RangeValidation
    {
        public bool Active { get; set; }
        public RangeDefinition RangeDefinition { get; set; }
        public int? Minimum { get; set; }
        public string MinimumUnit { get; set; }
        public int? Maximum { get; set; }
        public string MaximumUnit { get; set; }
        public string ErrorMessage { get; set; }
    }
}

using Newtonsoft.Json;

namespace StructuredContent.Models.ContentTypeFields.Validation
{
    [JsonObject]
    public class DateTimeRangeValidation<T> where T : struct
    {
        public bool Active { get; set; }
        public DateTimeRangeDefinition RangeDefinition { get; set; }
        public T? StartDateTime { get; set; }
        public T? EndDateTime { get; set; }
        public string ErrorMessage { get; set; }

        public void Clean()
        {
            RangeDefinition = DateTimeRangeDefinition.Between;
            StartDateTime = null;
            EndDateTime = null;
            ErrorMessage = string.Empty;
        }
    }
}

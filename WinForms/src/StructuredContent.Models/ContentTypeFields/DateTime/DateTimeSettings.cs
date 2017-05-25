using Newtonsoft.Json;

namespace StructuredContent.Models.ContentTypeFields.DateTime
{
    [JsonObject]
    public class DateTimeSettings
    {
        public DateTimeFormat SubType { get; set; }
        public DateFormat DateFormat { get; set; }
        public TimeFormat TimeFormat { get; set; }
        public bool TimeZoneInfoActive { get; set; }
        public string TimeZoneInfoId { get; set; }
    }
}

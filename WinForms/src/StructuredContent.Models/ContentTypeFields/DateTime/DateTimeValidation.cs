using Newtonsoft.Json;
using StructuredContent.Models.ContentTypeFields.Validation;

namespace StructuredContent.Models.ContentTypeFields.DateTime
{
    [JsonObject]
    public class DateTimeValidation : BaseFieldValidation
    {
        public RequireFieldValidation RequireField { get; set; } = new RequireFieldValidation();
        public DateTimeRangeValidation<System.DateTime> DateRange { get; set; } = new DateTimeRangeValidation<System.DateTime>();
        public DateTimeRangeValidation<System.TimeSpan> TimeRange { get; set; } = new DateTimeRangeValidation<System.TimeSpan>();
    }
}

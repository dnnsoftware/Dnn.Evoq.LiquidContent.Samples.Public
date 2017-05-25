using Newtonsoft.Json;
using StructuredContent.Models.ContentTypeFields.Validation;

namespace StructuredContent.Models.ContentTypeFields.ContentDescription
{
    [JsonObject]
    public class ContentDescriptionValidation : BaseFieldValidation
    {
        public RequireFieldValidation RequireField { get; set; } = new RequireFieldValidation();
    }
}

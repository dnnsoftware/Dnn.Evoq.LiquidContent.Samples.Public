using Newtonsoft.Json;
using StructuredContent.Models.ContentTypeFields.Validation;

namespace StructuredContent.Models.ContentTypeFields.ReferenceObject
{
    [JsonObject]
    public class ReferenceObjectValidation: BaseFieldValidation
    {
        public RequireFieldValidation RequireField { get; set; } = new RequireFieldValidation();
        public RangeValidation NumberOfReferences { get; set; } = new RangeValidation();
    }
}

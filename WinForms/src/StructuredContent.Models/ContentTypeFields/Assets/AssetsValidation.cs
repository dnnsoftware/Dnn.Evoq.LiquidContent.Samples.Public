using Newtonsoft.Json;
using StructuredContent.Models.ContentTypeFields.Validation;

namespace StructuredContent.Models.ContentTypeFields.Assets
{
    [JsonObject]
    public class AssetsValidation : BaseFieldValidation
    {
        public RequireFieldValidation RequireField { get; set; } = new RequireFieldValidation();
    }
}

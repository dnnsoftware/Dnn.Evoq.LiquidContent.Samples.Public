using Newtonsoft.Json;
using StructuredContent.Models.ContentTypeFields.Validation;

namespace StructuredContent.Models.ContentTypeFields.NumberSelector
{
    [JsonObject]
    public class NumberSelectorValidation : BaseFieldValidation
    {
        public RequireFieldValidation RequireField { get; set; } = new RequireFieldValidation();
        public DropdownOrder DropdownOrder { get; set; } = DropdownOrder.Ascending;
        public RangeValidation NumberOfCharacters { get; set; } = new RangeValidation();
    }
}

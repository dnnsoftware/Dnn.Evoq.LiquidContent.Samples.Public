using Newtonsoft.Json;
using StructuredContent.Models.ContentTypeFields.Validation;

namespace StructuredContent.Models.ContentTypeFields.MultipleChoice
{
    [JsonObject]
    public class MultipleChoiceValidation : BaseFieldValidation
    {
        public RequireFieldValidation RequireField { get; set; } = new RequireFieldValidation();
        public RangeValidation NumberOfChoices { get; set; } = new RangeValidation();
        public ChoicesOrder ChoiceOrder { get; set; } = ChoicesOrder.None;
    }
}

using StructuredContent.Models.ContentTypeFields.Validation;

namespace StructuredContent.Models.ContentTypeFields.ContentTags
{
    public class ContentTagsValidation : BaseFieldValidation
    {
        public RequireFieldValidation RequireField { get; set; } = new RequireFieldValidation();
    }
}

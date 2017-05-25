using Newtonsoft.Json;

namespace StructuredContent.Models.ContentTypeFields.Validation
{
    [JsonObject]
    public class RequireFieldValidation
    {
        public bool Active { get; set; }
        public string ErrorMessage { get; set; }

        public void Clean()
        {
            ErrorMessage = string.Empty;
        }
    }
}

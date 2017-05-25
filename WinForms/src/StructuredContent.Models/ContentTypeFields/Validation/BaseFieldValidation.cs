using Newtonsoft.Json;

namespace StructuredContent.Models.ContentTypeFields.Validation
{
    [JsonObject]
    public class BaseFieldValidation
    {
        public string StandardErrorMessage { get; set; }

        public void Clean()
        {
            StandardErrorMessage = string.Empty;
        }
    }
}

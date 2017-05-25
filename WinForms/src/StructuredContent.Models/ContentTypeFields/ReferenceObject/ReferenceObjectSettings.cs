using Newtonsoft.Json;

namespace StructuredContent.Models.ContentTypeFields.ReferenceObject
{
    [JsonObject]
    public class ReferenceObjectSettings
    {
        public ReferenceObjectFormat SubType { get; set; }
    }
}

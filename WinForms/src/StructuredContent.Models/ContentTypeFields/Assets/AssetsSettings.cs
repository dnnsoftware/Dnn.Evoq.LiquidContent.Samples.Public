using Newtonsoft.Json;

namespace StructuredContent.Models.ContentTypeFields.Assets
{
    [JsonObject]
    public class AssetsSettings
    {
        public AssetsFormat SubType { get; set; }
        public int MaxAssets { get; set; }
    }
}

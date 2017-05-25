using Newtonsoft.Json;

namespace StructuredContent.Models
{
    [JsonObject]
    public class SeoSettings
    {
        public string PageTitle { get; set; }
        public string Description { get; set; }
        public string[] Keywords { get; set; }
        public ImageAsset Image { get; set; }
    }
}
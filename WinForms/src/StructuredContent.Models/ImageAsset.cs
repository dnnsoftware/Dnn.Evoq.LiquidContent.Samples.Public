using Newtonsoft.Json;

namespace StructuredContent.Models
{
    [JsonObject]
    public class ImageAsset
    {
        public string FileName { get; set; }
        public int FileId { get; set; }
        public string Url { get; set; }
    }
}
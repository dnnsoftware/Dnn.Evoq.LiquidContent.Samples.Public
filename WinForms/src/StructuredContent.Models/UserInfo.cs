using Newtonsoft.Json;

namespace StructuredContent.Models
{
    [JsonObject]
    public class UserInfo
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Thumbnail { get; set; }
    }
}

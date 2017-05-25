using Newtonsoft.Json;

namespace LiquidApp.Model
{
    [JsonObject]
    public class APIversion
    {

        [JsonProperty("version")]
        public string Version { get; set; } = string.Empty;
    }
}

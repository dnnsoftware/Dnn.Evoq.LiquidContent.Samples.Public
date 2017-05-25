using Newtonsoft.Json;

namespace LiquidContentExplorer.DTO
{
    [JsonObject]
    public class ApiVersionDto
    {
        public string Version { get; set; }
    }
}
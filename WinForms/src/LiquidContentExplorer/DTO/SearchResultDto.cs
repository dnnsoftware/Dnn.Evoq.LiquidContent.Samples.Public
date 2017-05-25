using System.Collections.Generic;
using Newtonsoft.Json;

namespace LiquidContentExplorer.DTO
{
    [JsonObject]
    public class SearchResultDto<T>
    {
        public IEnumerable<T> Documents { get; set; } = new List<T>();
        public long? TotalResultCount { get; set; } = 0;
    }
}
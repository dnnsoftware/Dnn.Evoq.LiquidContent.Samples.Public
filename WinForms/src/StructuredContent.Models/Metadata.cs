using System;
using Newtonsoft.Json;

namespace StructuredContent.Models
{
    [JsonObject]
    public class Metadata
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "_etag")]
        public string ETag { get; set; }

        public string DocType { get; set; }
        public string TenantId { get; set; }
        public string PartitionKey { get; set; }
        public DateTime? CreatedAt { get; set; }
        public UserInfo CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public UserInfo UpdatedBy { get; set; }
        public bool Deleted { get; set; }        
    }
}
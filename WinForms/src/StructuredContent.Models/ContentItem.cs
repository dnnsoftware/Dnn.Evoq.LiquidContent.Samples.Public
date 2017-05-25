using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace StructuredContent.Models
{
    [JsonObject]
    public class ContentItem
    {
        public string Id { get; set; }
        public string Slug { get; set; }
        public string ContentTypeId { get; set; }
        public string ContentTypeName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public bool AlreadyPublished { get; set; }
        public JObject Details { get; set; }
        public int CurrentVersion { get; set; }
        public int Usages { get; set; }
        public DateTime? CreatedAt { get; set; }
        public UserInfo CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public UserInfo UpdatedBy { get; set; }
        public int StateId { get; set; }
        public string[] Tags { get; set; }
        public string ClientReferenceId { get; set; }
        public SeoSettings SeoSettings { get; set; }
    }
}
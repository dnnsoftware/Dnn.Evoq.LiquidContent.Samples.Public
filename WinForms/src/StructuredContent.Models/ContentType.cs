using System.Collections.Generic;
using Newtonsoft.Json;
using StructuredContent.Models.ContentTypeFields;

namespace StructuredContent.Models
{
    [JsonObject]
    public class ContentType : Metadata
    {
        public string Name { get; set; }
        public string Icon { get; set; }
        public string Description { get; set; }
        public bool Enabled { get; set; } = true;
        public IEnumerable<ContentTypeField> Fields { get; set; }
        public int NumberOfItems { get; set; }
        public int NumberOfVisualizers { get; set; }
        public bool IsSystem { get; set; }
        public int Version { get; set; }
        public ContentTypeProperties Properties { get; set; } = new ContentTypeProperties();
    }
}

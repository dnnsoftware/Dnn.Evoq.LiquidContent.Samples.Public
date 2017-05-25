using Newtonsoft.Json;

namespace StructuredContent.Models
{
    [JsonObject]
    public class Tag : Metadata
    {
        /// <summary>
        /// Tag value in lower case. 
        /// </summary>
        /// <remarks>
        /// This is used mainly for sorting and startsWith.
        /// DocumentDB sort and startsWith is case sensitive
        /// </remarks>
        public string Value { get; set; }

        /// <summary>
        /// Number of content item that use the tag in its published version
        /// </summary>
        public int ContentItemsUsage { get; set; }

        /// <summary>
        /// If true means that this value is related to the usage of a tag
        /// associated to a given content type
        /// </summary>
        public bool ContentTypeAggregation { get; set; } = false;

        /// <summary>
        /// Content type id, used if is a Content Type Aggregation
        /// </summary>
        public string ContentTypeId { get; set; }

        /// <summary>
        /// Document version
        /// </summary>
        /// <remarks>as part of SCM-548 we changed the way the id value of this document is set.
        /// We have introduced version 2 to ignore documents created in production with a different 
        /// format for the id value</remarks>
        public int? DocVersion { get; set; }
    }
}
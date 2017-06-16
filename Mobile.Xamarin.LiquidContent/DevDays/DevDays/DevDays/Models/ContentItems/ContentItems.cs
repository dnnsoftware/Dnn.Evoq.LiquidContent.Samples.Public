using System.Collections.Generic;

using Newtonsoft.Json.Linq;

namespace DevDays.Models.ContentItems
{
    public class CreatedBy
    {
        public string id { get; set; }
        public string name { get; set; }
        public string thumbnail { get; set; }
    }

    public class UpdatedBy
    {
        public string id { get; set; }
        public string name { get; set; }
        public string thumbnail { get; set; }
    }

    public class Image
    {
        public string fileName { get; set; }
        public int fileId { get; set; }
        public string url { get; set; }
    }

    public class SeoSettings
    {
        public string pageTitle { get; set; }
        public string description { get; set; }
        public IList<string> keywords { get; set; }
        public Image image { get; set; }
    }

    public class Document
    {
        public string id { get; set; }
        public string slug { get; set; }
        public string contentTypeId { get; set; }
        public string contentTypeName { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string language { get; set; }
        public bool alreadyPublished { get; set; }
        public JObject details { get; set; }
        public int currentVersion { get; set; }
        public int usages { get; set; }
        public string createdAt { get; set; }
        public CreatedBy createdBy { get; set; }
        public string updatedAt { get; set; }
        public UpdatedBy updatedBy { get; set; }
        public int stateId { get; set; }
        public IList<string> tags { get; set; }
        public string clientReferenceId { get; set; }
        public SeoSettings seoSettings { get; set; }
        public ContentTypes.Document contentType { get; set; }
    }

    public class ContentItems
    {
        public List<Document> documents { get; set; }
        public string totalResultCount { get; set; }
    }
}

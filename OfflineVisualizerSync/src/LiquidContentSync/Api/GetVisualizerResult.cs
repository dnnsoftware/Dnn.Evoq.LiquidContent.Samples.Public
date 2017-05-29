namespace LiquidContentSync.Api
{
    public class GetVisualizerResult
    {
        public Header header { get; set; }
        public Template template { get; set; }
        public Footer footer { get; set; }
        public string contentTypeId { get; set; }
        public string contentTypeName { get; set; }
        public Script[] scripts { get; set; }
        public Cssfile[] cssFiles { get; set; }
        public bool isSystem { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string iconUrl { get; set; }
        public bool isNew { get; set; }
        public string createdAt { get; set; }
        public Createdby createdBy { get; set; }
        public string updatedAt { get; set; }
        public Updatedby updatedBy { get; set; }
        public int numberOfVisualizerInstances { get; set; }
    }

    public class Header
    {
        public string content { get; set; }
        public string name { get; set; }
        public string uri { get; set; }
        public bool isPrivate { get; set; }
    }

    public class Template
    {
        public string content { get; set; }
        public string name { get; set; }
        public string uri { get; set; }
        public bool isPrivate { get; set; }
    }

    public class Footer
    {
        public string content { get; set; }
        public string name { get; set; }
        public string uri { get; set; }
        public bool isPrivate { get; set; }
    }

    public class Script
    {
        public string content { get; set; }
        public string name { get; set; }
        public string uri { get; set; }
        public bool isPrivate { get; set; }
    }

    public class Cssfile
    {
        public string content { get; set; }
        public string name { get; set; }
        public string uri { get; set; }
        public bool isPrivate { get; set; }
    }

}

namespace LiquidContentSync.Api
{
    public class GetVisualizersResult
    {
        public Document[] documents { get; set; }
        public int totalResultCount { get; set; }
    }

    public class Document
    {
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
        public bool isSystem { get; set; }
    }

    public class Createdby
    {
        public string id { get; set; }
        public string name { get; set; }
        public string thumbnail { get; set; }
    }

    public class Updatedby
    {
        public string id { get; set; }
        public string name { get; set; }
        public string thumbnail { get; set; }
    }

}

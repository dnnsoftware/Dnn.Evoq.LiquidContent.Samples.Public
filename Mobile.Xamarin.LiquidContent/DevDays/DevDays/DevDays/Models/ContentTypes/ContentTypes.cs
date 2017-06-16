using System.Collections.Generic;

namespace DevDays.Models.ContentTypes
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

    public class Settings
    {
        public string subType { get; set; }
        public object phoneNumberFormat { get; set; }
        public int? maxAssets { get; set; }
    }

    public class RequireField
    {
        public bool active { get; set; }
        public string errorMessage { get; set; }
    }

    public class NumberOfCharacters
    {
        public bool active { get; set; }
        public string rangeDefinition { get; set; }
        public object minimum { get; set; }
        public object minimumUnit { get; set; }
        public int? maximum { get; set; }
        public object maximumUnit { get; set; }
        public string errorMessage { get; set; }
    }

    public class NumberOfReferences
    {
        public bool active { get; set; }
        public string rangeDefinition { get; set; }
        public object minimum { get; set; }
        public object minimumUnit { get; set; }
        public object maximum { get; set; }
        public object maximumUnit { get; set; }
        public string errorMessage { get; set; }
    }

    public class Validation
    {
        public RequireField requireField { get; set; }
        public NumberOfCharacters numberOfCharacters { get; set; }
        public string standardErrorMessage { get; set; }
        public NumberOfReferences numberOfReferences { get; set; }
    }

    public class Field
    {
        public string type { get; set; }
        public string defaultValue { get; set; }
        public bool setDefaultValueAsHidden { get; set; }
        public Settings settings { get; set; }
        public Validation validation { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string label { get; set; }
        public bool descriptionActive { get; set; }
        public string description { get; set; }
        public bool tooltipActive { get; set; }
        public string tooltip { get; set; }
        public int row { get; set; }
        public string width { get; set; }
        public string position { get; set; }
        public string allowedContentTypeId { get; set; }
        public string allowedContentTypeName { get; set; }
    }

    public class Settings2
    {
        public List<object> nameGeneratorFields { get; set; }
    }

    public class ContentName
    {
        public string type { get; set; }
        public Settings2 settings { get; set; }
        public string id { get; set; }
        public object name { get; set; }
        public string label { get; set; }
        public bool descriptionActive { get; set; }
        public string description { get; set; }
        public bool tooltipActive { get; set; }
        public string tooltip { get; set; }
        public int row { get; set; }
        public string width { get; set; }
        public string position { get; set; }
    }

    public class RequireField2
    {
        public bool active { get; set; }
        public string errorMessage { get; set; }
    }

    public class Validation2
    {
        public RequireField2 requireField { get; set; }
        public string standardErrorMessage { get; set; }
    }

    public class ContentTags
    {
        public string type { get; set; }
        public Validation2 validation { get; set; }
        public string id { get; set; }
        public object name { get; set; }
        public string label { get; set; }
        public bool descriptionActive { get; set; }
        public string description { get; set; }
        public bool tooltipActive { get; set; }
        public string tooltip { get; set; }
        public int row { get; set; }
        public string width { get; set; }
        public string position { get; set; }
    }

    public class RequireField3
    {
        public bool active { get; set; }
        public string errorMessage { get; set; }
    }

    public class Validation3
    {
        public RequireField3 requireField { get; set; }
        public string standardErrorMessage { get; set; }
    }

    public class ContentDescription
    {
        public string type { get; set; }
        public Validation3 validation { get; set; }
        public string id { get; set; }
        public object name { get; set; }
        public string label { get; set; }
        public bool descriptionActive { get; set; }
        public string description { get; set; }
        public bool tooltipActive { get; set; }
        public string tooltip { get; set; }
        public int row { get; set; }
        public string width { get; set; }
        public string position { get; set; }
    }

    public class Properties
    {
        public ContentName contentName { get; set; }
        public ContentTags contentTags { get; set; }
        public ContentDescription contentDescription { get; set; }
    }

    public class Document
    {
        public string id { get; set; }
        public string createdAt { get; set; }
        public CreatedBy createdBy { get; set; }
        public string updatedAt { get; set; }
        public UpdatedBy updatedBy { get; set; }
        public string name { get; set; }
        public string icon { get; set; }
        public string description { get; set; }
        public bool enabled { get; set; }
        public List<Field> fields { get; set; }
        public int numberOfItems { get; set; }
        public int numberOfVisualizers { get; set; }
        public bool isSystem { get; set; }
        public Properties properties { get; set; }
    }

    public class ContentTypes
    {
        public List<Document> documents { get; set; }
        public int totalResultCount { get; set; }
    }
}

using Newtonsoft.Json;
using StructuredContent.Models.TypeNameResolvers;

namespace StructuredContent.Models.ContentTypeFields.DateTime
{
    [JsonObject]
    public class DateTimeField : ContentTypeField, ITypeNameResolver
    {
        public TypeNameMap GetTypeNameMap()
        {
            return new TypeNameMap
            {
                Key = "DateTimeField",
                ClassFullName = typeof(DateTimeField).AssemblyQualifiedName
            };
        }

        public override Type Type { get; set; } = Type.DateTime;
        public string DefaultValue { get; set; }
        public bool SetDefaultValueAsHidden { get; set; }
        public DateTimeSettings Settings { get; set; } = new DateTimeSettings();
        public DateTimeValidation Validation { get; set; } = new DateTimeValidation();
    }
}

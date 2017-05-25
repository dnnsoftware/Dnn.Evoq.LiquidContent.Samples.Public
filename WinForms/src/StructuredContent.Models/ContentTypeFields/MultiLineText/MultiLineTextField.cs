using Newtonsoft.Json;
using StructuredContent.Models.TypeNameResolvers;

namespace StructuredContent.Models.ContentTypeFields.MultiLineText
{
    [JsonObject]
    public class MultiLineTextField : ContentTypeField, ITypeNameResolver
    {
        public TypeNameMap GetTypeNameMap()
        {
            return new TypeNameMap
            {
                Key = "MultiLineTextField",
                ClassFullName = typeof(MultiLineTextField).AssemblyQualifiedName
            };
        }

        public override Type Type { get; set; } = Type.MultiLineText;
        public string DefaultValue { get; set; }
        public bool SetDefaultValueAsHidden { get; set; }
        public MultiLineTextSettings Settings { get; set; } = new MultiLineTextSettings();
        public MultiLineTextValidation Validation { get; set; } = new MultiLineTextValidation();
    }
}

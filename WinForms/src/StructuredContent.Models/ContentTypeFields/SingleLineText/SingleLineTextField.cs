using Newtonsoft.Json;
using StructuredContent.Models.TypeNameResolvers;

namespace StructuredContent.Models.ContentTypeFields.SingleLineText
{
    [JsonObject]
    public class SingleLineTextField : ContentTypeField, ITypeNameResolver
    {
        public TypeNameMap GetTypeNameMap()
        {
            return new TypeNameMap
            {
                Key = "SingleLineTextField",
                ClassFullName = typeof(SingleLineTextField).AssemblyQualifiedName
            };
        }

        public override Type Type { get; set; } = Type.SingleLineText;
        public string DefaultValue { get; set; }
        public bool SetDefaultValueAsHidden { get; set; }
        public SingleLineTextSettings Settings { get; set; } = new SingleLineTextSettings();
        public SingleLineTextValidation Validation { get; set; } = new SingleLineTextValidation();        
    }
}

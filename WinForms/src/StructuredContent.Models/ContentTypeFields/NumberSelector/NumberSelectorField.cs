using Newtonsoft.Json;
using StructuredContent.Models.TypeNameResolvers;

namespace StructuredContent.Models.ContentTypeFields.NumberSelector
{
    [JsonObject]
    public class NumberSelectorField : ContentTypeField, ITypeNameResolver
    {
        public TypeNameMap GetTypeNameMap()
        {
            return new TypeNameMap
            {
                Key = "NumberSelectorField",
                ClassFullName = typeof(NumberSelectorField).AssemblyQualifiedName
            };
        }

        public override Type Type { get; set; } = Type.NumberSelector;
        public double? DefaultValue { get; set; }
        public bool SetDefaultValueAsHidden { get; set; }
        public NumberSelectorSettings Settings { get; set; } = new NumberSelectorSettings();
        public NumberSelectorValidation Validation { get; set; } = new NumberSelectorValidation();
    }
}

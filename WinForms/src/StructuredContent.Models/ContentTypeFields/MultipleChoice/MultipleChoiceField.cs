using Newtonsoft.Json;
using StructuredContent.Models.TypeNameResolvers;

namespace StructuredContent.Models.ContentTypeFields.MultipleChoice
{
    [JsonObject]
    public class MultipleChoiceField : ContentTypeField, ITypeNameResolver
    {
        public TypeNameMap GetTypeNameMap()
        {
            return new TypeNameMap
            {
                Key = "MultipleChoiceField",
                ClassFullName = typeof(MultipleChoiceField).AssemblyQualifiedName
            };
        }

        public override Type Type { get; set; } = Type.MultipleChoice;
        public string DefaultValue { get; set; }
        public MultipleChoiceSettings Settings { get; set; } = new MultipleChoiceSettings();
        public MultipleChoiceValidation Validation { get; set; } = new MultipleChoiceValidation();
    }
}

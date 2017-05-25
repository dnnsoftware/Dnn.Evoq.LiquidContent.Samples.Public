using Newtonsoft.Json;
using StructuredContent.Models.TypeNameResolvers;

namespace StructuredContent.Models.ContentTypeFields.StaticText
{
    [JsonObject]
    public class StaticTextField : ContentTypeField, ITypeNameResolver
    {
        public TypeNameMap GetTypeNameMap()
        {
            return new TypeNameMap
            {
                Key = "StaticTextField",
                ClassFullName = typeof(StaticTextField).AssemblyQualifiedName
            };
        }

        public override Type Type { get; set; } = Type.StaticText;
        public StaticTextSettings Settings { get; set; } = new StaticTextSettings();
    }
}

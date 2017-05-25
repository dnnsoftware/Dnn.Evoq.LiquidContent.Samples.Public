using Newtonsoft.Json;
using StructuredContent.Models.TypeNameResolvers;

namespace StructuredContent.Models.ContentTypeFields.Assets
{
    [JsonObject]
    public class AssetsField : ContentTypeField, ITypeNameResolver
    {
        public TypeNameMap GetTypeNameMap()
        {
            return new TypeNameMap
            {
                Key = "AssetsField",
                ClassFullName = typeof(AssetsField).AssemblyQualifiedName
            };
        }

        public override Type Type { get; set; } = Type.Assets;
        public AssetsSettings Settings { get; set; } = new AssetsSettings();
        public AssetsValidation Validation { get; set; } = new AssetsValidation();
    }
}

using Newtonsoft.Json;
using StructuredContent.Models.TypeNameResolvers;

namespace StructuredContent.Models.ContentTypeFields.ReferenceObject
{
    [JsonObject]
    public class ReferenceObjectField : ContentTypeField, ITypeNameResolver
    {
        public TypeNameMap GetTypeNameMap()
        {
            return new TypeNameMap
            {
                Key = "ReferenceObjectField",
                ClassFullName = typeof(ReferenceObjectField).AssemblyQualifiedName
            };
        }

        public override Type Type { get; set; } = Type.ReferenceObject;
        public string AllowedContentTypeId { get; set; }
        public string AllowedContentTypeName { get; set; }
        public ReferenceObjectSettings Settings { get; set; } = new ReferenceObjectSettings();
        public ReferenceObjectValidation Validation { get; set; } = new ReferenceObjectValidation();
    }
}

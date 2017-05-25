using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StructuredContent.Models.ContentTypeFields;
using StructuredContent.Models.ContentTypeFields.Assets;
using StructuredContent.Models.ContentTypeFields.ContentDescription;
using StructuredContent.Models.ContentTypeFields.ContentName;
using StructuredContent.Models.ContentTypeFields.ContentTags;
using StructuredContent.Models.ContentTypeFields.DateTime;
using StructuredContent.Models.ContentTypeFields.MultiLineText;
using StructuredContent.Models.ContentTypeFields.MultipleChoice;
using StructuredContent.Models.ContentTypeFields.NumberSelector;
using StructuredContent.Models.ContentTypeFields.ReferenceObject;
using StructuredContent.Models.ContentTypeFields.SingleLineText;
using StructuredContent.Models.ContentTypeFields.StaticText;
using Type = System.Type;
using ScFieldType = StructuredContent.Models.ContentTypeFields.Type;

// Adapted from http://www.tomdupont.net/2014/04/deserialize-abstract-classes-with.html
namespace StructuredContent.Models.TypeNameResolvers
{
    public abstract class AbstractFieldConverter<T> : JsonConverter
    {
        protected abstract T Create(JObject jObject);

        public override bool CanConvert(Type objectType)
        {
            return typeof(T).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jObject = JObject.Load(reader);
            var target = Create(jObject);
            serializer.Populate(jObject.CreateReader(), target);

            return target;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        protected static bool FieldExists(JObject jObject, string name, JTokenType type)
        {
            JToken token;
            return jObject.TryGetValue(name, out token) && token.Type == type;
        }
    }

    public class ContentTypeFieldConverter : AbstractFieldConverter<ContentTypeField>
    {
        protected override ContentTypeField Create(JObject jObject)
        {
            if (FieldExists(jObject, "type", JTokenType.String))
            {
                var first1 = jObject.First;
                var first2 = first1.First;
                var val = first2.Value<string>();
                ScFieldType fieldType;
                if (!Enum.TryParse(val, true, out fieldType))
                {
                    throw new InvalidDataException();
                }

                switch (fieldType)
                {
                    case ScFieldType.SingleLineText:
                        return new SingleLineTextField();
                    case ScFieldType.MultiLineText:
                        return new MultiLineTextField();
                    case ScFieldType.Assets:
                        return new AssetsField();
                    case ScFieldType.ReferenceObject:
                        return new ReferenceObjectField();
                    case ScFieldType.MultipleChoice:
                        return new MultipleChoiceField();
                    case ScFieldType.NumberSelector:
                        return new NumberSelectorField();
                    case ScFieldType.DateTime:
                        return new DateTimeField();
                    case ScFieldType.StaticText:
                        return new StaticTextField();
                    case ScFieldType.ContentName:
                        return new ContentNameField();
                    case ScFieldType.ContentTags:
                        return new ContentTagsField();
                    case ScFieldType.ContentDescription:
                        return new ContentDescriptionField();
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            throw new InvalidOperationException();
        }
    }
}
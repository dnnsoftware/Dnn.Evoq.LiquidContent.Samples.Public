using System.Collections.Generic;
using Newtonsoft.Json;

namespace StructuredContent.Models.ContentTypeFields.ContentName
{
    [JsonObject]
    public class ContentNameSettings
    {
        public IEnumerable<string> NameGeneratorFields { get; set; } = new string[0];
    }
}
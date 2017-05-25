using System.Collections.Generic;
using Newtonsoft.Json;

namespace StructuredContent.Models.ContentTypeFields.MultipleChoice
{
    [JsonObject]
    public class MultipleChoiceSettings
    {
        public MultipleChoiceFormat SubType { get; set; }
        public IEnumerable<Choice> Choices { get; set; } = new List<Choice>();
        public bool MultiSelect { get; set; } = false;
        public Orientation ListOrientation { get; set; } = Orientation.Horizontal;
        public bool OtherAsAnOption { get; set; } = false;
    }
}

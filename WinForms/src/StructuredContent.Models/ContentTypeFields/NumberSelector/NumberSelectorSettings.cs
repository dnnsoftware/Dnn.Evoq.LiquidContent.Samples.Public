using Newtonsoft.Json;

namespace StructuredContent.Models.ContentTypeFields.NumberSelector
{
    [JsonObject]
    public class NumberSelectorSettings
    {
        public NumberSelectorFormat SubType { get; set; }
        public NumberSetFormat NumberSetFormat { get; set; } = NumberSetFormat.Integer;
        public Orientation SpinnerOrientation { get; set; } = Orientation.Horizontal;
        public NumberSelectorRange NumberRange { get; set; } = new NumberSelectorRange();
    }
}

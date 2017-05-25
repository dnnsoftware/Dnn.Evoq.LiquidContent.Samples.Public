using System;
using Newtonsoft.Json;

namespace StructuredContent.Models.ContentTypeFields
{
    [JsonObject]
    public abstract class ContentTypeField
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string Label { get; set; }
        public abstract Type Type { get; set; }
        public bool DescriptionActive { get; set; }
        public string Description { get; set; }
        public bool TooltipActive { get; set; }
        public string Tooltip { get; set; }
        public int Row { get; set; }
        public Width? Width { get; set; }
        public Position? Position { get; set; }
    }
}
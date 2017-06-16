using Newtonsoft.Json;
using System.Collections.Generic;

namespace Dnn.Alexa.Summit.Lambda.viewmodels
{


    public class DirectionContentViewModel
    {
        [JsonProperty("documents")]
        public List<Direction> direction { get; set; }
    }

    public class Direction
    {
        public string id { get; set; }
        public string name { get; set; }
        public DirectionDetails details { get; set; }
    }

    public class DirectionDetails
    {
        public string header { get; set; }
        public string embedCode { get; set; }
        public string mapUrl { get; set; }
        public Image[] mapImage { get; set; }
        public string directions { get; set; }
    }

}

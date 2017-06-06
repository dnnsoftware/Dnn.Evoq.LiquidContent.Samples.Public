using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dnn.Alexa.Summit.Lambda.viewmodels
{
    class SpeakerContentViewModel
    {
        [JsonProperty("documents")]
        public List<Speaker> speakers { get; set; }
    }

    public class Speaker
    {
        public string id { get; set; }
        public string contentTypeId { get; set; }
        public string contentTypeName { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public SpeakerDetails details { get; set; }
    }

    public class SpeakerDetails
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public List<Image> profileImage { get; set; }
        public string title { get; set; }
        public string shortBiography { get; set; }
        public string longBiography { get; set; }
        public string email { get; set; }
        public string linkedIn { get; set; }
        public string twitterHandle { get; set; }
    }
}



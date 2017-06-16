using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Amazon.Lambda.Core;
using Alexa.NET;
using Alexa.NET.Request;
using Alexa.NET.Response;
using System.Net.Http;
using Newtonsoft.Json;
using Alexa.NET.Request.Type;
using System.Linq;
using Dnn.Alexa.Summit.Lambda.viewmodels;


// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializerAttribute(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
namespace Dnn.Alexa.Summit.Lambda
{
    public enum ContentTypes
    {
        Directions,
        EventSpeaker
    }

    public class Function
    {
        // URL templates for Liquid Content
        const string contentUrl = "https://dnnapi.com/content/api/ContentItems/?maxItems=1&tags={1}&contentTypeId={0}";


        private ILambdaContext Context { get; set; }

        // Liquid Content requires an API Key to access the APIs
        private String ApiKey { get; set; }

        // This will hold whatever ContentTypes we want to work with
        private Dictionary<ContentTypes, String> ContentTypeIdList = new Dictionary<ContentTypes, string>();

        private readonly IDictionary<string, Func<SkillRequest, SkillResponse>> handlers;

        public Function()
        {
            handlers = new Dictionary<string, Func<SkillRequest, SkillResponse>>() {
                { "LaunchRequest", HandleLaunchRequest },
                { "IntentRequest", HandleIntentRequest },
                { "SessionEndedRequest", HandleSessionEndRequest },

                { "SendDirections", HandleSendDirectionsIntent },
                { "GetSpeaker", HandleGetSpeakerIntent },
            };
        }

        // Intent handlers encapsulate the business logic for each custom intent
        #region Intent Handlers
        public SkillResponse HandleSendDirectionsIntent(SkillRequest input)
        {
            // Log the method type for debugging purposes
            Context.Logger.LogLine("Calling SendDirections Intent");

            string title = "Liquid Summit Directions";
            string speech = "Thank you for your interest in the Liquid Summit Conference.  You can find directions to the event in your Alexa app.";


            var directions = GetDirectionsAsync().Result;

            if (directions is null)
            {
                return ResponseBuilder.TellWithCard(
                    new PlainTextOutputSpeech()
                    {
                        Text = speech
                    },
                    title,
                    speech
                );
            }

            return new SkillResponse()
            {
                Version = "1.0",
                Response = new ResponseBody()
                {
                    Card = new StandardCard()
                    {
                        Title = $"{title}",
                        Content = $"{directions.details.directions}",
                        Image = new CardImage
                        {
                            SmallImageUrl = directions.details.mapImage.First().url,
                            LargeImageUrl = directions.details.mapImage.First().url
                        }
                    },
                    OutputSpeech = new PlainTextOutputSpeech()
                    {
                        Text = speech
                    },
                    ShouldEndSession = true
                }
            };
        }

        public SkillResponse HandleGetSpeakerIntent(SkillRequest input)
        {
            // Log the method type for debugging purposes
            Context.Logger.LogLine("Calling GetSpeaker Intent");

            string title = "Keynote Speaker";
            string speech = "I'm sorry. The selection committee is still working to identify a keynote speaker.";

            var response = GetKeynoteSpeakerAsync().Result;

            if (response != null)
            {
                speech = $"We are pleased to announce that {response.firstName} {response.lastName}, ";
                speech += $"{ response.title} will be the keynote speaker at the Liquid Summit conference. ";
                speech += $"{response.shortBiography}";
            }

            return ResponseBuilder.TellWithCard(
                new PlainTextOutputSpeech()
                {
                    Text = speech
                },
                title,
                speech            
            );
        }
        #endregion

        // The service handlers are responsible for making service calls to Evoq
        #region Service Handlers
        private async Task<SpeakerDetails> GetKeynoteSpeakerAsync()
        {

            string url = string.Format(contentUrl, ContentTypeIdList[ContentTypes.EventSpeaker], "keynote");

            var json = await GetContentAsync(url);

            Context.Logger.LogLine("GetKeynoteSpeakerAsync:");
            Context.Logger.LogLine("-------------------------------");
            Context.Logger.LogLine($"Address: {url}");
            Context.Logger.LogLine($"Results: {json}");
            Context.Logger.LogLine("-------------------------------");

            var speakerList = JsonConvert.DeserializeObject<SpeakerContentViewModel>(json);
            var obj = JsonConvert.SerializeObject(speakerList);

            Context.Logger.LogLine($"Results: {speakerList.speakers.Count}");

            if (speakerList.speakers == null || speakerList.speakers.Count == 0) return null;


            return speakerList.speakers?.First()?.details;

        }

        private async Task<Direction> GetDirectionsAsync()
        {
            string url = string.Format(contentUrl, ContentTypeIdList[ContentTypes.Directions], string.Empty);

            var json = await GetContentAsync(url);

            Context.Logger.LogLine("GetDirectionsAsync:");
            Context.Logger.LogLine("-------------------------------");
            Context.Logger.LogLine($"TypeID: {ContentTypeIdList[ContentTypes.Directions]}");
            Context.Logger.LogLine($"Address: {url}");
            Context.Logger.LogLine($"Results: {json}");
            Context.Logger.LogLine("-------------------------------");

            var contentList = JsonConvert.DeserializeObject<DirectionContentViewModel>(json);

            var ci = contentList.direction.First();

            return ci;

        }

        private async Task<string> GetContentAsync(string Url)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(Url)
            };

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", ApiKey);

            var response = client.GetAsync("");
            var json = await response.Result.Content.ReadAsStringAsync();

            return json;
        }
        #endregion

        // Request Handlers are responsible for the three request types we could recieve.
        #region Request Handlers
        /// <summary>
        /// A sample Launch Request handler.  This returns rudimentary information about the app.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public SkillResponse HandleLaunchRequest(SkillRequest input)
        {
            // Log the method type for debugging purposes
            Context.Logger.LogLine("Calling HandleLaunchRequest");

            return ResponseBuilder.Tell(
                new PlainTextOutputSpeech()
                {
                    Text = "Welcome to the Liquid Summit. I am here to help you find your way around the conference."
                }
            );

        }

        /// <summary>
        /// The HandleIntentRequest method processes all intent requests and will 
        /// delegate the request to the appropriate intent handler based on the name of
        /// the intent that is requested.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public SkillResponse HandleIntentRequest(SkillRequest input)
        {
            // Log the method type for debugging purposes
            Context.Logger.LogLine("Calling HandleRequest");
            var request = input.Request as IntentRequest;
            return handlers[request.Intent.Name](input);
        }

        public SkillResponse HandleSessionEndRequest(SkillRequest input)
        {
            // Log the method type for debugging purposes
            Context.Logger.LogLine("Calling HandleSessionEndRequest");

            return ResponseBuilder.Empty();
        }

        #endregion

        /// <summary>
        /// The main entry point for our Alexa Skill
        /// </summary>
        /// <param name="input">This is the request object from Alexa</param>
        /// <param name="context">The context information for the request</param>
        /// <returns></returns>
        public SkillResponse Handler(SkillRequest input, ILambdaContext context)
        {
            Context = context;

            ApiKey = Environment.GetEnvironmentVariable("apikey");


            Array values = Enum.GetValues(typeof(ContentTypes));
            foreach (ContentTypes val in values)
            {
                var t = Enum.GetName(typeof(ContentTypes), val);

                ContentTypeIdList[val] = Environment.GetEnvironmentVariable(t);
                Context.Logger.LogLine($"{t}: {ContentTypeIdList[val]}");
            }

            return handlers[input.Request.Type](input);
        }

    }
}

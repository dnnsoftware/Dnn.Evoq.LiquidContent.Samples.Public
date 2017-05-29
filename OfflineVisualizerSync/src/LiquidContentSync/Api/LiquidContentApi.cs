using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace LiquidContentSync.Api
{
    public class LiquidContentApi
    {
        private static HttpResponseMessage GetWebApi(string queryString)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(Settings.LiquidApiAddress)
            };
            client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse($"bearer {Settings.ApiKey}");

            var result = client.GetAsync(queryString).Result;

            result.EnsureSuccessStatusCode();

            return result;
        }

        private static HttpResponseMessage PutWebApi(string queryString, string jsonString)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(Settings.LiquidApiAddress)
            };
            client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse($"bearer {Settings.ApiKey}");

            var content = new StringContent(jsonString);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var result = client.PutAsync(queryString, content).Result;

            result.EnsureSuccessStatusCode();

            return result;
        }


        public static GetVisualizersResult GetVisualizers(bool includeBuiltInVisualizers = false)
        {
            var response = GetWebApi(includeBuiltInVisualizers ? @"Visualizers?maxitems=20&fieldorder=createdAt&startIndex=1" : @"Visualizers?maxitems=20&fieldorder=createdAt&startIndex=19");

            var stream = response.Content.ReadAsStreamAsync().Result;
            var serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(GetVisualizersResult));
            var visualizersResult = (GetVisualizersResult)serializer.ReadObject(stream);

            return visualizersResult;
        }
        public static GetVisualizerResult GetVisualizer(string vizId)
        {
            var response = GetWebApi($"Visualizers?id={vizId}");

            var stream = response.Content.ReadAsStreamAsync().Result;
            var serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(GetVisualizerResult));
            var visualizerResult = (GetVisualizerResult)serializer.ReadObject(stream);

            return visualizerResult;
        }

        public static void PutVisualizer(GetVisualizerResult visualizer)
        {
            var stream = new MemoryStream();
            var serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(GetVisualizerResult));
            serializer.WriteObject(stream, visualizer);

            stream.Seek(0, SeekOrigin.Begin);
            var jsonString = Encoding.UTF8.GetString(stream.ToArray());
            
            PutWebApi($"Visualizers?id={visualizer.id}", jsonString);
        }
    }
}

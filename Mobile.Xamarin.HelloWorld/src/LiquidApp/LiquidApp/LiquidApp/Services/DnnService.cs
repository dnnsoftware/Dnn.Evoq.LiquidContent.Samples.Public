using LiquidApp.Model;
using System;
using System.Net;
using System.Collections.Generic;
using System.Net.Http;
using static Newtonsoft.Json.JsonConvert;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace LiquidApp.Services
{
    public class DnnService
    {
        const string MsaVersion = "https://dnnapi.com/content/api/version";

        public APIversion GetVersion()
        {
            var version = new APIversion();

            using (var client = GetClient())
            {
                var response = client.GetAsync("https://dnnapi.com/content/api/version").Result;
                var content = response.Content.ReadAsStringAsync().Result;
                if (!response.IsSuccessStatusCode)
                {
                    // show error here
                    version.Version = "ERROR: " + response.ReasonPhrase;
                }
                else
                {
                    version = JsonConvert.DeserializeObject<APIversion>(content);
                }
            }

            return version;
        }

        private HttpClient GetClient()
        {
            var client = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(45),
            };
            var headers = client.DefaultRequestHeaders;
            headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            headers.Authorization = new AuthenticationHeaderValue("Bearer", "cbf9b53593548cf077f9e03a067e75cc");
            return client;
        }

    }
}

using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace LiquidContentExplorer.Common
{
    public static class Utils
    {
        public static HttpClient GetLiquidContentClient(string apiKey)
        {
            var client = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(45),
            };

            var headers = client.DefaultRequestHeaders;
            headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            headers.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            return client;
        }
    }
}
using System;
using System.Net.Http;
using LiquidContentExplorer.DTO;
using Newtonsoft.Json;
using StructuredContent.Models;
using StructuredContent.Models.TypeNameResolvers;

namespace LiquidContentExplorer.Common
{
    public static class ApiCalls
    {
        public static ApiVersionDto GetSetviceVersion(HttpClient client, string serviceUrl)
        {
            var url = serviceUrl + Constants.GetServiceVersionPath;
            var response = client.GetAsync(url).Result;
            var content = response.Content.ReadAsStringAsync().Result;

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"StatusCode: {response.StatusCode:D} {response.StatusCode}. {content}");
            }

            var version = JsonConvert.DeserializeObject<ApiVersionDto>(content);
            return version;
        }

        public static SearchResultDto<ContentType> GetAllContentTypes(HttpClient client, string serviceUrl)
        {
            var url = serviceUrl + Constants.GetContentTypesPath;
            var query = new QueryDto
            {
                StartIndex = 0,
                MaxItems = 100,
            };

            var response = client.GetAsync(url + query.ToQueryString()).Result;
            var content = response.Content.ReadAsStringAsync().Result;

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"StatusCode: {response.StatusCode:D} {response.StatusCode}. {content}");
            }

            var converter = new ContentTypeFieldConverter();
            var contentTypes = JsonConvert.DeserializeObject<SearchResultDto<ContentType>>(content, converter);
            return contentTypes;
        }

        public static SearchResultDto<ContentItem> GetAllContentItems(HttpClient client, string serviceUrl)
        {
            var url = serviceUrl + Constants.GetContentItemsTypesPath;
            var query = new QueryDto
            {
                StartIndex = 0,
                MaxItems = 100,
            };

            var response = client.GetAsync(url + query.ToQueryString()).Result;
            var content = response.Content.ReadAsStringAsync().Result;

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"StatusCode: {response.StatusCode:D} {response.StatusCode}. {content}");
            }

            var contentItems = JsonConvert.DeserializeObject<SearchResultDto<ContentItem>>(content);
            return contentItems;
        }
    }
}
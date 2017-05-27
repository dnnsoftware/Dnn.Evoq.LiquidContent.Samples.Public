using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using static Newtonsoft.Json.JsonConvert;
using DevDays.Models.ContentItems;
using Xamarin.Forms;

using ModernHttpClient;

[assembly: Dependency(typeof(DevDays.Services.ContentItemDataStore))]
namespace DevDays.Services
{
    public class ContentItemDataStore : IContentItemDataStore<Document>
    {
        private const string ApiKey = "9eaa38824b300cb1cfa1f16174b88ba5";
        private const string ApiUrl = "https://dnnapi.com/content/api/ContentItems";

        private bool _isInitialized;
        private List<Document> _contentItems = new List<Document>();

        private static readonly HttpClient WebClient = new HttpClient(new NativeMessageHandler());

        public async Task<bool> AddContentItemAsync(Document contentItem)
        {
            await InitializeAsync();

            _contentItems.Add(contentItem);

            try
            {
                WebClient.DefaultRequestHeaders.Accept.Clear();
                WebClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                WebClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ApiKey);

                WebClient.BaseAddress = new Uri("https://dnnapi.com");

                var content = new StringContent(SerializeObject(contentItem), Encoding.UTF8, "application/json");

                var result = WebClient.PostAsync("/content/api/ContentItems", content).Result;
            }
            catch (Exception e)
            {
                return await Task.FromResult(false);
            }

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateContentItemAsync(Document contentItem)
        {
            await InitializeAsync();

            var existing = _contentItems.FirstOrDefault(arg => arg.id == contentItem.id);
            if (existing == null) return false;

            _contentItems.Remove(existing);
            _contentItems.Add(contentItem);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteContentItemAsync(Document contentItem)
        {
            await InitializeAsync();

            var existing = _contentItems.FirstOrDefault(arg => arg.id == contentItem.id);
            if (existing == null) return false;

            _contentItems.Remove(existing);

            return await Task.FromResult(true);
        }

        public async Task<Document> GetContentItemAsync(string id)
        {
            await InitializeAsync();

            return await Task.FromResult(_contentItems.FirstOrDefault(s => s.id == id));
        }

        public async Task<IEnumerable<Document>> GetContentItemsAsync(bool forceRefresh = false)
        {
            await InitializeAsync();

            return await Task.FromResult(_contentItems);
        }

        public Task<bool> PullLatestAsync()
        {
            return Task.FromResult(true);
        }

        public Task<bool> SyncAsync()
        {
            return Task.FromResult(true);
        }

        public async Task InitializeAsync()
        {
            try
            {
                if (_isInitialized)
                    return;

                WebClient.DefaultRequestHeaders.Accept.Clear();
                WebClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                WebClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ApiKey);

                var json = await WebClient.GetStringAsync(ApiUrl);

                var contentItems = DeserializeObject<ContentItems>(json);

                _contentItems = contentItems.documents;

                _isInitialized = true;
            }
            catch (Exception e)
            {
                _isInitialized = false;
            }
        }

    }
}

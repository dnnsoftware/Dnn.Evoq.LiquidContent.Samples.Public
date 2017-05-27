using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using static Newtonsoft.Json.JsonConvert;
using DevDays.Models.ContentTypes;
using ModernHttpClient;
using Xamarin.Forms;

[assembly: Dependency(typeof(DevDays.Services.ContentTypeDataStore))]
namespace DevDays.Services
{
    public class ContentTypeDataStore : IContentTypeDataStore<Document>
    {
        #region private
        private const string ApiKey = "9eaa38824b300cb1cfa1f16174b88ba5";
        private const string ApiUrl = "https://dnnapi.com/content/api/ContentTypes";
        #endregion

        private bool _isInitialized;
        private List<Document> _contentTypes = new List<Document>();

        private static readonly HttpClient WebClient = new HttpClient(new NativeMessageHandler());

        public async Task<bool> AddContentTypeAsync(Document contentType)
        {
            await InitializeAsync();

            _contentTypes.Add(contentType);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateContentTypeAsync(Document contentType)
        {
            await InitializeAsync();

            var existing = _contentTypes.FirstOrDefault(arg => arg.id == contentType.id);
            if (existing == null) return false;

            _contentTypes.Remove(existing);
            _contentTypes.Add(contentType);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteContentTypeAsync(Document contentType)
        {
            await InitializeAsync();

            var existing = _contentTypes.FirstOrDefault(arg => arg.id == contentType.id);
            if (existing == null) return false;

            _contentTypes.Remove(existing);

            return await Task.FromResult(true);
        }

        public async Task<Document> GetContentTypeAsync(string id)
        {
            await InitializeAsync();

            return await Task.FromResult(_contentTypes.FirstOrDefault(s => s.id == id));
        }

        public Document GetContentType(string id)
        {
            return _contentTypes.FirstOrDefault(s => s.id == id);
        }

        public async Task<IEnumerable<Document>> GetContentTypesAsync(bool forceRefresh = false)
        {
            await InitializeAsync();

            return await Task.FromResult(_contentTypes);
        }

        #region View methods
        public Task<bool> PullLatestAsync()
        {
            return Task.FromResult(true);
        }

        public Task<bool> SyncAsync()
        {
            return Task.FromResult(true);
        }
        #endregion

        public async Task InitializeAsync()
        {
            if (_isInitialized)
                return;
            
            WebClient.DefaultRequestHeaders.Accept.Clear();
            WebClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            WebClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ApiKey);

            var json = await WebClient.GetStringAsync(ApiUrl);

            var contentTypes = DeserializeObject<ContentTypes>(json);

            _contentTypes = contentTypes.documents;

            _isInitialized = true;
        }

    }
}

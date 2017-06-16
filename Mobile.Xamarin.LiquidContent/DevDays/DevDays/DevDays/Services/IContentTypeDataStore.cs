using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevDays.Services
{
    public interface IContentTypeDataStore<T>
    {
        Task<bool> AddContentTypeAsync(T item);
        Task<bool> UpdateContentTypeAsync(T item);
        Task<bool> DeleteContentTypeAsync(T item);
        Task<T> GetContentTypeAsync(string id);
        T GetContentType(string id);

        Task<IEnumerable<T>> GetContentTypesAsync(bool forceRefresh = false);

        Task InitializeAsync();
        Task<bool> PullLatestAsync();
        Task<bool> SyncAsync();
    }
}

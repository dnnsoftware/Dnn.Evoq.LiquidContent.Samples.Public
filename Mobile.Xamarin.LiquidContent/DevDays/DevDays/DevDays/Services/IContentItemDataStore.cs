using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevDays.Services
{
    public interface IContentItemDataStore<T>
    {
        Task<bool> AddContentItemAsync(T item);
        Task<bool> UpdateContentItemAsync(T item);
        Task<bool> DeleteContentItemAsync(T item);
        Task<T> GetContentItemAsync(string id);
        Task<IEnumerable<T>> GetContentItemsAsync(bool forceRefresh = false);

        Task InitializeAsync();
        Task<bool> PullLatestAsync();
        Task<bool> SyncAsync();
    }
}

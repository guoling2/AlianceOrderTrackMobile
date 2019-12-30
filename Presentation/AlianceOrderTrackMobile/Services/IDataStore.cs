using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlianceOrderTrackMobile.Services
{
    public interface IDataStore<T>
    {
        Task<bool> AddItemAsync(T item);
        Task<bool> UpdateItemAsync(T item);
        Task<bool> DeleteItemAsync(T item);
        Task<T> GetItemAsync(string id);
        Task<List<T>> GetItemsAsync(SystemSearchParameter systemSearch=null);
        Task InitializeAsync();
        Task<bool> PullLatestAsync();
        Task<bool> SyncAsync();
    }

    public abstract class SystemSearchParameter
    {
        
    }
}

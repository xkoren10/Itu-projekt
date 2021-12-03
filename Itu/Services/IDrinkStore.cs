using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Itu.Services
{
    public interface IDrinkStore <T>
    {
        Task<bool> AddItemsAsync(T Item);
        Task<bool> UpdateItemsAsync(T Item);
        Task<bool> DeleteItemsAsync(string id);
        Task<T> GetItemsAsync(string id);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
    }
}

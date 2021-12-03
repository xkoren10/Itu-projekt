using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Itu.Services
{
    public interface IDataStore<T>
    {
        Task<bool> AddPersonAsync(T Person);
       Task<bool> UpdatePersonAsync(T Person);
        Task<bool> DeletePersonAsync(string id);
        Task<T> GetPersonAsync(string id);
        Task<IEnumerable<T>> GetPersonAsync(bool forceRefresh = false);
    }
}

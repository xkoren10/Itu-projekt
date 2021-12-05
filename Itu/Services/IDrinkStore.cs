using Itu.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

/*
Funkcie na prácu s položkmi. Autor: Matej Koreň
*/


namespace Itu.Services
{
    public interface IDrinkStore <T>
    {
        Task<bool> AddItemsAsync(T Item, Person person);
        Task<bool> UpdateItemsAsync(T Item, Person person);
        Task<bool> DeleteItemsAsync(string id, Person person);
        Task<IEnumerable<T>> GetItemsAsync( Person person,bool forceRefresh = false);
    }
}

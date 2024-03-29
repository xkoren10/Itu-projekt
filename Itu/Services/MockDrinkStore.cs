﻿using Itu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
Implementácia funkcii na prácu s položkami. Autor: Matej Koreň
*/


namespace Itu.Services
{
   public class MockDrinkStore : MockDataStore, IDrinkStore<Item>
    {



        public MockDrinkStore()
        {
           

        }

        public async Task<bool> AddItemsAsync(Item item, Person person)
        {

            person.Items.Add(item);
      

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemsAsync(Item item, Person person)
        {
            var oldItem = person.Items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();

            int index = person.Items.IndexOf(oldItem);
            person.Items.Remove(oldItem);
            person.Items.Insert(index,item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemsAsync(string id, Person person)
        {
            var oldItem = person.Items.Where((Item arg) => arg.Id == id).FirstOrDefault();
            person.Items.Remove(oldItem);

            return await Task.FromResult(true);
        }


        public async Task<IEnumerable<Item>> GetItemsAsync(Person person,bool forceRefresh = false)
        {
            return await Task.FromResult(person.Items);
        }
    }



}


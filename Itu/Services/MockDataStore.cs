﻿using Itu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


/*
Implementácia funkcii na prácu s osobami. Autor: Marek Križan
*/



namespace Itu.Services
{
    public class MockDataStore : IDataStore<Person>
    {
       public readonly List<Person> persons;

        public MockDataStore()
        {
            persons = new List<Person>();
            
        }

        public async Task<bool> AddPersonAsync(Person item)
        {
            persons.Add(item);

            return await Task.FromResult(true);
        }


        public async Task<bool> UpdatePersonAsync( Person person)
        {
            var oldItem = persons.Where((Person arg) => arg.Id == person.Id).FirstOrDefault();

            int index = persons.IndexOf(oldItem);
            persons.Remove(oldItem);
            persons.Insert(index, person);

            return await Task.FromResult(true);
        }


        public async Task<bool> DeletePersonAsync(string id)
        {
            var oldItem = persons.Where((Person arg) => arg.Id == id).FirstOrDefault();
            persons.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Person> GetPersonAsync(string id)
        {
            return await Task.FromResult(persons.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Person>> GetPersonAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(persons);
        }
    }
}
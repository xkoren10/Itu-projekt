﻿using Itu.Models;
using Itu.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Itu.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ItemDetailViewModel : BaseViewModel
    {



        private string itemId;
        private int val = 0;
        private string valdisplay = "0";
        private string meno;





        public string Id { get; set; }

        public string Meno
        {
            get => meno;
            set => SetProperty(ref meno, value);
        }

        public string Value
        {
            get => valdisplay;
            set
            {
                valdisplay = value;
                OnPropertyChanged(nameof(Value));

            }
        }

        

        public string ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                LoadItemId(value);
            }
        }

        public async void LoadItemId(string itemId)
        {
            try
            {
                var item = await DataStore.GetPersonAsync(itemId);
                Id = item.Id;
                Meno = item.Text;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            
        }



        public ObservableCollection<Item> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }

        public Command IncrementCommand { get; }

        public Command DecrementCommand { get; }


        public ItemDetailViewModel()
        {

            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            IncrementCommand = new Command<Item>(IncrementValue);
            DecrementCommand = new Command<Item>(DecrementValue);

            AddItemCommand = new Command(OnAddItem);
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                System.Collections.Generic.IEnumerable<Item> items = await DataStore2.GetItemsAsync(true);
                foreach (Item item in items)
                {
                    Items.Add(item);
                    
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

 

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewDrinkPage));
        }

        public void IncrementValue(Item item)
        {
            item.Ammount++;
            Value = $"{item.Ammount}";
        }

        public void DecrementValue(Item item)
        {
           item.Ammount--;
            Value = $"{item.Ammount}";
        }




    }
}

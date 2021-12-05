using Itu.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace Itu.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class NewDrinkViewModel: BaseViewModel

    {

   
        public string itemId;
        private string text;
        private string price;
        private double ammount = 0.0;
        public NewDrinkViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
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
                LoadItemId2(value);
            }
        }

        public async void LoadItemId2(string itemId)
        {
            try
            {
                var item = await DataStore.GetPersonAsync(itemId);

            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }


        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(text) && !String.IsNullOrWhiteSpace(price);


        }

        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }

        public string Price
        {
            get => price;
            set => SetProperty(ref price, value);
        }

        public double Ammount
        {
            get => ammount;
            set => SetProperty(ref ammount, value);
        }


        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            Item newItem = new Item()
            {
                Id = Guid.NewGuid().ToString(),
                Text = Text,
                Price = Price,
                Ammount = Ammount

            };

            Person person = await DataStore.GetPersonAsync(ItemId);
            await DataStore2.AddItemsAsync(newItem,person);

            await Shell.Current.GoToAsync(".."); 
        }
    }
}

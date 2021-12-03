using Itu.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Itu.ViewModels
{
    public class NewDrinkViewModel : BaseViewModel

    {
        private string text;
        private string price;

        public NewDrinkViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
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

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            Item newItem = new Item()
            {
                Id = Guid.NewGuid().ToString(),
                Text = Text,
                Price = Price
            };

            await DataStore2.AddItemsAsync(newItem);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}

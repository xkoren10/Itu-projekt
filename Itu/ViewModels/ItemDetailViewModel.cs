using Itu.Models;
using Itu.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

/*
View Model pre stránku s položkami konkrétnej osoby. Autor: Marek Križan
*/



namespace Itu.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ItemDetailViewModel : BaseViewModel
    {



        public string itemId;
        private string meno;
        private string suma ;
        public double suma_double = 0.0;





        public string Id { get; set; }

        public string Meno
        {
            get => meno;
            set => SetProperty(ref meno, value);
        }

        public string Suma
        {
            get => suma;
            set => SetProperty(ref suma, value);
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
            CountSum();

            
        }









        public ObservableCollection<Item> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }

        public Command IncrementCommand { get; }

        public Command DecrementCommand { get; }

        public Command<Item> DeleteItem{ get; }
       


        public ItemDetailViewModel()
        {

            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            IncrementCommand = new Command<Item>(IncrementValue);
            DecrementCommand = new Command<Item>(DecrementValue);
            DeleteItem = new Command<Item>(Deletion);

            AddItemCommand = new Command(OnAddItem);
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
               var person = await DataStore.GetPersonAsync(Id);
                System.Collections.Generic.IEnumerable<Item> items = await DataStore2.GetItemsAsync(person,true);
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

        private async void Deletion(Item item)
        {

            Person person = await DataStore.GetPersonAsync(ItemId);
            await DataStore2.DeleteItemsAsync(item.Id,person);
            await ExecuteLoadItemsCommand();
            CountSum();

        }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync($"{nameof(NewDrinkPage)}?{nameof(NewDrinkViewModel.ItemId)}={ItemId}");
      
        }

        public async void IncrementValue(Item item)
        {
            item.Ammount++;
            Person person = await DataStore.GetPersonAsync(ItemId);
            await DataStore2.UpdateItemsAsync(item,person);
            await ExecuteLoadItemsCommand();
            CountSum();
        }

        public async void DecrementValue(Item item)
        {
            if (item.Ammount > 0) { 
           item.Ammount--;
            Person person = await DataStore.GetPersonAsync(ItemId);
            await DataStore2.UpdateItemsAsync(item,person);
            await ExecuteLoadItemsCommand();
            CountSum();
            }
        }

        public async void CountSum()
        {
            double tmp = 0.0;

            foreach (Item item in Items)
            {
                tmp = tmp + item.Ammount * Convert.ToDouble(item.Price);
                
            }

            suma_double = tmp;
            Suma = $"Suma: {suma_double} €";



            Person person = await DataStore.GetPersonAsync(ItemId);
            person.Suma = suma_double;
            await DataStore.UpdatePersonAsync(person);
        }


    }
}

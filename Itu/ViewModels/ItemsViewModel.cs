using Itu.ViewModels;
using Itu.Models;
using Itu.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Itu.ViewModels
{

    public class ItemsViewModel : BaseViewModel
    {
        private Person _selectedItem;

        public ObservableCollection<Person> Items { get; }
        public Command LoadItemsCommand { get; }


        public Command<Person> ItemToDelete { get; }
        public Command AddItemCommand { get; }
        public Command<Person> ItemTapped { get; }

        public Command SetBudgetCommand { get; }

        public ItemsViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<Person>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<Person>(OnItemSelected);

            ItemToDelete = new Command<Person>(DeletePerson);

            SetBudgetCommand = new Command(SetBudget);

            AddItemCommand = new Command(OnAddItem);
        }

        private Color color = Color.Black;
        private string suma ;
        public double suma_double = 0.0;
        public bool alerted = false;
        public string Suma
        {
            get => suma;
            set => SetProperty(ref suma, value);
        }
        //--------------------------------------------//

        public double budget = 0.0;
        public double budget2 = 0.0;

        public Color Color
        {
            get => color;
            set
            {
                color = value;
                OnPropertyChanged(nameof(Color));
            }
        }

        public double Budget
        {
            get => budget;

            set => SetProperty(ref budget, value);
        }


        public double Budget2
        {
            get => budget2;

            set => SetProperty(ref budget2, value);
        }

        public void SetBudget()
        {
            Budget2 = budget;
            alerted = false;

        }

        //-----------------------------------------------//

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                System.Collections.Generic.IEnumerable<Person> items = await DataStore.GetPersonAsync(true);
                foreach (Person item in items)
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

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
            CountSum();
       


        }

        public Person SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewItemPage));
        }

        private async void OnItemSelected(Person item)
        {
            if (item == null)
            {
                return;
            }

        
            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
        }


        private async void DeletePerson(Person person)
        {


            await DataStore.DeletePersonAsync(person.Id);
            await ExecuteLoadItemsCommand();
            CountSum();

        }

        public void CountSum()
        {
            double tmp = 0.0;

            foreach (Person item in Items)
            {
                tmp = tmp + item.Suma;

            }

            suma_double = tmp;
            Suma = $"Celková suma: {suma_double} kč";

            if ((suma_double > budget2) && (budget2 != 0.0)) 
            {
                
                Color = Color.Red;
                if (!alerted) 
                { 
                    Shell.Current.DisplayAlert("Pozor!", "Prekročili ste hranicu nastaveného rozpočtu.", "Beriem na vedomie.");
                    alerted = true;
                }
                
            }
            else
            {
                Color = Color.Black;
                alerted = false;
            }

        }
    }
}
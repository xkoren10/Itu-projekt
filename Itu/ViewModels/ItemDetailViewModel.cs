using Itu.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Itu.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ItemDetailViewModel : BaseViewModel
    {
        private string itemId;
        private string text;

        public string Id { get; set; }

        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
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
                LoadPersonId(value);
            }
        }

        public async void LoadPersonId(string itemId)
        {
            try
            {
                var item = await DataStore.GetPersonAsync(itemId);
                Id = item.Id;
                Text = item.Text;

            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}

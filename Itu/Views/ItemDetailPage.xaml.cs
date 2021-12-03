using Itu.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace Itu.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}
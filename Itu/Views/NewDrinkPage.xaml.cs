using Itu.Models;
using Itu.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Itu.Views
{

    public partial class NewDrinkPage : ContentPage
    {
        public Item Item { get; set; }
        public NewDrinkPage()
        {
            InitializeComponent();
            BindingContext = new NewDrinkViewModel();
        }
    }
}
using Itu.ViewModels;
using Itu.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;


/*
 * Pridanie routingu. Autor: Mtej Koreň
 */

namespace Itu
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            Routing.RegisterRoute(nameof(ItemsPage), typeof(ItemsPage));
            Routing.RegisterRoute(nameof(NewDrinkPage), typeof(NewDrinkPage));
        }

    }
}

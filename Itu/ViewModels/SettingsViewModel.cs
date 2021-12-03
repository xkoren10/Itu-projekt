using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Itu.ViewModels
{
    public class SettingsViewModel : ContentView
    {

        public float Budget;
        public List<string> Currency;
        
        public SettingsViewModel()
        {
            Currency = new List<string>
            {
                "Euro",
                "Česká koruna",
                "Rubeľ",
                "Yen"

            };



            Content = new StackLayout
            {


            };
        }
    }
}
using Itu.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Itu.Views
{
   public static class ViewModelLocator
    {

        private static ItemsViewModel _viewmodel = new ItemsViewModel();
        public static ItemsViewModel MainViewModel
        {
            get 
            { 
                return _viewmodel; 
            }
        }
    }
}

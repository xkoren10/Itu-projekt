﻿using Itu.Services;
using Itu.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Itu
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            DependencyService.Register<MockDrinkStore>();

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

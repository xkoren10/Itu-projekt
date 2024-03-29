﻿using Itu.ViewModels;
using Itu.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

/*
View Model pre stránku na pridnie novej osoby. Autor: Marek Tišš
*/

namespace Itu.ViewModels
{
    public class NewItemViewModel : BaseViewModel
    { 
        public string meno;


    public NewItemViewModel()
    {
        SaveCommand = new Command(OnSave, ValidateSave);
        CancelCommand = new Command(OnCancel);
        this.PropertyChanged +=
            (_,__) => SaveCommand.ChangeCanExecute();


    }

    private bool ValidateSave()
    {
            return !string.IsNullOrWhiteSpace(meno);
        }

    public string Meno
    {
        get => meno;
        set => SetProperty(ref meno, value);
    }

    public Command SaveCommand { get; }
    public Command CancelCommand { get; }


    private async void OnCancel()
    {
        await Shell.Current.GoToAsync("..");
    }

    private async void OnSave()
    {
        Person newItem = new Person()
        {
            Id = Guid.NewGuid().ToString(),
            Text = Meno,
            Items = new List<Item>()
        };

          
        await DataStore.AddPersonAsync(newItem);                                   

        await Shell.Current.GoToAsync("..");
    }

    }
}
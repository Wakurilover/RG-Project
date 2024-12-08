
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
//using MauiFireStore.Models;
//using MauiFireStore.services;
using PropertyChanged;
using RandomG.Services;


namespace RandomG.ViewModels
{
    public class RandomGViewModel
    {
        FirestoreService _firestoreService;
        public RandomGViewModel(FirestoreService firestoreService)
        {
            this._firestoreService = firestoreService;
            //this.Refresh();
        }
    }
}

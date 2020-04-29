﻿using System;
using AllinqApp.Managers;
using XamarinViewModels.Interfaces;
using DataModels;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using System.Collections.Generic;

namespace XamarinViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private INavigationService myNavigationService;
        private ApiManager myApiManager;

        public MainViewModel(INavigationService navigationService, ApiManager apiManager) 
        {
            myNavigationService = navigationService;
            myApiManager = apiManager;
            Haspels.Add(new Haspel
            {
                Barcode = "98764531230",
                Status = Enums.EHaspelStatus.IsUsed,
                UsedBy = "Team 2"
            });
            Haspels.Add(new Haspel
            {
                Barcode = "0123456789",
                Status = Enums.EHaspelStatus.Full,
                UsedBy = "-"
            });

            Haspels.CollectionChanged += Haspels_CollectionChanged;

            myApiManager.Initialized += (a, s) => SetHaspels();
            myApiManager.Initialize();
        }
        public string SelectedNavigationItem { get; set; }
        public List<string> NavigationItems { get; set; } = new List<string> { "", "", "" };

        public async void SetHaspels()
        {
            Haspel[] haspels = await myApiManager.GetData();
            Haspels.Clear();
            foreach (Haspel h in haspels)
            {
                Haspels.Add(h);
            }
        }

        private void Haspels_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (INotifyPropertyChanged item in e.OldItems)
                {
                    if (item == null)
                        continue;
                    item.PropertyChanged -= item_PropertyChanged;
                }
            }
            if (e.NewItems != null)
            {
                foreach (INotifyPropertyChanged item in e.NewItems)
                {
                    if (item == null)
                        continue;
                    item.PropertyChanged += item_PropertyChanged;
                }
            }
        }

        private void item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e.PropertyName);
        }

        public ObservableCollection<Haspel> Haspels { get; set; } = new ObservableCollection<Haspel>();
    }
}

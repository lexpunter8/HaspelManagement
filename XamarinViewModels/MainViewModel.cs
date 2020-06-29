using System;
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
                Barcode = "Kan niet met de server verbinden!"
            });

            Haspels.CollectionChanged += Haspels_CollectionChanged;

            myApiManager.Connected += (a, s) => SetHaspels();
            myApiManager.Initialize();

            RefreshCommand = new Command(RefreshCommandExecute);
        }

        private void RefreshCommandExecute(object obj)
        {
            IsRefreshing = true;
            OnPropertyChanged(nameof(IsRefreshing));
            SetHaspels();
            IsRefreshing = false;
            OnPropertyChanged(nameof(IsRefreshing));
        }

        public ICommand RefreshCommand { get; set; }
        public string SelectedNavigationItem { get; set; }
        public List<string> NavigationItems { get; set; } = new List<string> { "", "", "" };

        public async void SetHaspels()
        {
            Haspel[] haspels = await myApiManager.HaspelApiManager.GetData();
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
        public bool IsRefreshing { get; private set; }
    }
}

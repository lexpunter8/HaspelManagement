using System;
using AllinqApp.Managers;
using XamarinViewModels.Interfaces;
using DataModels;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

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
                Barcode = "Barcode",
                Status = Enums.EHaspelStatus.Unkown,
                UsedBy = "User"
            });

            Haspels.CollectionChanged += Haspels_CollectionChanged;

            myApiManager.Initialized += (a, s) => SetHaspels(myApiManager.GetData().Result);
            myApiManager.Initialize();

            ScanCommand = new Command(ScanCommandExecute);
        }

        public ICommand ScanCommand { get; set; }

        public void SetHaspels(Haspel[] haspels)
        {
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
                    item.PropertyChanged -= item_PropertyChanged;
            }
            if (e.NewItems != null)
            {
                foreach (INotifyPropertyChanged item in e.NewItems)
                    item.PropertyChanged += item_PropertyChanged;
            }
        }

        private void item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e.PropertyName);
        }

        public ObservableCollection<Haspel> Haspels { get; set; } = new ObservableCollection<Haspel>();

        private void ScanCommandExecute()
        {
            var scanner = new PartialScannerPageViewModel();
            myNavigationService.NavigateTo(scanner);

            scanner.OnScanResult += (o, scanResult) =>
            {
                myNavigationService.NavigateBack();
                var currentHaspel = Haspels.FirstOrDefault(h => h.Barcode == scanResult.Barcode);
                if (currentHaspel != null)
                {
                    currentHaspel.Status = scanResult.Status;
                    currentHaspel.UsedBy = scanResult.User;

                    OnPropertyChanged(nameof(Haspels));
                    return;
                }
                myApiManager.PostData(new[] { new Haspel
                {
                    Barcode = scanResult.Barcode,
                    Status = scanResult.Status,
                    UsedBy = scanResult.User
                }}).ConfigureAwait(true);
            };
        }
    }
}

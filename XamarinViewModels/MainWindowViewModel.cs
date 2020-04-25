using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using XamarinViewModels.Interfaces;

namespace XamarinViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private NavigationService myNavigationService;

        public MainWindowViewModel(NavigationService navService)
        {
            myNavigationService = navService;
            PropertyChanged += MainWindowViewModel_PropertyChanged;
        }

        public async void NavigateToScannerPage()
        {
            await myNavigationService.NavigateTo(ScannerViewModel);
        }

        private void MainWindowViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            //if (e.PropertyName == nameof(SelectedTab))
            //{

            //}

        }

        public MainViewModel MainViewModel { get; set; }
        public PartialScannerPageViewModel ScannerViewModel { get; set; }

        public void ScannerViewLeft()
        {
            ScannerViewModel = new PartialScannerPageViewModel(myNavigationService);
        }
        //public List<ViewModelBase> NavigationViewModels { get; set; }
        //public ObservableCollection<Page> Tabs { get; set; } = new ObservableCollection<Page>();

        //public Page SelectedTab { get; set; }

    }
}

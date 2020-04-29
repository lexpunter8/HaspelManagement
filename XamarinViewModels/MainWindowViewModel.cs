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

        public MainWindowViewModel(NavigationService navService, MainViewModel mainViewModel, PartialScannerPageViewModel partialScannerPageViewModel)
        {
            myNavigationService = navService;
            MainViewModel = mainViewModel;
            ScannerViewModel = partialScannerPageViewModel;
            ScannerViewModel.ScanResultHandled += (a, b) => MainViewModel.SetHaspels();
        }

        public void TabChanged(string selectedTitle)
        {
            TabBarTitle = selectedTitle;
            OnPropertyChanged(nameof(TabBarTitle));
        }

        public MainViewModel MainViewModel { get; set; }
        public PartialScannerPageViewModel ScannerViewModel { get; set; }
        public string TabBarTitle { get; set; } = "details";

        //public List<ViewModelBase> NavigationViewModels { get; set; }
        //public ObservableCollection<Page> Tabs { get; set; } = new ObservableCollection<Page>();

        //public Page SelectedTab { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using XamarinViewModels.Interfaces;

namespace XamarinViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel(MainViewModel mainViewModel, PartialScannerPageViewModel partialScannerPageViewModel, TeamsManagerViewModel teamsManagerViewModel)
        {
            MainViewModel = mainViewModel;
            ScannerViewModel = partialScannerPageViewModel;
            ScannerViewModel.ScanResultHandled += (a, b) => MainViewModel.SetHaspels();
            TeamsViewModel = teamsManagerViewModel;
        }

        public void TabChanged(string selectedTitle)
        {
            TabBarTitle = selectedTitle;
            OnPropertyChanged(nameof(TabBarTitle));
        }

        public MainViewModel MainViewModel { get; set; }
        public PartialScannerPageViewModel ScannerViewModel { get; set; }
        public TeamsManagerViewModel TeamsViewModel { get; set; }
        public string TabBarTitle { get; set; } = "details";

        //public List<ViewModelBase> NavigationViewModels { get; set; }
        //public ObservableCollection<Page> Tabs { get; set; } = new ObservableCollection<Page>();

        //public Page SelectedTab { get; set; }

    }
}

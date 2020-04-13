using System;
using System.Collections.Generic;
using System.Windows.Input;
using DataModels;
using Xamarin.Forms;
using XamarinViewModels.Interfaces;
using ZXing;
using static DataModels.Enums;

namespace XamarinViewModels
{
    public class PartialScannerPageViewModel  : ViewModelBase
    {
        private INavigationService myNavigationService;
        public PartialScannerPageViewModel(INavigationService navigationService)
        {
            myNavigationService = navigationService;
        }

        public async void HandleScanResult(Result result)
        {
            var handler = new ScannerResultHandlerViewModel(myNavigationService, result);
            handler.OnScanResult += (o, r) => OnScanResult(this, r);
            await myNavigationService.NavigateTo(handler);
            //OnPropertyChanged(nameof(ShowPopup));
            //ScanResult = result;
            //OnPropertyChanged(nameof(ScanResult));

            //OnPropertyChanged(nameof(ScanResult.Text));
        }

        public EventHandler<ScannerResult> OnScanResult;
    }
}

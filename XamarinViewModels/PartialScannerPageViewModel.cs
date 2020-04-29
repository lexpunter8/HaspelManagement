using System;
using System.Collections.Generic;
using System.Windows.Input;
using AllinqApp.Managers;
using DataModels;
using Xamarin.Forms;
using XamarinViewModels.Interfaces;
using ZXing;
using static DataModels.Enums;

namespace XamarinViewModels
{
    public class PartialScannerPageViewModel  : ViewModelBase
    {
        private ApiManager myApiManager;
        private INavigationService myNavigationService;
        private ScannerResultHandlerViewModel myScannerResultHandler;
        public PartialScannerPageViewModel(INavigationService navigationService, ApiManager apiManagger)
        {
            myApiManager = apiManagger;
            myNavigationService = navigationService;
            myScannerResultHandler = new ScannerResultHandlerViewModel(myNavigationService, myApiManager);
            myScannerResultHandler.OnScanResult += (o, r) => HandleHaspelResult(r);
        }

        public async void HandleScanResult(Result result)
        {
            await myNavigationService.NavigateTo(myScannerResultHandler);
            myScannerResultHandler.SetScanResult(result);
            myScannerResultHandler.HandleScanResult(result);
        }

        public EventHandler<ScannerResult> ScanResultHandled;

        private async void HandleHaspelResult(ScannerResult scanResult)
        {
            await myApiManager.PostData(new Haspel
            {
                Barcode = scanResult.Barcode,
                Status = scanResult.Status,
                UsedBy = scanResult.User
            });
            ScanResultHandled?.Invoke(this, scanResult);
        }
    }
}

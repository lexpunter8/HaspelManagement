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
    public class ScannerResultHandlerViewModel : ViewModelBase
    {
        public ScannerResultHandlerViewModel(INavigationService navigationService, Result scannerResult, ApiManager apiManager)
        {
            myApiManager = apiManager;
            HandleScanResult(scannerResult);
            InButtonClickedCommand = new Command(InButtonClickedExecute);
            CancelCommand = new Command(CancelButtonExecute);
            CompleteCommand = new Command(CompleteScan);

            UserOptions = new List<string>
            {
                "een","twee","drie"
            };
            myNavigationService = navigationService;
        }

        public ICommand InButtonClickedCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand CompleteCommand { get; set; }
        public ICommand ScanCommand { get; set; }

        private ApiManager myApiManager;

        private Result ScanResult { get; set; }
        public string SelectedUserOptions { get; set; }
        public List<string> UserOptions { get; set; }
        public Haspel ScannedHaspel { get; set; }
        public string Barcode { get; set; }

        public async void HandleScanResult(Result result)
        {
            try
            {

                ScanResult = result;
                Barcode = ScanResult.Text;
                OnPropertyChanged(nameof(ScanResult));
                OnPropertyChanged(nameof(Barcode));

                OnPropertyChanged(nameof(ScanResult.Text));

                ScannedHaspel = await myApiManager.GetHaspelByBarcode(result.Text);
                OnPropertyChanged(nameof(ScannedHaspel));
            }
            catch (Exception e)
            {
                //
            }
        }

        public EventHandler<ScannerResult> OnScanResult;
        private readonly INavigationService myNavigationService;

        public bool InButtonChecked { get; set; }
        public bool OutButtonChecked { get; set; }

        private void InButtonClickedExecute()
        {
            CompleteScan();
        }

        private async void CancelButtonExecute()
        {
            await myNavigationService.NavigateBackToRoot();
        }

        private async void CompleteScan()
        {
            var status = OutButtonChecked ? EHaspelStatus.IsUsed : EHaspelStatus.Empty;
            OnScanResult?.Invoke(this, new ScannerResult
            {
                Barcode = ScanResult.Text,
                Status = status,
                User = status == EHaspelStatus.IsUsed ? SelectedUserOptions : string.Empty
            });

            await myNavigationService.NavigateBackToRoot();
        }
    }
}

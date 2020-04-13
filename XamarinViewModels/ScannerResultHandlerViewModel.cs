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
    public class ScannerResultHandlerViewModel : ViewModelBase
    {
        public ScannerResultHandlerViewModel(INavigationService navigationService, Result scannerResult)
        {
            ScanResult = scannerResult;
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

        private Result ScanResult { get; set; }
        public string SelectedUserOptions { get; set; }
        public List<string> UserOptions { get; set; }

        public void HandleScanResult(Result result)
        {
            ScanResult = result;
            OnPropertyChanged(nameof(ScanResult));

            OnPropertyChanged(nameof(ScanResult.Text));
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
            var status = OutButtonChecked ? EHaspelStatus.IsUsed : EHaspelStatus.IsNotUsed;
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

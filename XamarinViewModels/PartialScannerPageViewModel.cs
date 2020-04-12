using System;
using System.Collections.Generic;
using System.Windows.Input;
using DataModels;
using Xamarin.Forms;
using ZXing;
using static DataModels.Enums;

namespace XamarinViewModels
{
    public class PartialScannerPageViewModel  : ViewModelBase
    {
        public PartialScannerPageViewModel()
        {
            ShowPopup = false;

            AcceptCommand = new Command<bool>(AcceptButtonCommandExecute);
            CancelCommand = new Command(CancelButtonExecute);

            UserOptions = new List<string>
            {
                "een","twee","drie"
            };
        }
        public ICommand AcceptCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public Result ScanResult { get; set; }
        public bool IsScanning { get; set; }
        public string SelectedUserOptions { get; set; }
        public List<string> UserOptions { get; set; }

        public bool ShowPopup { get; set; }
        public void HandleScanResult(Result result)
        {
            IsScanning = false;
            ShowPopup = true;
            OnPropertyChanged(nameof(ShowPopup));
            ScanResult = result;
            OnPropertyChanged(nameof(ScanResult));

            OnPropertyChanged(nameof(ScanResult.Text));
        }

        public EventHandler<ScannerResult> OnScanResult;

        private void AcceptButtonCommandExecute(bool inUse)
        {
            OnScanResult?.Invoke(this, new ScannerResult
            {
                Barcode = ScanResult.Text,
                Status = inUse ? EHaspelStatus.IsUsed : EHaspelStatus.IsNotUsed,
                User = SelectedUserOptions
            });
        }
    
        private void CancelButtonExecute()
        {
            ShowPopup = false;
            IsScanning = true;
            OnPropertyChanged(nameof(ShowPopup));
        }
    }
}

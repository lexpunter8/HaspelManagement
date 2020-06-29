using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        private Result myCurrentResult;
        private readonly ApiManager myApiManager;
        private readonly INavigationService myNavigationService;

        public ScannerResultHandlerViewModel(INavigationService navigationService, ApiManager apiManager)
        {
            myApiManager = apiManager;
            CancelCommand = new Command(CancelButtonExecute);
            CompleteCommand = new Command(CompleteScan);
            StatusChangedCommand = new Command(ExecuteStatusChangedCommand);

            myApiManager.Connected += (a,b) => SetTeams();

            myNavigationService = navigationService;
        }

        private async void SetTeams()
        {
            var team = await myApiManager.TeamApiManager.GetData();

            UserOptions = team.ToList();
            OnPropertyChanged(nameof(UserOptions));
        }

        public ICommand CancelCommand { get; set; }
        public ICommand CompleteCommand { get; set; }
        public ICommand StatusChangedCommand { get; set; }
        public string SelectedUserOptions { get; set; }
        public List<string> UserOptions { get; set; }
        public Haspel ScannedHaspel { get; set; }
        public string Barcode { get; set; }
        public EventHandler<ScannerResult> OnScanResult;

        public bool InButtonChecked { get; set; }
        public bool OutButtonChecked { get; set; }
        public bool FullButtonChecked { get; set; }
        public bool EmptyButtonChecked { get; set; }
        public bool ShowCompleteButton { get; set; } = true;

        public void SetScanResult(Result result)
        {
            myCurrentResult = result;
            SetTeams(); 
        }

        public async void HandleScanResult(Result result)
        {
            try
            {
                Barcode = result.Text;
                OnPropertyChanged(nameof(Barcode));

                ScannedHaspel = await myApiManager.HaspelApiManager.GetHaspelByBarcode(result.Text);
                OnPropertyChanged(nameof(ScannedHaspel));
                OnPropertyChanged(nameof(ScannedHaspel.StatusText));

                if (ScannedHaspel.Status == EHaspelStatus.IsUsed || ScannedHaspel.Status == EHaspelStatus.Unkown)
                {
                    InButtonChecked = true;
                    OnPropertyChanged(nameof(InButtonChecked));

                    EmptyButtonChecked = ScannedHaspel.Status == EHaspelStatus.IsUsed;
                    FullButtonChecked = ScannedHaspel.Status == EHaspelStatus.Unkown;

                    OnPropertyChanged(nameof(FullButtonChecked));
                    OnPropertyChanged(nameof(EmptyButtonChecked));

                    return;
                }

                OutButtonChecked = true;
                OnPropertyChanged(nameof(OutButtonChecked));
            }
            catch (Exception e)
            {
                //
            }
        }

        private async void CancelButtonExecute()
        {
            await myNavigationService.NavigateBackToRoot();
        }
        private void ExecuteStatusChangedCommand()
        {
            return;
            bool canComplete;
            if (InButtonChecked)
            {
                canComplete = FullButtonChecked || EmptyButtonChecked;
            }
            else
            {
                canComplete = SelectedUserOptions != null;
            }

            ShowCompleteButton = canComplete;
            OnPropertyChanged(nameof(ShowCompleteButton));
        }

        private async void CompleteScan()
        {
            try
            {
                var status = OutButtonChecked ? EHaspelStatus.IsUsed : EHaspelStatus.Unkown;

                if (status == EHaspelStatus.Unkown)
                {
                    status = FullButtonChecked ? EHaspelStatus.Full : EHaspelStatus.Empty;
                }

                var result = new ScannerResult
                {
                    Barcode = myCurrentResult.Text,
                    Status = status,
                    User = status == EHaspelStatus.IsUsed ? SelectedUserOptions : string.Empty
                };
                OnScanResult?.Invoke(this, result);

                await myNavigationService.NavigateBackToRoot();
            } 
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
        }
    }
}

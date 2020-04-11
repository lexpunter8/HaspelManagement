using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing;

namespace Allinq
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PartialSannerPage : ContentPage, INotifyPropertyChanged
    {
        public Result ScanResult;
        public PartialSannerPage()
        {
            InitializeComponent();
            ShowPopup = false;
        }


        public new event PropertyChangedEventHandler PropertyChanged;
        protected override void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public bool ShowPopup { get; set; }
        private void HandleScanResult(Result result)
        {
            ScannerView.IsScanning = false;
            ShowPopup = true;
            OnPropertyChanged(nameof(ShowPopup));
            ScanResult = result;
            OnPropertyChanged(nameof(ScanResult));

            OnPropertyChanged(nameof(ScanResult.Text));
        }

        public EventHandler<ScanResult> OnScanResult;

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ScannerView.IsScanning = true;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ScannerView.IsScanning = false;
        }

        void Button_Clicked(System.Object sender, System.EventArgs e)
        {

            OnScanResult?.Invoke(this, new ScanResult
            {
                Barcode = ScanResult.Text,
                IsInHouse = true,
                User = ploegPicker.SelectedItem as string
            });
            
        }

        void Button_Clicked_1(System.Object sender, System.EventArgs e)
        {
            OnScanResult?.Invoke(this, new ScanResult
            {
                Barcode = ScanResult.Text,
                IsInHouse = false,
                User = string.Empty
            });
        }

        void Button_Clicked_2(System.Object sender, System.EventArgs e)
        {
            ShowPopup = false;
            ScannerView.IsScanning = true;
            OnPropertyChanged(nameof(ShowPopup));
        }
    }
    public class ScanResult
    {
        public string Barcode { get; set; }
        public bool IsInHouse { get; set; }
        public string User { get; set; }
    }
}
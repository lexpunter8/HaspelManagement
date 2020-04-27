using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;
using XamarinViewModels;
using ZXing;
using ZXing.Mobile;

namespace AllinqApp
{
    public partial class PartialScannerView : ContentPage
    {
        public PartialScannerView()
        {
            InitializeComponent();
            //var opt = new ZXing.Mobile.MobileBarcodeScanningOptions()
            //{
            //    PossibleFormats = new List<BarcodeFormat> { BarcodeFormat.QR_CODE },
            //    CameraResolutionSelector = SelectLowestResolutionMatchingDisplayAspectRatio

            //};

            //ScannerView.Options = opt;
        }

        private bool _isScanning = true;

        protected override void OnAppearing()
        {
            ScannerView.IsScanning = true;
            ScannerView.IsAnalyzing = true;
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            //ScannerView.IsScanning = false;
        }

        private void HandleScanResult(Result result)
        {
            ScannerView.IsAnalyzing = false;
            //Device.BeginInvokeOnMainThread(() => {
            //    ScannerView.IsAnalyzing = false; ScannerView.IsScanning = false;
            //});
            var dataContext = (PartialScannerPageViewModel)BindingContext;
            dataContext.HandleScanResult(result);
                
        }
    }
}

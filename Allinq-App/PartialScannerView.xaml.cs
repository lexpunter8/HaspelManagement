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
        public CameraResolution SelectLowestResolutionMatchingDisplayAspectRatio(List<CameraResolution> availableResolutions)
        {
            CameraResolution result = null;

            //a tolerance of 0.1 should not be visible to the user
            double aspectTolerance = 0.1;
            var displayOrientationHeight = DeviceDisplay.MainDisplayInfo.Orientation == DisplayOrientation.Portrait ? DeviceDisplay.MainDisplayInfo.Height : DeviceDisplay.MainDisplayInfo.Width;
            var displayOrientationWidth = DeviceDisplay.MainDisplayInfo.Orientation == DisplayOrientation.Portrait ? DeviceDisplay.MainDisplayInfo.Width : DeviceDisplay.MainDisplayInfo.Height;

            //calculatiing our targetRatio
            var targetRatio = displayOrientationHeight / displayOrientationWidth;
            var targetHeight = displayOrientationHeight;
            var minDiff = double.MaxValue;

            //camera API lists all available resolutions from highest to lowest, perfect for us
            //making use of this sorting, following code runs some comparisons to select the lowest resolution that matches the screen aspect ratio and lies within tolerance
            //selecting the lowest makes Qr detection actual faster most of the time
            foreach (var r in availableResolutions.Where(r => Math.Abs(((double)r.Width / r.Height) - targetRatio) < aspectTolerance))
            {
                //slowly going down the list to the lowest matching solution with the correct aspect ratio
                if (Math.Abs(r.Height - targetHeight) < minDiff)
                    minDiff = Math.Abs(r.Height - targetHeight);
                result = r;
            }

            return result;
        }
    }
}

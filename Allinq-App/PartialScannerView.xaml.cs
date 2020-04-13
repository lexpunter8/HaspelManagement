using System;
using System.Collections.Generic;

using Xamarin.Forms;
using XamarinViewModels;
using ZXing;

namespace AllinqApp
{
    public partial class PartialScannerView : ContentPage
    {
        public PartialScannerView()
        {
            InitializeComponent();
        }
        private bool _isScanning = true;

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ScannerView.IsScanning = true;
            _isScanning = true;
            ScannerView.IsAnalyzing = true;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ScannerView.IsScanning = false;
            ScannerView.IsAnalyzing = false;
        }

        private void HandleScanResult(Result result)
        {

            //Device.BeginInvokeOnMainThread(() => {
            //    ScannerView.IsAnalyzing = false; ScannerView.IsScanning = false;
            //});
            var dataContext = (PartialScannerPageViewModel)BindingContext;
            dataContext.HandleScanResult(result);
                
        }

        //void CheckButton_Clicked(object sender, EventArgs e)
        //{
        //    //ScannerView.IsScanning = true;
        //    _isScanning = true;
        //    Device.BeginInvokeOnMainThread(() => { 
        //    ScannerView.IsAnalyzing = true;ScannerView.IsScanning = true; });
        //    //OnPropertyChanged(nameof(ScannerView.IsScanning));
        //}
    }
}

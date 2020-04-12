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

        private void HandleScanResult(Result result)
        {
            var dataContext = (PartialScannerPageViewModel)BindingContext;
            dataContext.HandleScanResult(result);
                
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing;
using ZXing.Net.Mobile.Forms;

namespace Allinq
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FullScreenScanning : ZXingScannerPage
    {
        public FullScreenScanning()
        {
            InitializeComponent();
        }

        public void Handle_OnScanResult(Result result)
        {
            Device.BeginInvokeOnMainThread(async () => { await DisplayAlert("Scanned Result", result.Text, "OK"); });
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            IsScanning = true;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            IsScanning = false;
        }
    }
}
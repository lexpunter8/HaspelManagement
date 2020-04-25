using System;
using System.Collections.Generic;

using Xamarin.Forms;
using XamarinViewModels;

namespace AllinqApp
{
    public partial class PlaceholderView : ContentPage
    {
        private NavigationService myNavigationService;

        public PlaceholderView()
        {
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {
            var dataContext = BindingContext as MainWindowViewModel;
            dataContext.NavigateToScannerPage();
            base.OnAppearing();
        }
    }
}

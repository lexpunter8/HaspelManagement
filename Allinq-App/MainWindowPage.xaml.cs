using System;
using System.Collections.Generic;

using Xamarin.Forms;
using XamarinViewModels;

namespace AllinqApp
{
    public partial class MainWindowPage : TabbedPage
    {
        public MainWindowPage()
        {
            InitializeComponent();

        }
        Page myPreviousPage;
        void TabbedPage_CurrentPageChanged(Object sender, System.EventArgs e)
        {
            var dataContext = BindingContext as MainWindowViewModel;
            //dataContext.TabChanged()
            MainWindowPage s = sender as MainWindowPage;
            if (myPreviousPage?.GetType() == typeof(PartialScannerView))
            {
                dataContext.ScannerViewLeft();
            }
            myPreviousPage = CurrentPage;
        }
    }
}

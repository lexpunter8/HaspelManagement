using Allinq;
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

        void TabbedPage_CurrentPageChanged(Object sender, System.EventArgs e)
        {
            var dataContext = BindingContext as MainWindowViewModel;
            if (dataContext == null)
            {
                return;
            }
            //dataContext.TabChanged()
            var c = (TabbedPage)sender;
            var s = c.CurrentPage;
            if (s.GetType() == typeof(MainView))
            {
                dataContext.TabChanged("Details");
            }

            if (s.GetType() == typeof(PartialScannerView))
            {
                dataContext.TabChanged("Scan");
            }

        }
    }
}

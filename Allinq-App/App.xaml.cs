using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Allinq;
using AllinqApp.Managers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Allinq_App
{
    public partial class App : Application
    {
        private ApiManager myApiManager;
        private PartialSannerPage myScannerPage;
        private DetailsView myDetailsView;
        public App()
        {
            InitializeComponent();

            myDetailsView = new DetailsView();
            myScannerPage = new PartialSannerPage();

            var mainPage = new TabbedPage();
            // mainPage.Children.Add(new FullScreenScanning());
            mainPage.Children.Add(myScannerPage);
            mainPage.Children.Add(myDetailsView);

            MainPage = new NavigationPage(new MainPage(new ApiManager()));
        }
        protected override void OnStart()
        {

        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

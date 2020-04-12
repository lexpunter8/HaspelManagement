using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Allinq;
using AllinqApp;
using AllinqApp.Managers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinViewModels;
using XamarinViewModels.Interfaces;

namespace Allinq_App
{
    public partial class App : Application, IHaveMainPage
    {
        private ApiManager myApiManager;
        private PartialScannerView myScannerPage;
        private DetailsView myDetailsView;

        Page IHaveMainPage.MainPage
        {
            get => MainPage;
            set => MainPage = value;
        }

        public App()
        {
            InitializeComponent();

            myDetailsView = new DetailsView();
            myScannerPage = new PartialScannerView();

            var mainPage = new TabbedPage();
            // mainPage.Children.Add(new FullScreenScanning());
            mainPage.Children.Add(myScannerPage);
            mainPage.Children.Add(myDetailsView);

            var viewLocator = new ViewLocator();

            viewLocator.AddMapping<MainViewModel, MainView>();
            viewLocator.AddMapping<PartialScannerPageViewModel, PartialScannerView>();

            var navigationService = new NavigationService(this, viewLocator);
            var mainViewModel = new MainViewModel(navigationService, new ApiManager());
            navigationService.PresentAsNavigatableMainPage(mainViewModel);
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

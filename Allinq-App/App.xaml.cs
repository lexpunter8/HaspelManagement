using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Allinq;
using AllinqApp;
using AllinqApp.Managers;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
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
            myApiManager = new ApiManager();

            var viewLocator = new ViewLocator();

            viewLocator.AddMapping<MainWindowViewModel, MainWindowPage>();
            viewLocator.AddMapping<MainViewModel, MainView>();
            viewLocator.AddMapping<PartialScannerPageViewModel, FullScreenScanning>();
            viewLocator.AddMapping<ScannerResultHandlerViewModel, ScannerResultHandlerView>();

            var navigationService = new NavigationService(this, viewLocator);
            var mainViewModel = new MainViewModel(navigationService, myApiManager);

            var main = new MainWindowViewModel(navigationService);
            main.ScannerViewModel = new PartialScannerPageViewModel(navigationService, myApiManager);
            main.MainViewModel = mainViewModel;

            //var tabbed = new MainWindowPage();
            //tabbed.BindingContext = main;
            //tabbed.Children.Add(viewLocator.CreateAndBindPageFor(main));
            //tabbed.Children.Add(viewLocator.CreateAndBindPageFor(new PartialScannerPageViewModel(navigationService)));
            //tabbed.Children.Add(viewLocator.CreateAndBindPageFor(main));

            //MainPage = tabbed;
            navigationService.PresentAsNavigatableMainPage(main);
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

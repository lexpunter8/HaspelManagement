using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Allinq;
using Xamarin.Forms;
using DataModels;
using ZXing.Net.Mobile.Forms;
using AllinqApp.Managers;
using AllinqApp;

namespace Allinq_App
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage, INotifyPropertyChanged
    {
        private ApiManager myApiManager;

        public new event PropertyChangedEventHandler PropertyChanged;
        protected override void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public MainPage(ApiManager manager)
        {
            InitializeComponent();

            myApiManager = manager;
            Haspels.Add(new Haspel
            {
                Barcode = "Barcode",
                Status = Enums.EHaspelStatus.Unkown,
                UsedBy = "User"
            });

            Haspels.CollectionChanged += Haspels_CollectionChanged;

            myApiManager.Initialized += (a, s) => SetHaspels(myApiManager.HaspelApiManager.GetData().Result);
            myApiManager.Initialize();
        }

        public void SetHaspels(Haspel[] haspels)
        {
            Haspels.Clear();
            foreach(Haspel h in haspels)
            {
                Haspels.Add(h);
            }
        }

        private void Haspels_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (INotifyPropertyChanged item in e.OldItems)
                    item.PropertyChanged -= item_PropertyChanged;
            }
            if (e.NewItems != null)
            {
                foreach (INotifyPropertyChanged item in e.NewItems)
                    item.PropertyChanged += item_PropertyChanged;
            }
        }

        private void item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e.PropertyName);
        }

        public ObservableCollection<Haspel> Haspels { get; set; } = new ObservableCollection<Haspel>();

    }
}

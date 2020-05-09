using AllinqApp.Managers;
using DataModels;
using Infrastructure.Managers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace XamarinViewModels
{
    public class TeamsManagerViewModel : ViewModelBase
    {

        private TeamApiManager myTeamApiManager;
        public TeamsManagerViewModel(ApiManager teamApiManager)
        {
            Teams.Add(new Team
            {
                Name = "twee"
            });

            Teams.CollectionChanged += Haspels_CollectionChanged;

            teamApiManager.Initialized += (e, a) => GetTeams();

            myTeamApiManager = teamApiManager.TeamApiManager;

            GetTeams();

            RefreshCommand = new Command(RefreshCommandExecute);
        }

        private void RefreshCommandExecute(object obj)
        {
            IsRefreshing = true;
            OnPropertyChanged(nameof(IsRefreshing));
            GetTeams();
            IsRefreshing = false;
            OnPropertyChanged(nameof(IsRefreshing));

        }

        public ICommand RefreshCommand { get; set; }
        public bool IsRefreshing { get; set; }

        private async void GetTeams()
        {
            string[] teams = await myTeamApiManager.GetData();
            foreach (var t in teams)
            {
                Teams.Add(new Team
                {
                    Name = t
                });
            }
        }

        private void Haspels_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (INotifyPropertyChanged item in e.OldItems)
                {
                    if (item == null)
                        continue;
                    item.PropertyChanged -= item_PropertyChanged;
                }
            }
            if (e.NewItems != null)
            {
                foreach (INotifyPropertyChanged item in e.NewItems)
                {
                    if (item == null)
                        continue;
                    item.PropertyChanged += item_PropertyChanged;
                }
            }
        }

        private void item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e.PropertyName);
        }


        public ObservableCollection<Team> Teams { get; set; } = new ObservableCollection<Team>
            {
                new Team
                {
                    Name = "test"
                }
            };
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DataModels
{
    public class Team : INotifyPropertyChanged
    {
        private string myName;

        public string Name
        {
            get { return myName; }
            set { myName = value; OnPropertyChanged(nameof(Name)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

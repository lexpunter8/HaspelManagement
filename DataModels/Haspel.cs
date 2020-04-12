using System.ComponentModel;
using static DataModels.Enums;

namespace DataModels
{
    public class Haspel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private string myBarcode;
        private EHaspelStatus myStatus;
        private string myUsedBy;
        private string myComment;

        public string Barcode
        {
            get { return myBarcode; }
            set { myBarcode = value; OnPropertyChanged(nameof(Barcode)); }
        }
        public EHaspelStatus Status
        {
            get { return myStatus; }
            set { myStatus = value; OnPropertyChanged(nameof(Status)); }
        }
        public string UsedBy
        {
            get { return myUsedBy; }
            set { myUsedBy = value; OnPropertyChanged(nameof(UsedBy)); }
        }
        public string Comment
        {
            get { return myComment; }
            set { myComment = value; OnPropertyChanged(nameof(Comment)); }
        }

        public string ToCsvString()
        {
            return $"{Barcode},{UsedBy},{Status},{Comment}";
        }
    }
}

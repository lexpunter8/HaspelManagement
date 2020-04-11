using System.ComponentModel;

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
        private bool myIsInUse;
        private string myUsedBy;
        private string myComment;

        public string Barcode
        {
            get { return myBarcode; }
            set { myBarcode = value; OnPropertyChanged(nameof(Barcode)); }
        }
        public bool IsInUse
        {
            get { return myIsInUse; }
            set { myIsInUse = value; OnPropertyChanged(nameof(IsInUse)); }
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
            return $"{Barcode},{UsedBy},{IsInUse},{Comment}";
        }
    }
}

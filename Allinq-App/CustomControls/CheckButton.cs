using System;
using Xamarin.Forms;

namespace AllinqApp.CustomControls
{
    public class CheckButton : Button
    {

        public CheckButton()
        {
            Clicked += CheckButton_Clicked;
        }

        private void CheckButton_Clicked(object sender, EventArgs e)
        {
            IsChecked = !IsChecked;
            if (IsChecked)
            {
                BorderColor = CheckBorderColor;
            }
            else
            {
                BorderColor = BackgroundColor;
            }
        }

        public bool IsChecked { get; set; }
        public Color CheckBorderColor { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace AllinqApp.CustomControls
{
    public class CheckButtonGroup : Grid
    {
        public CheckButtonGroup()
        {
            ChildAdded += CheckButtonGroup_ChildAdded;
        }
        private List<CheckButton> checkButtons = new List<CheckButton>();

        private void CheckButtonGroup_ChildAdded(object sender, ElementEventArgs e)
        {
            if (e.Element is CheckButton)
            {
                var button = (CheckButton)e.Element;
                button.Clicked += CheckButtonClicked;
                checkButtons.Add(button);
            }
        }

        private void CheckButtonClicked(object sender, EventArgs e)
        {
            var s = (CheckButton)sender;

            foreach(CheckButton cb in checkButtons)
            {
                if (cb == s)
                {
                    continue;
                }

                cb.IsChecked = false;
            }
            s.IsChecked = true;
        }
    }

    public class TextCheckbutton : CheckButton
    {
        protected override void IsCheckedChanged()
        {
            if (!CanBeChecked)
            {
                TextColor = DefaultTextColor;
                return;
            }
            if (IsChecked)
            {
                TextColor = CheckBorderColor;
                return;
            }
            TextColor = DefaultTextColor;
        }
        public Color DefaultTextColor { get; set; }
        public bool CanBeChecked { get; set; } = true;
    }

    public class CheckButton : Button
    {
        public static readonly BindableProperty IsCheckedProperty =
                                BindableProperty.Create("IsChecked", typeof(bool), typeof(CheckButton), null);

        public CheckButton()
        {
        }


        public bool IsChecked
        {
            get { return (bool)GetValue(IsCheckedProperty); }
            set {
                SetValue(IsCheckedProperty, value);
                IsCheckedChanged();
            }
        }

        protected virtual void IsCheckedChanged()
        {
            if (IsChecked)
            {
                BorderColor = CheckBorderColor;
                return;
            }
            BorderColor = BackgroundColor;
        }

        public Color CheckBorderColor { get; set; }

    }
}

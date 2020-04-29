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
                button.PropertyChanged += Button_PropertyChanged;
                checkButtons.Add(button);
            }
        }

        private void Button_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var button = sender as CheckButton;
            if (e.PropertyName == nameof(CheckButton.IsChecked) && button.IsChecked)
            {
                UpdateChilderen(button);
            }
        }

        private void CheckButtonClicked(object sender, EventArgs e)
        {
            UpdateChilderen(sender as CheckButton);
        }

        public void UpdateChilderen(CheckButton button)
        {
            foreach (CheckButton cb in checkButtons)
            {
                if (cb == button)
                {
                    continue;
                }

                if (cb.IsChecked)
                {
                    cb.SetToFalse();
                }
            }
            button.SetToTrue();
        }
    }

    public class CheckButton : Button
    {
        public static readonly BindableProperty IsCheckedProperty =
                                BindableProperty.Create(nameof(IsChecked), typeof(bool), typeof(CheckButton), default(bool), BindingMode.TwoWay);

        public bool IsChecked
        {
            get { return (bool)GetValue(IsCheckedProperty); }
            set
            {
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

        internal void SetToFalse()
        {
            SetValue(IsCheckedProperty, false);
            IsCheckedChanged();
        }

        internal void SetToTrue()
        {
            SetValue(IsCheckedProperty, true);
            IsCheckedChanged();
        }

        public Color CheckBorderColor { get; set; }
    }
}

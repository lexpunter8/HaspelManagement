using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace AllinqApp.CustomControls
{
    public partial class IconTextLabel : ContentView
    {
        public static readonly BindableProperty TextProperty =
                            BindableProperty.Create(nameof(Text), typeof(string),
                                typeof(IconTextLabel), default(string), BindingMode.TwoWay);
        public static readonly BindableProperty IconFontFamilyProperty =
                            BindableProperty.Create(nameof(IconFontFamiy), typeof(string),
                                typeof(IconTextLabel), default(string), BindingMode.TwoWay);
        public static readonly BindableProperty IconProperty =
                            BindableProperty.Create(nameof(Icon), typeof(string),
                                typeof(IconTextLabel), default(string), BindingMode.TwoWay);

        public IconTextLabel()
        {
            InitializeComponent();
        }

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }
        public string Icon
        {
            get => (string)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }
        public string IconFontFamiy
        {
            get => (string)GetValue(IconFontFamilyProperty);
            set => SetValue(IconFontFamilyProperty, value);
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == TextProperty.PropertyName)
            {
                text.Text = Text;
            }
            else if (propertyName == IconProperty.PropertyName)
            {
                iconLabel.Text = Icon;
            }
        }
    }
}

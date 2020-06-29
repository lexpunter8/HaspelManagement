using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinViewModels;

namespace AllinqApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TeamsPage : ContentPage
    {
        private ViewCell lastCell;

        public TeamsPage()
        {
            InitializeComponent();
        }

        private void MenuItem_Clicked(object sender, EventArgs e)
        {

        }

        private void ViewCell_Tapped(object sender, EventArgs e)
        {

        }

        private async void Button_Clicked(object sender, EventArgs e)
        {

            string result = await DisplayPromptAsync("Voeg team toe", "Name");
            var datacontext = BindingContext as TeamsManagerViewModel;

            if (string.IsNullOrWhiteSpace(result))
            {
                return;
            }

            datacontext.AddTeam(result);
        }
    }
}
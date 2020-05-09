using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
            //if (lastCell != null)
            //    lastCell.View.BackgroundColor = Color.Transparent;
            //var viewCell = (ViewCell)sender;
            //if (viewCell.View != null)
            //{
            //    viewCell.View.BackgroundColor = Color.Red;
            //    lastCell = viewCell;
            //}
        }
    }
}
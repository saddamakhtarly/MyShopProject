using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyShop.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : Shell
    {
        public HomePage()
        {
            InitializeComponent();
            
        }

        private async void MenuItem_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//views:FrontPage");
        }

        private async void MenuItem_Clicked_1(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//views:ProductsPage");
        }
    }
}
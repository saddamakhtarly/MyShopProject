using MyShop.Models;
using MyShop.ViewModels;
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
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
                      
        }
        protected override void OnAppearing()
        {
            BindingContext = new SettingsPageViewModel(Navigation);
            if (GlobalVariables.user_role == 1)
            {
                itemCreationStack.IsVisible = true;
            }
        }
    }
}
using MyShop.Models;
using MyShop.Views;
using MyShopCommonLib;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MyShop.ViewModels
{
    public class SettingsPageViewModel : NotifyModel
    {
        public INavigation Navigation { get; set; }
        public SettingsPageViewModel(INavigation navigation)
        {
            Navigation = navigation;
        }
        public Command CreateCategory
        {
            get
            {
                return new Command(() =>
                {
                    Navigation.PushAsync(new CreateCategory());
                });
            }
        }
        public Command CreateProduct
        {
            get
            {
                return new Command(() =>
                {
                    Navigation.PushAsync(new CreateProduct());
                });
            }
        }
        public Command Logout
        {
            get
            {
                return new Command(() =>
                {
                    GlobalVariables.user = null;
                    GlobalVariables.username = "";
                    Preferences.Set("Username", "");

                    Application.Current.MainPage = new LoginPage();
                });
            }
        }
        public Command OrderHistory
        {
            get
            {
                return new Command(() =>
                {
                    Navigation.PushAsync(new OrderHistoryPage());
                });
            }
        }
        
    }
}

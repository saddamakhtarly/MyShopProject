using MyShop.Interfaces;
using MyShop.Models;
using MyShop.Views;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyShop
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            string username = Preferences.Get("Username", "");
            GlobalVariables.user_role = Preferences.Get("UserRole", 0);
            GlobalVariables.user_id = Preferences.Get("UserId", 0);

            if (string.IsNullOrEmpty(username))
            {
                MainPage = new NavigationPage(new LoginPage());
            }
            else
            {

                MainPage = new HomePage();
                GlobalVariables.username = username;
            }

            // MainPage =new NavigationPage( new ApiLoginPage());


            //GlobalVariables.conn = DependencyService.Get<ISQLIte>().GetConnection();

        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

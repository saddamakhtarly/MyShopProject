using MyShop.Models;
using MyShop.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MyShop.ViewModels
{
   public class ApiLoginPageViewModel:NotifyModel
    {
        public INavigation Navigation { get; set; }
        public ApiLoginPageViewModel(INavigation navigation)
        {
            Navigation = navigation;
        }
        public Command LoginClicked
        {
            get
            {
                return new Command(async () =>
                {

                    var resp = await new ApiFunctions().apilogin(UserName,Password);
                    if (!string.IsNullOrEmpty(resp.access_token))
                    {
                        GlobalVariables.apilogin = resp;
                        await Navigation.PushAsync(new ApiProductPage());
                        //await Navigation.PushAsync(new FrontPage());
                    }
                    else
                    {
                       await Application.Current.MainPage.DisplayAlert("Message", "Invalid Login", "Ok");
                    }

                });
            }

        }
        public Command SignupClicked
        {
            get
            {
                return new Command(() =>
                {
                   // Navigation.PushAsync(new SignupPage());
                });

            }
        }
        public Command ForgotPasswordClicked
        {
            get
            {
                return new Command(() =>
                {
                    // Navigation.PushAsync(new ResetPasswordPage());
                });

            }
        }
        private string _username;
        public string UserName
        {
            get { return _username; }
            set { _username = value; OnPropertyChanged(); }
        }
        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(); }
        }


    }
}

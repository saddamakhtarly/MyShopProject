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
    public class LoginViewModel : NotifyModel
    {
        public INavigation Navigation { get; set; }
        public LoginViewModel(INavigation navigation)
        {
            Navigation = navigation;
        }

        public Command LoginClicked
        {
            get
            {
                return new Command(async () =>
                {
                    if (!string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password))
                    {
                        LoginRequest rqst = new LoginRequest();
                        rqst.Username = UserName;
                        rqst.Password = Password;
                        var resp = await new GlobalFunctions().ValidateUser(rqst);
                        GlobalVariables.user = resp;
                        if (resp.IsValid)
                        {
                            Preferences.Set("Username", resp.FullName);
                            Preferences.Set("UserRole", resp.Role);
                            Preferences.Set("UserId", resp.Id);
                           
                            GlobalVariables.username = GlobalVariables.user.FullName;
                            GlobalVariables.user_role = GlobalVariables.user.Role;
                            GlobalVariables.user_id = GlobalVariables.user.Id;
                            Application.Current.MainPage = new HomePage();
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert("Message", "Invalid Login", "Ok");
                        }
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Message", "Please provide valid credential", "Ok");
                    }


                });
            }
        }
        public Command SignupClicked
        {
            get
            {
                return new Command(async() =>
                {
                   await Navigation.PushAsync(new SignupPage());
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
        string _username;
        public string UserName
        {
            get
            {
                return _username;
            }
            set
            {
                if (value != null)
                {
                    _username = value;
                    OnPropertyChanged();
                }
            }
        }

        string _password;
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                if (value != null)
                {
                    _password = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}

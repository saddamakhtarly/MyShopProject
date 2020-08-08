using MyShop.Models;
using MyShopCommonLib;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MyShop.ViewModels
{
    public class SignupPageViewModel : NotifyModel
    {
        public INavigation Navigation { get; set; }
        public SignupPageViewModel(INavigation navigation)
        {
            Navigation = navigation;
        }
        public Command SubmitClicked
        {
            get
            {
                return new Command(async () =>
                {
                    if (!string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password) && Password == ConfirmPassword)
                    {
                        tblUser user = new tblUser
                        {
                            Email = Email,
                            FullName = Name,
                            Mobile = Mobile,
                            Password = Password
                        };
                        var resp = await new GlobalFunctions().RegisterUser(user);
                        if (resp.IsValid)
                        {
                            // success
                            await Application.Current.MainPage.DisplayAlert("Message", "Registration Successfully Completed", "OK");
                            await Navigation.PopAsync();
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert("Message", "Registration Failed", "OK");
                        }
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Message", "Please enter valid Credential", "Ok");
                    }
                });
            }
        }
        public Command BackClicked
        {
            get
            {
                return new Command(() =>
                {
                    Navigation.PopAsync();
                });
            }
        }
        string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (value != null)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }
        string _email;
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                if (value != null)
                {
                    _email = value;
                    OnPropertyChanged();
                }
            }
        }
        string _mobile;
        public string Mobile
        {
            get
            {
                return _mobile;
            }
            set
            {
                if (value != null)
                {
                    _mobile = value;
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
        string _confirmpassword;
        public string ConfirmPassword
        {
            get
            {
                return _confirmpassword;
            }
            set
            {
                if (value != null)
                {
                    _confirmpassword = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}

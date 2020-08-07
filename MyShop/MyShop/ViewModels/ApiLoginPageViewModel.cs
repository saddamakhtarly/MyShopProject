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
        public Command LoginCommand
        {
            get
            {
                return new Command(async () =>
                {

                    var resp = await new ApiFunctions().apilogin(UserName,Password);
                    GlobalVariables.apilogin = resp;
                    await Navigation.PushAsync(new ApiProductPage());

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

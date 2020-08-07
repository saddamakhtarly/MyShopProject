using MyShop.Models;
using MyShop.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MyShop.ViewModels
{
   public class ApiProductPageViewModel:NotifyModel
    {
        public INavigation Navigation { get; set; }
        public ApiProductPageViewModel(INavigation navigation)
        {
            Navigation = navigation;
        }

        public Command ShowProduct
        {
            get
            {
                return new Command(async () =>
                {

                    var resp = await new ApiFunctions().apiproduct(GlobalVariables.apilogin.access_token);
                    if (resp!=null)
                    {
                        await Navigation.PushAsync(new AboutPage());
                    }
                    
                });
            }

        }


    }
}

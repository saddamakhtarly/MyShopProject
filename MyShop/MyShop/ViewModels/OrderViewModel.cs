using MyShop.Models;
using MyShopCommonLib;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MyShop.ViewModels
{
    public class OrderViewModel : NotifyModel
    {
        INavigation Navigation;
        public OrderViewModel(INavigation navigation)
        {
            Navigation = navigation;

            MessagingCenter.Unsubscribe<SaveAddressResponce>(this, "AddressId");

            MessagingCenter.Subscribe<SaveAddressResponce>(this, "AddressId", (sender) =>
            {
                // Do something whenever the "AddressId" message is received
                var Address = sender.AddressId;

            });
        }
        public Command RazerPaybtnClicked
        {
            get
            {
                return new Command(async () =>
                {
                    // await Navigation.PushAsync(new OrderPage());


                });
            }
        }
        public Command CodbtnClicked
        {
            get
            {
                return new Command(async () =>
                {
                    //  await Navigation.PushAsync(new OrderPage());
                });
            }
        }
        public Command ConfirmOrder
        {
            get
            {
                return new Command(async () =>
                {
                    // await Navigation.PushAsync(new OrderPage());
                });
            }
        }

    }
}

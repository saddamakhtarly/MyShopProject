using MyShop.Models;
using MyShopCommonLib;
using Xamarin.Forms;

namespace MyShop.ViewModels
{
    public class OrderViewModel : NotifyModel
    {
        public INavigation Navigation { get; set; }
        Address Selected_Address = new Address();
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

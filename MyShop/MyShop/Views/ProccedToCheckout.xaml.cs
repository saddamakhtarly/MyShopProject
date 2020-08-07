using MyShop.ViewModels;
using MyShopCommonLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyShop.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProccedToCheckout : ContentPage
    {
        public ProccedToCheckout(string totalqty, string totalprice,ObservableCollection<CartItem> Items)
        {
            InitializeComponent();
            BindingContext = new CheckoutViewModel(Navigation, totalqty, totalprice, Items);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            //int i = CheckoutViewModel.GetQty();
            //CheckoutViewModel.Totalqty = Convert.ToString(i);
            //double price = CheckoutViewModel.GetPrice();
            //CheckoutViewModel.TotalPrice = Convert.ToString(price);
        }
        public void PlaceOrder(object sender, EventArgs e)
        {

            if (btn1.Image == "radiobtn.png" || btn2.Image == "radiobtn.png" || btn3.Image == "radiobtn.png" || btn4.Image == "radiobtn.png")
            {

                Navigation.PushAsync(new OrderPage());

            }
            else
            {
                // UserDialogs.Instance.Alert("Please Select payment option", null, "Ok");
            }
        }
        public void btn1Clicked(object sender, EventArgs e)
        {
            btn1.Image = "radiobtn.png";
            btn2.Image = "radiobtnunchecked.png";
            btn3.Image = "radiobtnunchecked.png";
            btn4.Image = "radiobtnunchecked.png";
        }
        public void btn2Clicked(object sender, EventArgs e)
        {
            btn1.Image = "radiobtnunchecked.png";
            btn2.Image = "radiobtn.png";
            btn3.Image = "radiobtnunchecked.png";
            btn4.Image = "radiobtnunchecked.png";
        }

        public void btn3Clicked(object sender, EventArgs e)
        {
            btn1.Image = "radiobtnunchecked.png";
            btn2.Image = "radiobtnunchecked.png";
            btn3.Image = "radiobtn.png";
            btn4.Image = "radiobtnunchecked.png";
        }

        public void btn4Clicked(object sender, EventArgs e)
        {
            btn1.Image = "radiobtnunchecked.png";
            btn2.Image = "radiobtnunchecked.png";
            btn3.Image = "radiobtnunchecked.png";
            btn4.Image = "radiobtn.png";
        }
    }
}
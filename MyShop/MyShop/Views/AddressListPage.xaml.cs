using MyShop.Models;
using MyShop.ViewModels;
using MyShopCommonLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyShop.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddressListPage : ContentPage
    {
        public AddressListPage()
        {
            InitializeComponent();
            
            
        }
        protected override void OnAppearing()
        {
            BindingContext = new AddressListPageViewModel(Navigation);
        }


        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ShippingAddressPage());
        }

        private void addresses_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Address selectedAddress = e.Item as Address;
            MessagingCenter.Send<Address>(selectedAddress, "SelectedAddress");
            Navigation.PopAsync();
        }
    }
}
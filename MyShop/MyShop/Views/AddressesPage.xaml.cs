using MyShop.Models;
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
    public partial class AddressesPage : ContentPage
    {
        public AddressesPage()
        {
            InitializeComponent();
            PopulateAddress();
        }

        private async void PopulateAddress()
        {
            var data = await new GlobalFunctions().GetAddress(GlobalVariables.user_id);
            addresses.ItemsSource = data.Addresses;
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
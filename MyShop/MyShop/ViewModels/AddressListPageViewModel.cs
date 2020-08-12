using MyShop.Models;
using MyShop.Views;
using MyShopCommonLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace MyShop.ViewModels
{
  public  class AddressListPageViewModel: NotifyModel
    {
        public INavigation Navigation { get; set; }
        public AddressListPageViewModel(INavigation navigation)
        {
            Navigation = navigation;
            PopulateAddress();
        }


        private async void PopulateAddress( )
        {
            Addresses = new ObservableCollection<Address>();

            var data = await new GlobalFunctions().GetAddress(GlobalVariables.user_id);
            Addresses = new ObservableCollection<Address>(data.Address as List<Address>);
        }

        public Command AddressPage
        {
            get
            {
                return new Command(async () =>
                {
                 await   Navigation.PushAsync(new ShippingAddressPage());
                });
            }
        }
        ObservableCollection<Address> _addresses;
        public ObservableCollection<Address> Addresses
        {
            get { return _addresses; }
            set
            {
                _addresses = value;
                OnPropertyChanged();
            }
        }
        Address _selectedAddress;
        public Address SelectedAddress
        {
            get { return _selectedAddress; }
            set
            {
                if (value != null)
                {
                    // Navigation.PushAsync(new CheckOutPage());
                    
                    _selectedAddress = value;
                    OnPropertyChanged();
                    MessagingCenter.Send<Address>(value, "SelectedAddress");
                    Navigation.PopAsync();
                }
            }
        }
    }
}

using MyShop.Models;
using MyShop.Views;
using MyShopCommonLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace MyShop.ViewModels
{
    public class CheckOutPageViewModel : NotifyModel
    {
        public INavigation Navigation { get; set; }
        Address Selected_Address=new Address();
        ObservableCollection<CartItem> CartItems;
        public CheckOutPageViewModel(INavigation navigation, string totalqty, string totalprice, ObservableCollection<CartItem> Items)
        {
            Navigation = navigation;
            Totalqty = totalqty;
            CartItems = Items;
            TotalPrice = totalprice;
        }
        public Command AddAddress
        {
            get
            {
                return new Command(async () =>
                {
                    MessagingCenter.Unsubscribe<Address>(this, "SelectedAddress");
                    MessagingCenter.Subscribe<Address>(this, "SelectedAddress", (value) =>
                    {
                        FullName = value.FullName;
                        City = value.CityName;
                        MobileNumber = value.MobileNumber;
                        Street = value.StreetNo;
                        HouseNo = value.HouseNo;
                        Selected_Address = value;          
                    });
                    await Navigation.PushAsync(new AddressListPage());
                });
            }
        }
        public Command PlaceOrder
        {
            get
            {
                return new Command(async () =>
                {
                    Order order = new Order();
                    order.Items = new List<CartItem>();
                    order.UserId = GlobalVariables.user_id;
                    order.AddressId = Selected_Address.Id;
                   

                    order.OrderAmount = CartItems.Sum(x => x.SalePrice * x.Quantity);
                    order.PaidAmount = 0;
                    order.Discount = 0;
                    order.ShippingCharge = 0;
                    order.PaymentType = 1;
                    order.Items = CartItems.ToList();
                    
                    var resp = await new GlobalFunctions().CreateOrder(order);
                    await Navigation.PushAsync(new OrderPage());
                });
            }
        }
        private bool CheckValidations()
        {
            if (string.IsNullOrWhiteSpace(FullName))
            {
                Application.Current.MainPage.DisplayAlert("Message", "Please enter FullName", "Cancel");
                return false;
            }
            else if (PinCode <= 0)
            {
                Application.Current.MainPage.DisplayAlert("Message", "Please enter PinCode", "Cancel");
                return false;
            }
            else if (string.IsNullOrWhiteSpace(City))
            {
                Application.Current.MainPage.DisplayAlert("Message", "Please enter City", "Cancel");
                return false;
            }

            else if (StateId <= 0)
            {
                Application.Current.MainPage.DisplayAlert("Message", "Please enter State", "Cancel");
                return false;
            }

            else
            {
                return true;
            }
        }
        string _totalqty;
        public string Totalqty
        {
            get { return _totalqty; }
            set
            {
                if (value != null)
                {
                    _totalqty = value;
                    OnPropertyChanged();
                }
            }
        }
        string _totalPrice;
        public string TotalPrice
        {
            get { return _totalPrice; }
            set
            {
                if (value != null)
                {
                    _totalPrice = value;
                    OnPropertyChanged();
                }
            }
        }
        int _countryId;
        public int CountryId
        {
            get { return _countryId; }
            set
            {
                    _countryId = value;
                    OnPropertyChanged();
                
            }
        }
        string _city;
        public string City
        {
            get { return _city; }
            set
            {
                if (value != null)
                {
                    _city = value;
                    OnPropertyChanged();
                }
            }
        }
        int _stateId;
        public int StateId
        {
            get { return _stateId; }
            set
            {
                _stateId = value;
                OnPropertyChanged();
            }
        }
        string _fullName;
        public string FullName
        {
            get { return _fullName; }
            set
            {
                if (value != null)
                {
                    _fullName = value;
                    OnPropertyChanged();
                }
            }
        }
        string _mobileNumber;
        public string MobileNumber
        {
            get { return _mobileNumber; }
            set
            {
                if (value != null)
                {
                    _mobileNumber = value;
                    OnPropertyChanged();
                }
            }
        }
        int _pinCode;
        public int PinCode
        {
            get
            {
                return _pinCode;
            }
            set
            {

                _pinCode = value;
                OnPropertyChanged();

            }
        }
        string _street;
        public string Street
        {
            get { return _street; }
            set
            {
                if (value != null)
                {
                    _street = value;
                    OnPropertyChanged();
                }
            }
        }
        string _houseNo;
        public string HouseNo
        {
            get { return _houseNo; }
            set
            {
                if (value != null)
                {
                    _houseNo = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}

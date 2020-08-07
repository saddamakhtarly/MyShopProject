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
    public class CheckoutViewModel : NotifyModel
    {
        INavigation Navigation;
        Address addresess;
        Address Selected_Address;
        ///string TotalPrice;
        ObservableCollection<CartItem> CartItems;
        public CheckoutViewModel(INavigation navigation, string totalqty, string totalprice, ObservableCollection<CartItem> Items)
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
                        State = value.State.ToString();
                        Country = value.Country.ToString();
                        Selected_Address = value;
                    });
                    await Navigation.PushAsync(new AddressesPage());


                    
                    //}
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
                    order.UserId = GlobalVariables.user_id;

                    order.ShipmentAddress.Area = Selected_Address.Area;
                    order.ShipmentAddress.City = Selected_Address.City;
                    order.ShipmentAddress.Country = Selected_Address.Country;
                    order.ShipmentAddress.Description = "";
                    order.ShipmentAddress.FullName = Selected_Address.FullName;
                    order.ShipmentAddress.HouseNo = Selected_Address.HouseNo;
                    order.ShipmentAddress.Landmark = Selected_Address.Landmark;
                    order.ShipmentAddress.MobileNumber = Selected_Address.MobileNumber;
                    order.ShipmentAddress.PinCode = Selected_Address.PinCode;
                    order.ShipmentAddress.State = Selected_Address.State;
                    order.ShipmentAddress.StreetNo = Selected_Address.StreetNo;

                    order.PaidAmount = 0;
                    order.PaymentType = 1;
                    order.OrderAmount = CartItems.Sum(x=>x.SalePrice * x.Quantity);
                    order.ShippingCharge = 0;
                    order.Items = CartItems.ToList();
                    order.Discount = 0;

                    var resp = await new GlobalFunctions().CreateOrder(order);
                    //await Navigation.PushAsync(new OrderPage());
                });
            }
        }


        public async void ExecutePlaceOrderCommand()
        {
            // UserDialogs.Instance.ShowLoading();
            //foreach (var item in cartList)
            //{
            //    item.Qty = 1;
            //}
            //Rest_Response rest_result = await WebService.PostData(cartList, "Product/PlaceOrder");
            //if (rest_result != null)
            //{
            //    if (rest_result.status_code == 200)
            //    {
            //        await GetProduct();
            //        RootObjectProduct data = JsonConvert.DeserializeObject<RootObjectProduct>(rest_result.response_body);
            //        if (data.StatusCode == 200)
            //        {
            //            CrossLocalNotifications.Current.Show("ByMe", "Your Order has Been Placed worth Rs." + GetPrice());
            //            cartList = new ObservableCollection<ProductModel>();
            //        }

            //    }
            //    else
            //    {
            //        UserDialogs.Instance.Alert("User Already registered");
            //    }

            //}
            //UserDialogs.Instance.HideLoading();


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

            else if (string.IsNullOrWhiteSpace(State))
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

        string _country;
        public string Country
        {
            get { return _country; }
            set
            {
                if (value != null)
                {
                    _country = value;
                    OnPropertyChanged();
                }
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
        string _state;
        public string State
        {
            get { return _state; }
            set
            {
                if (value != null)
                {
                    _state = value;
                    OnPropertyChanged();
                }
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
    }
}

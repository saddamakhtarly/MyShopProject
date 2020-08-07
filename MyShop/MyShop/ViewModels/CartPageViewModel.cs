using MyShop.DatabaseHandler;
using MyShop.Models;
using MyShop.Views;
using MyShopCommonLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace MyShop.ViewModels
{
    public class CartPageViewModel : NotifyModel
    {
        INavigation Navigation;
        public CartPageViewModel(INavigation navigation)
        {
            Navigation = navigation;
            PopulateCartItem();
        }
        private async void PopulateCartItem()
        {
            var data = await new GlobalFunctions().GetCart(GlobalVariables.user_id);
            foreach (var item in data.cartItems)
            {
                item.ThumbnailURL = $"{GlobalVariables.serviceURL}{item.ThumbnailURL}";
            }
            CartItem = new ObservableCollection<CartItem>(data.cartItems);
            if (CartItem != null)
            {
                CalculateTotalQty();
            }
        }

        private void CalculateTotalQty()
        {
            Totalqty = CartItem.Count.ToString();
            TotalPrice = CartItem.Sum(x => x.SalePrice * x.Quantity).ToString("F");
        }
                

        public void AddCartCount()
        {
            var CartCount = CartItem.Count;
        }
        
        public Command DeleteCommand
        {
            get
            {
                return new Command<CartItem>(item =>
                {
                    CartItem.Remove(item);
                    CalculateTotalQty();
                    // need to delete item form api,pending
                    Application.Current.MainPage.DisplayAlert("Deleted from cart", "Success", "OK");
                });
            }

        }
        public Command DecreaseQuantity
        {
            get
            {
                return new Command((e) =>
                {
                    CartItem selectedItem = e as CartItem;
                    if (selectedItem != null)
                    {
                        selectedItem.Quantity -= 1;
                        if (selectedItem.Quantity <= 0)
                        {
                            CartItem.Remove(selectedItem);
                        }
                        //new DatabaseFunction().UpdateCart(selectedItem);
                        // need to update it to api,pending
                        CalculateTotalQty();
                    }
                });
            }
        }
        public Command IncreaseQuantity
        {
            get
            {
                return new Command((e) =>
                {
                    CartItem selectedItem = e as CartItem;
                    if (selectedItem != null)
                    {
                        selectedItem.Quantity += 1;
                        //new DatabaseFunction().UpdateCart(selectedItem);
                        // need to update it to api,pending
                    }
                    CalculateTotalQty();
                });
            }
        }

        public Command Proceed
        {
            get
            {
                return new Command(() =>
                {
                    Navigation.PushAsync(new ProccedToCheckout(Totalqty, TotalPrice, CartItem));
                });
            }
        }
        
        ObservableCollection<CartItem> _cartItem;
        public ObservableCollection<CartItem> CartItem
        {
            get { return _cartItem; }
            set
            {
                _cartItem = value;
                OnPropertyChanged();
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

        string _imageURL;
        public string ImageURL
        {
            get { return _imageURL; }
            set
            {
                if (value != null)
                {
                    _imageURL = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}

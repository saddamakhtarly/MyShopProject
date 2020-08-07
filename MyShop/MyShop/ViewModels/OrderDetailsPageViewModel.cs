using MyShop.Models;
using MyShopCommonLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MyShop.ViewModels
{
    public class OrderDetailsPageViewModel : NotifyModel
    {
        public OrderDetailsPageViewModel(int orderId)
        {
            PopulateOrderDetails(orderId);
        }

        private async void PopulateOrderDetails(int orderId)
        {
            var orderDetails = await new GlobalFunctions().GetOrderDetails(orderId);
            OrderItems = new ObservableCollection<OrderItem>(orderDetails.OrderItems as List<OrderItem>);

            OrderTotal = orderDetails.OrderAmount.ToString("F");
            ShippingCharge = orderDetails.ShippingCharge.ToString("F");
            DiscountedAmount = orderDetails.Discount.ToString("F");
        }
        ObservableCollection<OrderItem> _orderItems;
        public ObservableCollection<OrderItem> OrderItems
        {
            get { return _orderItems; }
            set
            {
                _orderItems = value;
                OnPropertyChanged();
            }
        }
        string _orderTotal;
        public string OrderTotal
        {
            get { return _orderTotal; }
            set
            {
                _orderTotal = value;
                OnPropertyChanged();
            }
        }
        string _shippingCharge;
        public string ShippingCharge
        {
            get { return _shippingCharge; }
            set
            {
                _shippingCharge = value;
                OnPropertyChanged();
            }
        }
        string _discountedAmount;
        public string DiscountedAmount
        {
            get { return _discountedAmount; }
            set
            {
                _discountedAmount = value;
                OnPropertyChanged();
            }
        }
    }
}

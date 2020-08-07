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
    public class OrderHistoryPageViewModel : NotifyModel
    {
        public INavigation Navigation { get; set; }
        public OrderHistoryPageViewModel(INavigation navigation)
        {
            Navigation = navigation;
            PopulateOrders();
        }

        private async void PopulateOrders()
        {
            var data = await new GlobalFunctions().GetOrders(GlobalVariables.user_id);
            Orders = new ObservableCollection<OrderHistory>(data.Orders as List<OrderHistory>);
        }

        ObservableCollection<OrderHistory> _orders;
        public ObservableCollection<OrderHistory> Orders
        {
            get { return _orders; }
            set
            {
                _orders = value;
                OnPropertyChanged();
            }
        }
        OrderHistory _selectedOrder;
        public OrderHistory SelectedOrder
        {
            get { return _selectedOrder; }
            set
            {
                _selectedOrder = value;
                if (value != null)
                {
                    Navigation.PushAsync(new OrderDetailsPage(value.Id));
                }
                OnPropertyChanged();
               
            }
        }
    }
    
}

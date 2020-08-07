using MyShop.Models;
using MyShop.Views;
using MyShopCommonLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MyShop.ViewModels
{
    public class ProductViewModel : NotifyModel
    {
        public INavigation Navigation { get; set; }
       
        public ProductViewModel(INavigation navigation)
        {
            Navigation = navigation; 
            PopulateProduct();
        }

        private async void PopulateProduct(int categoryId = 0)
        {
            var resp = await new GlobalFunctions().GetProducts(categoryId);

            Products = new ObservableCollection<Product>();
            foreach (var item in resp.Products)
            {
                item.ThumbnailURL = $"{GlobalVariables.serviceURL}{item.ThumbnailURL}";
            }
            Products = new ObservableCollection<Product>(resp.Products as List<Product>);
        }
        ObservableCollection<Product> _productData;
        ObservableCollection<Product> ProductData
        {
            get { return _productData; }
            set
            {
                _productData = value;
                OnPropertyChanged();
            }
        }
        Product _selectedProduct;
        public Product SelectedProduct
        {
            get { return _selectedProduct; }
            set
            {
                if (value != null)
                {
                    Navigation.PushAsync(new ProductDetailsPage(value));
                    _selectedProduct = value;
                    OnPropertyChanged();
                }
            }
        }
        ObservableCollection<Product> _products;
        public ObservableCollection<Product> Products
        {
            get { return _products; }
            set
            {
                _products = value;
                OnPropertyChanged();
            }
        }
    }
}

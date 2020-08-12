using MyShop.DatabaseHandler;
using MyShop.Models;
using MyShopCommonLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace MyShop.ViewModels
{
    public class ProductDetailsPageViewModel : NotifyModel
    {
        public INavigation Navigation { get; set; }
        Product SelectedProduct;
        public ProductDetailsPageViewModel(INavigation navigation,Product selectedProduct)
        {
            Navigation = navigation;
            SelectedProduct = selectedProduct;
            PopulateProductDetails(selectedProduct);
            PopulateImages();
            
        }

        private async void PopulateImages()
        {
            
            var resp = await new GlobalFunctions().GetProductImage(SelectedProduct.Id);
            Images = new ObservableCollection<ProductImage>();
            foreach (var item in resp.ProductImages)
            {
                item.ImageURL = $"{GlobalVariables.serviceURL}{item.ImageURL}";
            }
            Images = new ObservableCollection<ProductImage>(resp.ProductImages as List<ProductImage>);
        }
        ObservableCollection<ProductImage> _images;
        public ObservableCollection<ProductImage> Images
        {
            get { return _images; }
            set
            {
                _images = value;
                OnPropertyChanged();
            }
        }

        private void PopulateProductDetails(Product selectedProduct)
        {
            ProductTitle = selectedProduct.Name;
            Description = selectedProduct.Description;
            Price = $"${selectedProduct.SalePrice.ToString()}";
            
            
        }

        public Command AddToCart
        {
            get
            {
                return new Command(async () =>
                {
                    if (Quantity <= 0)
                    {
                        Quantity = 1;
                    }
                    Cart cart = new Cart();
                    cart.Id = 0;
                    cart.ProductId = SelectedProduct.Id;
                    cart.Quantity = Quantity;
                    cart.UserId = GlobalVariables.user_id;
                    var resp = await new GlobalFunctions().SaveCart(cart);
                    if (resp.IsValid)
                    {
                      await  Application.Current.MainPage.DisplayAlert("Message", "Item Add To Cart", "Ok");
                        await Navigation.PopAsync();
                    }
                    else
                    {
                       await Application.Current.MainPage.DisplayAlert("Message", "Item Failed To Add", "Ok");
                    }
                });
            }
        }

        public Command BuyNow
        {
            get
            {
                return new Command(() =>
                {
                    //
                });
            }
        }

        string _productTitle;
        public string ProductTitle
        {
            get { return _productTitle; }
            set
            {
                _productTitle = value;
                OnPropertyChanged();
            }
        }
        string _price;
        public string Price
        {
            get { return _price; }
            set
            {
                _price = value;
                OnPropertyChanged();
            }
        }
        string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }
        int _quantity;
        public int Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;
                OnPropertyChanged();
            }
        }
    }
}

using MyShop.Models;
using MyShop.Views;
using MyShopCommonLib;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace MyShop.ViewModels
{
    public class CreateProductViewModel : NotifyModel
    {
        List<MediaFile> ProductImages;
        public INavigation Navigation { get; set; }
        public CreateProductViewModel(INavigation navigation)
        {
            Navigation = navigation;
            PopulateCategories();
            ProductImages = new List<MediaFile>();

            MessagingCenter.Unsubscribe<MediaFile>(this, "ProductImage");
            MessagingCenter.Subscribe<MediaFile>(this, "ProductImage", (media) =>
              {
                  ProductImages.Add(media);
              });
        }

        private async void PopulateCategories()
        {
            var resp = await new GlobalFunctions().GetCategories();
            Categories = resp.CategoryList;
            //Categories = new List<Category>();
            //Categories = GlobalVariables.conn.Query<Category>("SELECT * FROM Category");
        }
        public Command UploadImage
        {
            get
            {
                return new Command(() =>
                {
                    Navigation.PushAsync(new UploadProductImagePage());
                });
            }
        }
        public Command SaveProduct
        {
            get
            {
                return new Command(async() =>
                {
                    var res = ValidateForm();
                    if (res.Item1)
                    {
                        Product product = new Product();
                        product.CategoryId = SelectedCategory.Id;
                        product.Name = ProductName;
                        product.Description = Description;
                        product.MRP = Price;
                        product.SalePrice = Price;

                        //int resp = GlobalVariables.conn.Insert(product);
                        var resp = await new GlobalFunctions().SaveProduct(product);
                        if (resp.IsValid)
                        {
                            var imageResp = await new GlobalFunctions().SaveProductImages(resp.ProductId, ProductImages);
                            
                            //foreach (var item in ProductImages)
                            //{
                            //    item.ProductId = resp;
                            //}
                            //IEnumerable<ProductImage> productImages = ProductImages.AsEnumerable();
                            //GlobalVariables.conn.InsertAll(productImages);
                            //// save image urls
                            //Application.Current.MainPage.DisplayAlert("Message", "Product Saved", "Ok");
                            //SelectedCategory = null;
                            //ProductName = "";
                            //Description = "";
                            //Price = 0;
                        }
                        else
                        {
                            Application.Current.MainPage.DisplayAlert("Message", "Failed to Save", "Ok");
                        }
                    }
                    else
                    {
                        Application.Current.MainPage.DisplayAlert("Message", res.Item2, "Ok");
                    }
                });
            }
        }

        public Tuple<bool,string> ValidateForm()
        {
            bool resp = true;
            string respStr = "";
            if (string.IsNullOrEmpty(ProductName))
            {
                resp = false;
                respStr = "Please enter product name";
            }
            else if (SelectedCategory == null)
            {
                resp = false;
                respStr = "Please select Category";
            }

            return Tuple.Create(resp, respStr);
        }

        string _productName;
        public string ProductName
        {
            get { return _productName; }
            set
            {
                if (value != null)
                {
                    _productName = value;
                    OnPropertyChanged();
                }
            }
        }
        string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                if (value != null)
                {
                    _description = value;
                    OnPropertyChanged();
                }
            }
        }
        List<Category> _categories;
        public List<Category> Categories
        {
            get { return _categories; }
            set
            {
                if (value != null)
                {
                    _categories = value;
                    OnPropertyChanged();
                }
            }
        }
        Category _selectedCategory;
        public Category SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                if (value != null)
                {
                    _selectedCategory = value;
                    OnPropertyChanged();
                }
            }
        }

        decimal _price;
        public decimal Price
        {
            get { return _price; }
            set
            {
                _price = value;
                OnPropertyChanged();
            }
        }
    }
}

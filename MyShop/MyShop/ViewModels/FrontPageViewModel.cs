using MyShop.Models;
using MyShop.Views;
using MyShopCommonLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Xamarin.Forms;

namespace MyShop.ViewModels
{
    public class FrontPageViewModel : NotifyModel
    {
        public INavigation Navigation { get; set; }
        public FrontPageViewModel(INavigation navigation)
        {
            PopulateSlider();
            Navigation = navigation;
            PopulateCategory();
            PopulateProducts();
        }
        private async void PopulateCategory()
        {
            var resp = await new GlobalFunctions().GetCategories();
            CategoryData = resp.CategoryList;
        }
        private async void PopulateProducts(int categoryId = 0)
        {
            var resp = await new GlobalFunctions().GetProducts(categoryId);

            Products = new ObservableCollection<Product>();
            foreach (var item in resp.Products)
            {
                item.ThumbnailURL = $"{GlobalVariables.serviceURL}{item.ThumbnailURL}";
            }
            Products = new ObservableCollection<Product>(resp.Products as List<Product>);
        }
        private void PopulateSlider()
        {
            SliderImages = new List<ImageSlider>();
            SliderImages.Add(new ImageSlider { URL = "https://www.cameraegg.org/wp-content/uploads/2013/08/AF-S-DX-NIKKOR-18-140mm-f-3.5-5.6G-ED-VR-sample-images-1.jpg" });
            SliderImages.Add(new ImageSlider { URL = "https://live.staticflickr.com/4561/38054606355_26429c884f_b.jpg" });
            SliderImages.Add(new ImageSlider { URL = "https://www.alphashooters.com/wp-content/uploads/2019/04/sony-a6400-sample-image-butterfly-DSC07365.jpg" });
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
        List<ImageSlider> _sliderImages;
        public List<ImageSlider> SliderImages
        {
            get { return _sliderImages; }
            set
            {
                _sliderImages = value;
                OnPropertyChanged();
            }
        }
        List<Category> _categoryData;
        public List<Category> CategoryData
        {
            get { return _categoryData; }
            set
            {
                _categoryData = value;
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
        Category _selectedCategory;
        public Category SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                if (value != null)
                {                    
                    _selectedCategory = value;
                    PopulateProducts(SelectedCategory.Id);
                    OnPropertyChanged();
                }
            }
        }

        //new data
        string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                if (value != null)
                {
                    _searchText = value;
                    OnPropertyChanged();
                }
            }
        }
        bool _isVisibleStatus;
        public bool IsVisibleStatus
        {
            get { return _isVisibleStatus; }
            set
            {
                _isVisibleStatus = value;
                OnPropertyChanged();
            }
        }
        public Command SearchData
        {
            get
            {
                return new Command(async() =>
                {
                    if (!string.IsNullOrEmpty(SearchText))
                    {
                        //List<Product> searchData = Products.Where(x => x.Name.ToLower().Contains(SearchText.ToLower())).ToList();
                        //Products = new ObservableCollection<Product>(searchData as List<Product>);
                        var resp= await new GlobalFunctions().SearchProducts(SearchText);
                        Products = new ObservableCollection<Product>(resp.Products as List<Product>);
                        if (Products.Count() == 0)
                        {
                            IsVisibleStatus = true;
                        }
                        else
                        {
                            IsVisibleStatus = false;
                        }
                    }
                    else
                    {
                        PopulateProducts();
                        IsVisibleStatus = false;
                    }
                });
            }
        }
        public Command SearchBtnPress
        {
            get
            {
                return new Command(async() =>
                {
                    if (!string.IsNullOrEmpty(SearchText))
                    {
                        //List<Product> searchData = Products.Where(x => x.Name.ToLower().Contains(SearchText.ToLower())).ToList();
                        //Products = new ObservableCollection<Product>(searchData as List<Product>);
                        var resp = await new GlobalFunctions().SearchProducts(SearchText);
                        Products = new ObservableCollection<Product>(resp.Products as List<Product>);
                        if (Products.Count() == 0)
                        {
                            IsVisibleStatus = true;
                        }
                        else
                        {
                            IsVisibleStatus = false;
                        }
                    }
                    else
                    {
                        PopulateProducts();
                        IsVisibleStatus = false;
                    }
                });
            }
        }
    }
}

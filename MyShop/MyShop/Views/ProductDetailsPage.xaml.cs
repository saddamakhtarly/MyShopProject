using MyShop.Models;
using MyShop.ViewModels;
using MyShopCommonLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyShop.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductDetailsPage : ContentPage
    {
        public ProductDetailsPage(Product selectedProduct)
        {
            InitializeComponent();
            BindingContext = new ProductDetailsPageViewModel(Navigation, selectedProduct);
        }
    }
}
using MyShop.ViewModels;
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
    public partial class ShippingAddressPage : ContentPage
    {
        public ShippingAddressPage()
        {
            InitializeComponent();
            BindingContext = new ShippingAddressViewModel(Navigation);
        }
    }
}
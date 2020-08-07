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
    public partial class FrontPage : ContentPage
    {
        public FrontPage()
        {
            InitializeComponent();
            Device.StartTimer(TimeSpan.FromSeconds(3), () =>
             {
                 slider.Position = (slider.Position + 1) % 3;
                 return true;
             });
        }
        protected override void OnAppearing()
        {
            BindingContext = new FrontPageViewModel(Navigation);
        }
    }
}
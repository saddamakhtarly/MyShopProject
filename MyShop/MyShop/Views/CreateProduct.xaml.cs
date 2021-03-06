﻿using MyShop.ViewModels;
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
    public partial class CreateProduct : ContentPage
    {
        public CreateProduct()
        {
            InitializeComponent();
            BindingContext = new CreateProductViewModel(Navigation);
        }
    }
}
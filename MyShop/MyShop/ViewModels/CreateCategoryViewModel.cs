using MyShop.Models;
using MyShopCommonLib;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MyShop.ViewModels
{
    public class CreateCategoryViewModel : NotifyModel
    {
        public INavigation Navigation { get; set; }
        public CreateCategoryViewModel(INavigation navigation)
        {
            Navigation = navigation;
        }
        public Command SaveCategory
        {
            get
            {
                return new Command(async() =>
                {
                    if (!string.IsNullOrEmpty(CategoryName))
                    {
                        Category category = new Category();
                        category.Name = CategoryName;
                        //int resp = GlobalVariables.conn.Insert(category);
                        var resp = await new GlobalFunctions().SaveCategory(category);
                        if (resp.IsValid)
                        {
                            // success
                          await  Application.Current.MainPage.DisplayAlert("Message", "Category Saved Successfully", "OK");
                            CategoryName = "";
                            await Navigation.PopAsync();
                        }
                        else
                        {
                            // failed
                           await Application.Current.MainPage.DisplayAlert("Message", "Category Failed To Save", "OK");
                        }
                    }
                    else
                    {
                       await Application.Current.MainPage.DisplayAlert("Message", "Please enter category name", "OK");
                    }
                });
            }
        }

        string _categoryName;
        public string CategoryName
        {
            get { return _categoryName; }
            set
            {
                if (value != null)
                {
                    _categoryName = value;
                    OnPropertyChanged();
                }
            }
        }
    }

}

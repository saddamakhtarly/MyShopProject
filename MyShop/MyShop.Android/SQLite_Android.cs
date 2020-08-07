using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MyShop.Droid;
using MyShop.Interfaces;
using MyShop.Models;
using SQLite;

[assembly: Xamarin.Forms.Dependency(typeof(SQLite_Android))]
namespace MyShop.Droid
{
    public class SQLite_Android : ISQLIte
    {
        public SQLiteConnection GetConnection()
        {
            string db = "dbMyShop.db3";
            string documentPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string path = Path.Combine(documentPath, db);
            SQLiteConnection con = new SQLiteConnection(path);

           //con.CreateTable<Category>();
           // con.CreateTable<Product>();
            con.CreateTable<CartData>();
          //  con.CreateTable<ProductImage>();

            return con;
        }
    }
}
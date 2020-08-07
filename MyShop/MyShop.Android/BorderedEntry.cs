using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MyShop;
using MyShop.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly:ExportRenderer(typeof(BorderEntry),typeof(BorderedEntry))]
namespace MyShop.Droid
{
    public class BorderedEntry : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                this.Control.SetTextColor(Android.Graphics.Color.Black);
                this.Control.SetBackgroundColor(Android.Graphics.Color.Transparent);
                this.Control.SetPadding(15, 15, 15, 15);

                GradientDrawable gd = new GradientDrawable();
                gd.SetCornerRadius(5);
                gd.SetColor(Android.Graphics.Color.Transparent);
                gd.SetStroke(2, Android.Graphics.Color.Gray);

                Control.SetBackgroundDrawable(gd);
            }
        }
    }
}
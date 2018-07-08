using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Stock6.CustomForms;
using Stock6.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomImage), typeof(CustomImageRenderer))]
namespace Stock6.Droid
{
   
    public class CustomImageRenderer : ImageRenderer
    {
        public CustomImageRenderer(Context context) : base(context)
        {
        }
        protected override void OnDetachedFromWindow()
        {
            //BitmapDrawable bitmapDrawable = (BitmapDrawable)Control.Drawable;
            //Bitmap bitmap = bitmapDrawable.Bitmap;
            //if (bitmap != null && !bitmap.IsRecycled)
            //{
            //    bitmap.Recycle();
            //}
            base.OnDetachedFromWindow();

        }
    }
}
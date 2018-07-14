using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Com.Facebook.Drawee.Backends.Pipeline;
using Stock6.Droid.Services;
using Xamarin.Forms;

namespace Stock6.Droid
{
    public class Platform
    {
        public static Context Context { get; set; }

        public static void Init(Context context)
        {
            Context = context;

            Fresco.Initialize(Context);

            DependencyService.Register<PhotoBrowserImplementation>();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using Stock6.iOS.Services;
using UIKit;
using Xamarin.Forms;

namespace Stock6.iOS
{
   public class Platform
    {
        public static void Init()
        {
            DependencyService.Register<PhotoBrowserImplementation>();
        }
    }
}
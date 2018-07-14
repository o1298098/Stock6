using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Com.Stfalcon.Frescoimageviewer;
using Stock6.Droid.Services;
using Stock6.Services;
using Stock6.ViewModels;
using Xamarin.Forms.Platform.Android;


namespace Stock6.Droid.Services
{
   public class PhotoBrowserImplementation : IPhotoViewer
    {
        protected static ImageViewer _imageViewer;

        public void Show(PhotoBrowserModel photoBrowser)
        {
            ImageViewer.Builder builder = new ImageViewer.Builder(Platform.Context, photoBrowser.Photos.Select(x => x.Path).ToArray());
            ImageOverlayView overlay = new ImageOverlayView(Platform.Context, photoBrowser);
            builder.SetOverlayView(overlay);
            builder.SetImageChangeListener(overlay);
            builder.SetStartPosition(photoBrowser.StartIndex);
            _imageViewer = builder.Show();
        }

        public void Close()
        {
            if (_imageViewer != null)
            {
                _imageViewer.OnDismiss();
                _imageViewer = null;
            }
        }
    }
}
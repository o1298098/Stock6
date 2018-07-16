using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using Stock6.Services;
using Stock6.ViewModels;
using UIKit;

namespace Stock6.iOS.Services
{
    public class PhotoBrowserImplementation : IPhotoViewer
    {
        protected static MyMWPhotoBrower _mainBrowser;

        public void Show(PhotoBrowserModel photoBrowser)
        {
            _mainBrowser = new MyMWPhotoBrower(photoBrowser);
            _mainBrowser.Show();
        }

        public void Close()
        {
            if (_mainBrowser != null)
            {
                _mainBrowser.Close();
                _mainBrowser = null;
            }
        }
    }
}
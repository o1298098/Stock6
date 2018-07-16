using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using Ricardo.LibMWPhotoBrowser.iOS;
using Stock6.Models;
using Stock6.ViewModels;
using UIKit;

namespace Stock6.iOS
{
    public class MyMWPhotoBrower : MWPhotoBrowserDelegate
    {
        protected PhotoBrowserModel _photoBrowser;

        protected List<MWPhoto> _photos = new List<MWPhoto>();

        public MyMWPhotoBrower(PhotoBrowserModel photoBrowser)
        {
            _photoBrowser = photoBrowser;
        }

        public void Show()
        {
            _photos = new List<MWPhoto>();

            foreach (ImageModel p in _photoBrowser.Photos)
            {
                MWPhoto mp = MWPhoto.FromUrl(new Foundation.NSUrl(p.Path));

                if (!string.IsNullOrWhiteSpace(p.Name))
                {
                    mp.Caption = p.Name;
                }

                _photos.Add(mp);
            }

            MWPhotoBrowser browser = new MWPhotoBrowser(this);

            browser.DisplayActionButton = _photoBrowser.ActionButtonPressed != null;
            browser.SetCurrentPhoto((nuint)_photoBrowser.StartIndex);
            browser.EnableGrid = _photoBrowser.EnableGrid;

            UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(new UINavigationController(browser), true, null);
        }

        public override MWPhoto GetPhoto(MWPhotoBrowser photoBrowser, nuint index) => _photos[(int)index];

        public override nuint NumberOfPhotosInPhotoBrowser(MWPhotoBrowser photoBrowser) => (nuint)_photos.Count;

        public override void OnActionButtonPressed(MWPhotoBrowser photoBrowser, nuint index)
        {
            _photoBrowser.ActionButtonPressed?.Invoke((int)index);
        }

        public void Close()
        {
            UIApplication.SharedApplication.KeyWindow.RootViewController.DismissViewController(true, null);
        }
    }
}
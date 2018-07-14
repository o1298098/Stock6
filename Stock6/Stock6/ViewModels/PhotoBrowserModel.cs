using Stock6.Models;
using Stock6.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Stock6.ViewModels
{
   public class PhotoBrowserModel
    {
        public List<ImageModel> Photos { get; set; }

        public Action<int> ActionButtonPressed { get; set; }

        public int StartIndex { get; set; } = 0;

        public bool EnableGrid { get; set; }

        public void Show()
        {
            DependencyService.Get<IPhotoViewer>().Show(this);
        }

        public static void Close()
        {
            DependencyService.Get<IPhotoViewer>().Close();
        }
    }
}

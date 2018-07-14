using Stock6.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stock6.Services
{
    interface IPhotoViewer
    {
        void Show(PhotoBrowserModel photoBrowser);

        void Close();
    }
}

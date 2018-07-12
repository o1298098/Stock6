using FFImageLoading.Forms;
using Lottie.Forms;
using Stock6.Actions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Stock6.Views.StockUp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SearchPhoto : ContentPage
	{
        private static StringBuilder ftpurl;
        private FtpHelper ftpHelper;
        private CachedImage image;

        public SearchPhoto()
		{
			InitializeComponent ();
            ftpurl = new StringBuilder();
            ftpHelper = new FtpHelper(App.Context.FtpUrl, App.Context.FtpUser, App.Context.FtpPassword);
            image = new CachedImage();
            image.IsVisible = false;
            TapGestureRecognizer tapgr = new TapGestureRecognizer();
            tapgr.Tapped += (sender, args) => {
                image.IsVisible = false;
                image.Source = null;
            };
            image.GestureRecognizers.Add(tapgr);
            grid.Children.Add(image);
            scanbtn.Clicked += async delegate {
                ScanPage scanPage = new ScanPage(4);
                scanPage.BindingContext = ftpurl;
                scanPage.Title = "扫描图片二维码";
                await Navigation.PushAsync(scanPage);
            };
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (!string.IsNullOrWhiteSpace(ftpurl.ToString()))
            {

                var path = ftpurl.ToString().Clone();
                string[] files = ftpHelper.GetFilesDetailList(path.ToString());

                if (files != null)
                {
                    foreach (var q in files)
                    {
                        int Mindex = q.IndexOf("M");
                        string str = q.Substring(Mindex + 1).Trim();
                        int index = str.IndexOf(" ");
                        string file = str.Substring(index).Trim();
                        CachedImage cachedImage = new CachedImage
                        {
                            Source = UriImageSource.FromStream(() => ftpHelper.Download(path.ToString() + file)),
                            WidthRequest = 120,
                            HeightRequest = 120,
                            Aspect = Aspect.AspectFill,
                            DownsampleToViewSize = true,
                        };
                        TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
                        tapGestureRecognizer.Tapped += (sender, args) => {
                            image.Source = cachedImage.Source;
                            image.IsVisible = true;
                        };
                        cachedImage.GestureRecognizers.Add(tapGestureRecognizer);
                        flexLayout.Children.Add(cachedImage);
                    }

                }
                ftpurl = new StringBuilder();
            }
        }
    }
}
using FFImageLoading.Forms;
using Lottie.Forms;
using Stock6.Actions;
using Stormlion.PhotoBrowser;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
        static StringBuilder ftpurl;
        FtpHelper ftpHelper;
        CachedImage image;
        List<string> fileslist;

        public SearchPhoto()
		{
			InitializeComponent ();
            ftpurl = new StringBuilder();
            fileslist = new List<string>();
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
            ScanPage scanPage = new ScanPage(4);
            scanPage.BindingContext = ftpurl;
            scanPage.Title = "扫描二维码查看图片";
            //scanbtn.Clicked += (s,e)=>{
                //SearchPhotoDetail searchPhotoDetail = new SearchPhotoDetail();
                //searchPhotoDetail.BindingContext = fileslist;
                //await Navigation.PushAsync(searchPhotoDetail);
                
            //};
            Task.Run(async() => {
                await Navigation.PushAsync(scanPage);
            });
        }
        protected void OnClickedShowGallery(object sender, EventArgs e)
        {
            //Stormlion.PhotoBrowser.Droid.PhotoBrowserImplementation
            new PhotoBrowser
            {
                Photos = new List<Photo>
                    {
                        new Photo
                        {
                            URL = "https://raw.githubusercontent.com/stfalcon-studio/FrescoImageViewer/v.0.5.0/images/posters/Vincent.jpg",
                            Title = "Vincent"
                        },
                        new Photo
                        {
                            URL = "https://raw.githubusercontent.com/stfalcon-studio/FrescoImageViewer/v.0.5.0/images/posters/Jules.jpg",
                            Title = "Jules"
                        },
                        new Photo
                        {
                            URL = "https://raw.githubusercontent.com/stfalcon-studio/FrescoImageViewer/v.0.5.0/images/posters/Korben.jpg",
                            Title = "Korben"
                        }
                    },
                ActionButtonPressed = (index) =>
                {
                    PhotoBrowser.Close();
                }
            }.Show();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (!string.IsNullOrWhiteSpace(ftpurl.ToString()))
            {
                flexLayout.Children.Clear();
                fileslist = new List<string>();
                var path = ftpurl.ToString().Clone();
                string[] files = ftpHelper.GetFilesDetailList(path.ToString());
                if (files != null)
                {
                    Device.BeginInvokeOnMainThread(() => {
                        foreach (var q in files)
                        {
                            int Mindex = q.IndexOf("M");
                            string str = q.Substring(Mindex + 1).Trim();
                            int index = str.IndexOf(" ");
                            string file = str.Substring(index).Trim();
                            fileslist.Add(path.ToString() + file);
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
                    });
                }
                    
                ftpurl = new StringBuilder();
            }
        }
    }
}
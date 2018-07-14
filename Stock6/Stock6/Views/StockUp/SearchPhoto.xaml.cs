using FFImageLoading.Forms;
using Lottie.Forms;
using Stock6.Actions;
using Stock6.Models;
using Stock6.ViewModels;
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
        List<ImageModel> models;

        public SearchPhoto()
		{
			InitializeComponent ();
            ftpurl = new StringBuilder();
            fileslist = new List<string>();
            models = new List<ImageModel>();
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
            string path = "/storage/emulated/0/Pictures/"+models[1].Name;
            Stream stream = ftpHelper.Download(models[1].Path);
            MemoryStream mStream = new MemoryStream();
            stream.CopyTo(mStream);
            StreamToFile(mStream, path);
            string[] a = {  };
            //Stormlion.PhotoBrowser.Droid.PhotoBrowserImplementation
            new PhotoBrowserModel
            {
                Photos = new List<ImageModel>
                    {
                        new ImageModel(ImageSource.FromFile(path).ToString(),0,"",""),
                        new ImageModel("https://raw.githubusercontent.com/stfalcon-studio/FrescoImageViewer/v.0.5.0/images/posters/Vincent.jpg",0,"",""),
                        new ImageModel("https://raw.githubusercontent.com/stfalcon-studio/FrescoImageViewer/v.0.5.0/images/posters/Vincent.jpg",0,"",""),
                    },
                ActionButtonPressed = (index) =>
                {
                    PhotoBrowserModel.Close();
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
                            models.Add(new ImageModel(path.ToString() + file, 0,file,file.Substring(file.IndexOf('.'))));
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

        public void StreamToFile(Stream stream, string fileName)
        {
           
            // 把 Stream 转换成 byte[] 
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            // 设置当前流的位置为流的开始 
            stream.Seek(0, SeekOrigin.Begin);

            // 把 byte[] 写入文件 
            FileStream fs = new FileStream(fileName, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(bytes);
            bw.Close();
            fs.Close();
        }

        }
    }
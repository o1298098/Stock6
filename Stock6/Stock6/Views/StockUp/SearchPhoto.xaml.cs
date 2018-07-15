using FFImageLoading.Forms;
using Stock6.Actions;
using Stock6.Models;
using Stock6.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
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
            scanbtn.Clicked += async (s, e) =>
            {
                await Navigation.PushAsync(scanPage);
            };
            Task.Run(async() => {
                await Navigation.PushAsync(scanPage);
            });
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
                string localpath = "/storage/emulated/0/Pictures/Stock6/";
                if (!Directory.Exists(localpath))
                {
                    Directory.CreateDirectory(localpath);
                }
                if (files != null)
                {
                    
                    Device.BeginInvokeOnMainThread(async() => {
                        foreach (var q in files)
                        {
                            int Mindex = q.IndexOf("M");
                            string str = q.Substring(Mindex + 1).Trim();
                            int index = str.IndexOf(" ");
                            string file = str.Substring(index).Trim();
                            string localfilename = localpath + file;
                            models.Add(new ImageModel(localfilename, 0, file, file.Substring(file.IndexOf('.'))));
                            CachedImage cachedImage = new CachedImage
                            {
                                Source = UriImageSource.FromStream(()=>ftpHelper.Download(path.ToString()+file)),
                                WidthRequest = 120,
                                HeightRequest = 120,
                                Aspect = Aspect.AspectFill,
                                DownsampleToViewSize = true,
                            };
                            TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
                            tapGestureRecognizer.Tapped += async (sender, args) => {
                                //image.Source = cachedImage.Source;
                                //image.IsVisible = true;
                                PhotoBrowserModel browserModel = new PhotoBrowserModel
                                {
                                    ActionButtonPressed = (i) =>
                                    {
                                        PhotoBrowserModel.Close();
                                    }
                                };
                                browserModel.Photos = new List<ImageModel>();
                                await Task.Run(() => {
                                    foreach (var s in models)
                                    {
                                        browserModel.Photos.Add(new ImageModel(Com.Facebook.Common.Util.UriUtil.GetUriForFile(new Java.IO.File(s.Path)).ToString(), 0, "", ""));
                                    }
                                });
                                browserModel.StartIndex =Array.IndexOf(files,q) ;
                                browserModel.Show();
                            };
                            cachedImage.GestureRecognizers.Add(tapGestureRecognizer);
                            flexLayout.Children.Add(cachedImage);
                        }
                        await Task.Run(() => {
                        foreach (var t in models)
                        {
                                Stream stream = ftpHelper.Download(path.ToString() + t.Name);
                                StreamToFile(stream, t.Path);
                        }
                        });
                    });
                    
                }
                    
                ftpurl = new StringBuilder();
            }
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();           
            string localpath = "/storage/emulated/0/Pictures/Stock6/";
            if (Directory.Exists(localpath))
            {
                DirectoryInfo dir = new DirectoryInfo(localpath);
                FileSystemInfo[] fileinfo = dir.GetFileSystemInfos(); 
                foreach (FileSystemInfo i in fileinfo)
                {
                    if (i is DirectoryInfo)            
                    {
                        DirectoryInfo subdir = new DirectoryInfo(i.FullName);
                        subdir.Delete(true);
                    }
                    else
                    {
                        File.Delete(i.FullName);
                    }
                }
            }

        }

        public void StreamToFile(Stream stream, string fileName)
        {
            FileStream fs = File.Create(fileName);
            if (fs != null)
            {
                int buffer_count = 65536;
                byte[] buffer = new byte[buffer_count];
                int size = 0;
                while ((size = stream.Read(buffer, 0, buffer_count)) > 0)
                {
                    fs.Write(buffer, 0, size);
                }
                fs.Flush();
                fs.Close();
            }

        }

        }
    }
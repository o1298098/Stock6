using Stock6.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Media.Abstractions;
using Plugin.Media;
using Stock6.Models;
using System.ComponentModel;
using Stock6.CustomForms;
using FFImageLoading.Forms;
using System.Threading;
using FFImageLoading;
using Stock6.apiHelper;
using Stock6.Actions;
using Xamarin.Essentials;

namespace Stock6.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class StockUpPhoto : ContentPage
	{
        private static List<ImageModel> images { get; set; }
        private List<ImageModel> selectedImages { get; set; }
        private double oldY = 0;
        static SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1,1);

        private int PageIndex { get; set; }

        private int PageSize { get; set; }
        private int Selected { get; set; }


        public StockUpPhoto()
		{
			InitializeComponent ();
            this.PageIndex = 0;
            this.PageSize = 24;
            this.Selected = 0;
            this.selectedImages = new List<ImageModel>();
            ImageService.Instance.Config.MaxMemoryCacheSize = 1000;
            Grid firstgrid = new Grid()
            {
                WidthRequest = 120,
                HeightRequest = 120,
            };
            Image takaphoto = new Image
            {
                WidthRequest = 120,
                HeightRequest = 120,
                Scale = 0.4,
                Source="camera_black.png"

            };
            BoxView box = new BoxView
            {
                Opacity = 0.5,
                Color = Color.Black,
            };
            TapGestureRecognizer recognizer = new TapGestureRecognizer();
            recognizer.Tapped += async (s, args) =>
            {
                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                    return;
                var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                {
                    Directory = "Stock6Media",
                    SaveToAlbum = true,
                    CompressionQuality = 75,
                    CustomPhotoSize = 50,
                    PhotoSize = PhotoSize.MaxWidthHeight,
                    MaxWidthHeight = 2000,
                    DefaultCamera = CameraDevice.Front
                });
                if (file == null)
                    return;
                ImageModel model = new ImageModel(file.Path,111,"zaop","");
                Grid grid = AddImage(model);
                flexLayout.Children.Insert(1, grid);
                //takaphoto.Source = ImageSource.FromStream(() =>
                //{
                //    var stream = file.GetStream();
                //    file.Dispose();
                //    return stream;
                //});
            };
            box.GestureRecognizers.Add(recognizer);
            firstgrid.Children.Add(takaphoto);
            firstgrid.Children.Add(box);
            flexLayout.Children.Add(firstgrid);
            this.Appearing += async (sender, e) =>
            {                
                images = await DependencyService.Get<IPicturePicker>().GetImageStreamAsync(); 
                var initResult = await BindSearchResult();
                activityIndicator.IsRunning = false;
                activityIndicator.IsVisible = false;
            };
            scrollview.Scrolled += async (sender, e) =>
            {         
                if (scrollview.ScrollY >= scrollview.ContentSize.Height - scrollview.Height*1.5)
                {
                    oldY = scrollview.ScrollY;
                    var result = await BindSearchResult();
                   
                }
            };
            SendBtn.Clicked +=async delegate
            {
                var current = Connectivity.NetworkAccess;
                if (current != NetworkAccess.Internet)
                {
                    DependencyService.Get<IToast>().LongAlert("网络异常,请稍后重试！");
                    return;
                }
                
                string billno ;                
                if (BindingContext != null)
                {
                    billno = BindingContext.ToString();
                    if (selectedImages.Count > 0)
                    {
                        if (string.IsNullOrWhiteSpace(App.Context.FtpUrl) || string.IsNullOrWhiteSpace(App.Context.FtpUser) || string.IsNullOrWhiteSpace(App.Context.FtpPassword))
                        {
                            DependencyService.Get<IToast>().LongAlert("请在设置中完善ftp信息");
                            return;
                        }
                        FtpHelper ftpHelper = new FtpHelper(App.Context.FtpUrl, App.Context.FtpUser, App.Context.FtpPassword);
                        bool isdirexist = ftpHelper.DirectoryExist(billno);
                        if (!isdirexist)
                            ftpHelper.MakeDir(billno);
                        progressbar.IsVisible = true;
                        progressbar.Progress = 0;
                        await Task.Run(() => {
                            for (int i = 0; i < selectedImages.Count; i++)
                            {
                                ftpHelper.Upload(selectedImages[i].Path, billno);
                                progressbar.Progress = (double)(i + 1) / (double)selectedImages.Count;
                            }

                        });

                        progressbar.IsVisible = false;
                        DependencyService.Get<IToast>().LongAlert("成功");
                        if (billno.Contains("WLBHD"))
                        {
                            List<object> Parameters = new List<object>();
                            Parameters.Add(App.Context.DataCenterId);
                            Parameters.Add(5);
                            Parameters.Add(billno);
                            Parameters.Add(App.Context.FtpUrl + billno + "/");
                            string result = InvokeHelper.AbstractWebApiBusinessService("Kingdee.BOS.WebAPI.ServiceExtend.ServicesStub.CustomBusinessService.UpdateStockUpScanState", Parameters);
                        }                       
                    }
                }
                else
                {
                     DependencyService.Get<IToast>().LongAlert("请先扫描备货单条码后再上传图片");
                }
               
            };
            //BackgroundWorker worker = new BackgroundWorker();
            //worker.DoWork += Worker_DoWorkAsync;
            //worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            //worker.RunWorkerAsync();
            //LoadBitmapCollection();
        }
        private async Task<bool> BindSearchResult()
        {
                //await semaphoreSlim.WaitAsync();
                if (images != null)
                {

                    int imageDimension = Device.RuntimePlatform == Device.iOS ||
                                     Device.RuntimePlatform == Device.Android ? 120 : 60;
                    int curretcount = (this.PageIndex + 1) * this.PageSize;
                    int count = curretcount < images.Count ? curretcount : images.Count;


                Device.BeginInvokeOnMainThread(()=>{ 
                        for (int i = this.PageIndex * this.PageSize; i < count; i++)
                    {
                       
                        Grid stack = AddImage(images[i]);
                        flexLayout.Children.Add(stack);


                    }
                    this.PageIndex++;
                       });
                return true;
                }
                else
                {
                    return false;
                }           
        }

        private Grid AddImage(ImageModel imagemodel)
        {
            int imageDimension = Device.RuntimePlatform == Device.iOS ||
                                    Device.RuntimePlatform == Device.Android ? 120 : 60;
            Grid stack = new Grid()
            {
                WidthRequest = imageDimension,
                HeightRequest = imageDimension,
            };

            CachedImage cachedImage = new CachedImage
            {
                Source = ImageSource.FromFile(imagemodel.Path),
                WidthRequest = imageDimension,
                HeightRequest = imageDimension,
                Aspect = Aspect.AspectFill,
                DownsampleToViewSize = true,
                //LoadingPlaceholder = "xiaobin.jpg"
            };
            bool ischeck = false;
            BoxView box = new BoxView
            {
                Opacity = 0.5,
                Color = Color.Black,
            };
            Image selectpic = new Image
            {
                Source = "select_green.png",
                Margin = new Thickness(0, 5, 5, 0),
                WidthRequest = 20,
                HeightRequest = 20,
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.Start
            };
            TapGestureRecognizer recognizer = new TapGestureRecognizer();
            ImageModel smodel = imagemodel;
            recognizer.Tapped += (sender, args) =>
            {
                if (ischeck)
                {
                    ischeck = !ischeck;
                    Selected--;
                    SendBtn.Text = string.Format("发送({0}/9)", Selected);
                    selectedImages.Remove(smodel);
                    stack.Children.Remove(selectpic);
                    stack.Children.Remove(box);
                }
                else
                {
                    if (Selected < 9)
                    {
                        ischeck = !ischeck;
                        Selected++;
                        SendBtn.Text = string.Format("发送({0}/9)", Selected);
                        selectedImages.Add(smodel);
                        stack.Children.Add(box);
                        stack.Children.Add(selectpic);
                    }

                }

            };
            box.GestureRecognizers.Add(recognizer);
            cachedImage.GestureRecognizers.Add(recognizer);
            stack.Children.Add(cachedImage);
            return stack;
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //activityIndicator.IsRunning = false;
            //activityIndicator.IsVisible = false;
        }

        private async void Worker_DoWorkAsync(object sender, DoWorkEventArgs e)
        {
            images = await DependencyService.Get<IPicturePicker>().GetImageStreamAsync();
        }
        async void LoadBitmapCollection()
        {
            int imageDimension = Device.RuntimePlatform == Device.iOS ||
                                 Device.RuntimePlatform == Device.Android ? 120 : 60;
            List<ImageModel> images = await DependencyService.Get<IPicturePicker>().GetImageStreamAsync();

            try
            {
                Device.BeginInvokeOnMainThread(() => {
                    if (images != null)
                    {
                        for (int i = 0; i < 200; i++)
                        {
                            CachedImage cachedImage = new CachedImage
                            {
                                Source = ImageSource.FromFile(images[i].Path),
                                WidthRequest = imageDimension,
                                HeightRequest = imageDimension,
                                Aspect = Aspect.AspectFill,
                                DownsampleToViewSize = true,
                                //LoadingPlaceholder = "xiaobin.jpg"
                            };
                            flexLayout.Children.Add(cachedImage);
                        }
                    }
                });
                    
                
            }
            catch (Exception ex)
            {
                flexLayout.Children.Add(new Label
                {
                    Text = "Cannot access list of bitmap files"
                });
            }
            activityIndicator.IsRunning = false;
            activityIndicator.IsVisible = false;
        }
        protected override void OnDisappearing()
        {
            GC.Collect();
            base.OnDisappearing();
        }

    }
    
}
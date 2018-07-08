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

namespace Stock6.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class StockUpPhoto : ContentPage
	{
        private static List<ImageModel> images;
        private double oldY = 0;
        static SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 1);

        public int PageIndex { get; set; }

        public int PageSize { get; set; }


        public StockUpPhoto()
		{
			InitializeComponent ();
            this.PageIndex = 0;
            this.PageSize = 15;
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
                //else if (scrollview.ScrollY - oldY >= 120)
                //{
                //    if (PageIndex > 2)
                //    {
                //        for (int i = (PageIndex-2) * PageSize; i < (PageIndex - 2) * PageSize+3; i++)
                //        {
                //            ((CachedImage)flexLayout.Children[i]).IsVisible = false;
                //        }
                           
                //    }
                   
                //}


            };
            //takePhoto.Clicked += async (sender, args) =>
            //{
            //    if (!CrossMedia.Current.IsPickPhotoSupported)
            //    {
            //        DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
            //        return;
            //    }
            //    var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            //    {
            //        PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,
            //        ModalPresentationStyle=MediaPickerModalPresentationStyle.FullScreen,



            //    });


            //    if (file == null)
            //        return;

            //    image.Source = ImageSource.FromStream(() =>
            //    {
            //        var stream = file.GetStream();
            //        file.Dispose();
            //        return stream;
            //    });
            //};
            //images = new List<ImageModel>();
            //photolist.ItemsSource = images;
            //int imageDimension = Device.RuntimePlatform == Device.iOS ||
            //                    Device.RuntimePlatform == Device.Android ? 120 : 60;
            //DataTemplate template = new DataTemplate(() => {
            //    Image image = new Image
            //    {
            //        WidthRequest = imageDimension,
            //        HeightRequest= imageDimension,
            //        Aspect=Aspect.AspectFill
            //    };
            //    image.SetBinding(Image.SourceProperty, new Binding("Uri"));
            //    return new ViewCell
            //    {
            //        View = image,
            //    };
            //});


            //photolist.ItemTemplate = template;
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

                Device.BeginInvokeOnMainThread(() =>
                {
                for (int i = this.PageIndex * this.PageSize; i < count; i++)
                        {                                                   
                              CachedImage cachedImage = new CachedImage
                              {
                                Source = ImageSource.FromFile(images[i].Path),
                                WidthRequest = imageDimension,
                                HeightRequest = imageDimension,
                                Aspect = Aspect.AspectFill,
                                DownsampleToViewSize = true,
                                LoadingPlaceholder = "xiaobin.jpg"
                              };
                              flexLayout.Children.Add(cachedImage);
                         
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
                                LoadingPlaceholder = "xiaobin.jpg"
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

    }
    
}
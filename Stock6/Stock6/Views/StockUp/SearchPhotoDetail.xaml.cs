using FFImageLoading.Forms;
using Lottie.Forms;
using Stock6.Actions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Stock6.Views.StockUp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPhotoDetail : CarouselPage
    {
        class streams: INotifyPropertyChanged {
            public Stream stream { get; set; }
            public ImageSource imageSource { get => ImageSource.FromStream(() => stream); }

            public event PropertyChangedEventHandler PropertyChanged;
        }
       
		public SearchPhotoDetail()
		{
			InitializeComponent ();
            ItemTemplate = new DataTemplate(() => {
               StackLayout stack= new StackLayout();
            CachedImage cachedImage = new CachedImage {
                CacheDuration = TimeSpan.FromDays(30),
                RetryCount = 0,
                RetryDelay = 250,
                WidthRequest=300,
                HeightRequest=500
            };
                cachedImage.SetBinding(CachedImage.SourceProperty, "imageSource");
                stack.Children.Add(cachedImage);
                return new ContentPage
                {

                    Content =new ScrollView {Content= stack } 
                };
                });
            this.BindingContextChanged +=async delegate {
                List<streams> streams = new List<streams>();
                List<string> vs = BindingContext as List<string>;
                FtpHelper ftpHelper = new FtpHelper(App.Context.FtpUrl, App.Context.FtpUser, App.Context.FtpPassword);
                if (vs != null)
                {
                  await  Task.Run(() => {
                        foreach (var q in vs)
                        {
                            Stream file = ftpHelper.Download(q);
                            streams.Add(new streams() { stream= file });
                        }
                    });
                }

                ItemsSource = streams;
            };
        
        }
	}
}
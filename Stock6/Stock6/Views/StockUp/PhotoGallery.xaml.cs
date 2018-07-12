using FFImageLoading;
using Stock6.Actions;
using Stock6.apiHelper;
using Stock6.CustomControls;
using Stock6.Models;
using Stock6.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Stock6.Views.StockUp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PhotoGallery : ContentPage
	{
        List<ImageModel> images;
        private static List<ImageModel> Selectedimages;
        public PhotoGallery ()
		{
			InitializeComponent ();
            Grid grid = new Grid();
            ProgressBar progressbar = new ProgressBar
            {
                Progress = 0,
                VerticalOptions = LayoutOptions.Start,
                BackgroundColor = Color.FromHex("#fafafa"),
                Opacity = 0.9,
                HeightRequest = 50,
                IsVisible = false,
            };
            ListView listView = new ListView(ListViewCachingStrategy.RecycleElement);
            ImageService.Instance.Config.MaxMemoryCacheSize = 1000;
            this.Appearing += async (sender, e) =>
            {
                images = await DependencyService.Get<IPicturePicker>().GetImageStreamAsync();
                Selectedimages = new List<ImageModel>();
                listView.BeginRefresh();
            };
            listView.ItemsSource = images;
            listView.ItemTemplate = new DataTemplate(typeof(CachedImageCell));
            //listView.IsPullToRefreshEnabled = true;
            listView.HasUnevenRows = true;
            
            listView.ItemSelected += (s, e) =>
             {
                 ImageModel selected = (ImageModel)e.SelectedItem;
                 if (Selectedimages.Contains(selected))
                 {

                     Selectedimages.Remove(selected);
                 }
                 else
                 {
                     Selectedimages.Add(selected);
                 }                 
             };
            listView.Refreshing += delegate
            {
                listView.ItemsSource = images;
                listView.EndRefresh();
            };
            SendBtn.Clicked += async delegate
            {
                string billno;
                if (BindingContext != null)
                {
                    billno = BindingContext.ToString();
                    if (Selectedimages.Count > 0)
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
                            for (int i = 0; i < Selectedimages.Count; i++)
                            {
                                ftpHelper.Upload(Selectedimages[i].Path, billno);
                                progressbar.Progress = (double)(i + 1) / (double)Selectedimages.Count;
                            }

                        });

                        progressbar.IsVisible = false;
                        DependencyService.Get<IToast>().LongAlert("成功");
                        List<object> Parameters = new List<object>();
                        Parameters.Add(App.Context.DataCenterId);
                        Parameters.Add(5);
                        Parameters.Add(billno);
                        Parameters.Add(App.Context.FtpUrl + billno);
                        string result = InvokeHelper.AbstractWebApiBusinessService("Kingdee.BOS.WebAPI.ServiceExtend.ServicesStub.CustomBusinessService.UpdateStockUpScanState", Parameters);
                    }
                }
                else
                {
                    DependencyService.Get<IToast>().LongAlert("请先扫描备货单条码后再上传图片");
                }

            };
            grid.Children.Add(listView);
            grid.Children.Add(progressbar);
            Content = grid;
        }
	}
}
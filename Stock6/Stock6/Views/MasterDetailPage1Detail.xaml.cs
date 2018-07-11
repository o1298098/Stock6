using FFImageLoading;
using FFImageLoading.Forms;
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

namespace Stock6.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterDetailPage1Detail : ContentPage
    {
        List<ImageModel> images;
        public MasterDetailPage1Detail()
        {
            InitializeComponent();
            CustomListView listView = new CustomListView();
            ImageService.Instance.Config.MaxMemoryCacheSize = 1000;
            this.Appearing += async (sender, e) =>
            {
                images = await DependencyService.Get<IPicturePicker>().GetImageStreamAsync();
            };
            listView.ItemsSource = images;
            listView.ItemTemplate = new DataTemplate(typeof(CachedImageCell));
            listView.IsPullToRefreshEnabled = true;
            listView.HasUnevenRows = true;
            
            listView.Refreshing +=delegate{
                listView.ItemsSource = images;
                listView.EndRefresh();
            };
            Content = listView;
        }
    }
}
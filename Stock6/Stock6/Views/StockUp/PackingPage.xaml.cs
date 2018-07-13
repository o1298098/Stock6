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
	public partial class PackingPage : ContentPage
	{
        private static StringBuilder scanResult;
		public PackingPage ()
		{
			InitializeComponent ();
            Grid grid = new Grid();
            Label label = new Label
            {
                Text = "请扫描二维码后选择需要上传的图片",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions=LayoutOptions.Center,
                FontSize=20
            };
            grid.Children.Add(label);
            Content = grid;
            ScanPage scanPage = new ScanPage(5);
            scanPage.BindingContext = scanResult;
            scanPage.Title = "扫描二维码上传图片";
            Scanbtn.Clicked += async delegate {
                await Navigation.PushAsync(scanPage);
            };
            Task.Run(async()=> {
                await Navigation.PushAsync(scanPage);
            });
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
    }
}
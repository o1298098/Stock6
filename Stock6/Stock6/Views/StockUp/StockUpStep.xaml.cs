using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Stock6.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Stock6.Views.StockUp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class StockUpStep : ContentPage
	{
        private static StockUpBillModel stockUpBillModel;
        public StockUpStep ()
		{
			InitializeComponent ();
            stockUpBillModel = new StockUpBillModel();
            BillNo.SetBinding(Label.TextProperty, new Binding("FBillNo") { Source = stockUpBillModel });
            Name.SetBinding(Label.TextProperty, new Binding("F_XAY_Custom") { Source = stockUpBillModel });
            Phone.SetBinding(Label.TextProperty, new Binding("F_XAY_Phone") { Source = stockUpBillModel });
            scanBarAnimation.OnClick += async delegate
            {
                ScanPage scanPage = new ScanPage(1);
                scanPage.BindingContext = stockUpBillModel;
                await Navigation.PushAsync(scanPage);
                //scanstacklayout.IsVisible = false;
                //resultstacklayout.IsVisible = true;
            };
            scanQrAnimation.OnClick +=async delegate {
                if (string.IsNullOrWhiteSpace(stockUpBillModel.FBillNo))
                {
                    Qrlabel.Text = "请先操作Step1";
                    Qrlabel.TextColor = Color.Red;
                }
                else
                {
                    await Navigation.PushAsync(new ScanPage(2));
                }                
                //QrResultstacklayouy.IsVisible = true;
                //QRStacklayout.IsVisible = false;
            };
		}
        protected override void OnAppearing()
        {
            if (!string.IsNullOrWhiteSpace(stockUpBillModel.FBillNo))
            {
                scanstacklayout.IsVisible = false;
                resultstacklayout.IsVisible = true;
                listview.IsVisible = true;
                QRStacklayout.IsVisible = false;
                listview.ItemsSource = stockUpBillModel.XAY_StockUpOrderEntry;
                Logistics.SetBinding(Label.TextProperty, new Binding("Value") { Source = stockUpBillModel.F_XAY_Logistics.SimpleName[0]});
            }
            base.OnAppearing();
           
        }
    }
}
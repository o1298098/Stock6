using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            BillNo.SetBinding(Label.TextProperty, new Binding("billno") { Source = stockUpBillModel });
            Name.SetBinding(Label.TextProperty, new Binding("name") { Source = stockUpBillModel });
            Phone.SetBinding(Label.TextProperty, new Binding("phone") { Source = stockUpBillModel });
            Logistics.SetBinding(Label.TextProperty, new Binding("logistics") { Source = stockUpBillModel });
            scanBarAnimation.OnClick += async delegate
            {
                ScanPage scanPage = new ScanPage();
                scanPage.BindingContext = stockUpBillModel;
                await Navigation.PushAsync(scanPage);
                //scanstacklayout.IsVisible = false;
                //resultstacklayout.IsVisible = true;
            };
            scanQrAnimation.OnClick +=async delegate {
                await Navigation.PushAsync(new ScanPage());
                //QrResultstacklayouy.IsVisible = true;
                //QRStacklayout.IsVisible = false;
            };
		}
        protected override void OnAppearing()
        {
            if (!string.IsNullOrWhiteSpace(stockUpBillModel.billno))
            {
                scanstacklayout.IsVisible = false;
                resultstacklayout.IsVisible = true;
            }
            base.OnAppearing();
           
        }
    }
}
using candaBarcode.Forms;
using Stock6.action;
using Stock6.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;
using Xamarin.Essentials;

namespace Stock6.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ScanPage : ContentPage
	{
        private ZXingScannerView zxing;
        private ZXingOverlay overlay;
        private Label label;
        private Button button;
        public ScanPage ()
		{
			InitializeComponent ();           
            ZXing.Mobile.MobileBarcodeScanningOptions scanningOptions = new ZXing.Mobile.MobileBarcodeScanningOptions { DelayBetweenContinuousScans = 2000, PossibleFormats = new List<ZXing.BarcodeFormat> { ZXing.BarcodeFormat.CODE_128, ZXing.BarcodeFormat.CODE_39, ZXing.BarcodeFormat.CODE_93, ZXing.BarcodeFormat.EAN_13, ZXing.BarcodeFormat.EAN_8 ,ZXing.BarcodeFormat.QR_CODE}};
           
            zxing = new ZXingScannerView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Options = scanningOptions,
            };
            zxing.OnScanResult += (result) =>

                Device.BeginInvokeOnMainThread(async () =>
                {
                    Vibration.Vibrate();
                    var duration = TimeSpan.FromSeconds(1);
                    Vibration.Vibrate(duration);
                    string content = "{\"FormId\":\"9d0a72f2a1104fe1881969ad5a1fc22d\",\"FieldKeys\":\"FBillNo,F_XAY_Custom,F_XAY_Phone,F_XAY_Logistics.FSimpleName\",\"FilterString\":\"FBillNo='WLBHD201806290003'and  FDocumentStatus='A'\",\"OrderString\":\"\",\"TopRowCount\":\"0\",\"StartRow\":\"0\",\"Limit\":\"0\"}";
                    string[] results = Jsonhelper.JsonToString(content);
                    if (results == null)
                    {
                        await DisplayAlert("提示", "系统无此单号", "OK");
                    }
                    else
                    {
                        StockUpBillModel stockUpBillModel = (StockUpBillModel)BindingContext;
                        string txt = results[0].Replace("[", "");
                        string[] array = txt.Split(',');
                        stockUpBillModel.billno = array[0];
                        stockUpBillModel.name = array[1];
                        stockUpBillModel.phone = array[2];
                        stockUpBillModel.logistics = array[3].Replace("]", "");
                    }
                    // Stop analysis until we navigate away so we don't keep reading barcodes
                    //zxing.IsAnalyzing = false;
                    // Show an alert
                    //await DisplayAlert("扫描条码", result.Text, "OK");                   
                    //Navigate away
                    await Navigation.PopAsync(true);
                });

            label = new Label
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                AutomationId = "label",
                TextColor = Color.White,
                FontSize = 30

            };
            button = new Button {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                Text="hahha"
            };
            button.Clicked += async delegate {
                Vibration.Vibrate();
                var duration = TimeSpan.FromSeconds(1);
                Vibration.Vibrate(duration);
                string content = "{\"FormId\":\"9d0a72f2a1104fe1881969ad5a1fc22d\",\"FieldKeys\":\"FBillNo,F_XAY_Custom,F_XAY_Phone,F_XAY_Logistics.FSimpleName\",\"FilterString\":\"FBillNo='WLBHD201806290003'and  FBillStatus='A'\",\"OrderString\":\"\",\"TopRowCount\":\"0\",\"StartRow\":\"0\",\"Limit\":\"0\"}";
                string[] results = Jsonhelper.JsonToString(content);
                if (results == null)
                {
                    await DisplayAlert("提示", "系统无此单号", "OK");
                }
                else
                {
                    StockUpBillModel stockUpBillModel = (StockUpBillModel)BindingContext;
                    string txt = results[0].Replace("[", "");
                    string[] array = txt.Split(',');
                    stockUpBillModel.billno = array[0];
                    stockUpBillModel.name = array[1];
                    stockUpBillModel.phone = array[2];
                    stockUpBillModel.logistics = array[3].Replace("]", "");
                }
                await Navigation.PopAsync(true);
            };
            overlay = new ZXingOverlay
            {
                ShowFlashButton = false,
            };
            overlay.FlashButtonClicked += (sender, e) =>
            {
                try
                {

                    //if (!zxing.IsTorchOn)
                    //{
                    //    sender.Image = "flashlighton.png";
                    ////    CrossLampState = true;
                    //    zxing.IsTorchOn = true;

                    //}
                    //else
                    //{
                    //    sender.Image = "flashlightoff.png";
                    //    zxing.IsTorchOn = false;
                    //}
                }
                catch (Exception ex)
                {
                    throw ex;

                }
            };
            var grid = new Grid
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };
            grid.Children.Add(zxing);
            overlay.Children.Add(button, 0, 0);
            grid.Children.Add(overlay);
            Content = grid;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            zxing.IsScanning = true;
        }

        protected override void OnDisappearing()
        {
            zxing.IsScanning = false;

            base.OnDisappearing();
        }
    }
}

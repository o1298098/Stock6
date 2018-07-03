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
using Stock6.apiHelper;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Stock6.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ScanPage : ContentPage
	{
        private ZXingScannerView zxing;
        private ZXingOverlay overlay;
        private Label label;
        private Button button;
        public ScanPage (int mode)
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
                    var duration = TimeSpan.FromMilliseconds(100);
                    Vibration.Vibrate(duration);                    
                    if (mode == 1)
                    {
                        List<object> Parameters = new List<object>();
                        Parameters.Add("5ab05fc34e03d1");
                        Parameters.Add(result);
                        InvokeHelper.Login();
                        string json = apiHelper.InvokeHelper.AbstractWebApiBusinessService("Kingdee.BOS.WebAPI.ServiceExtend.ServicesStub.CustomBusinessService.StockUpExecuteService", Parameters);
                        if (json == "err")
                        {
                            await DisplayAlert("提示", "系统无此单号", "OK");
                        }
                        else
                        {
                            var jsonobject = JsonConvert.DeserializeObject<StockUpBillModel>(json);
                            StockUpBillModel stockUpBillModel = (StockUpBillModel)BindingContext;
                            stockUpBillModel.FBillNo = jsonobject.FBillNo;
                            stockUpBillModel.F_XAY_Custom = jsonobject.F_XAY_Custom;
                            stockUpBillModel.F_XAY_Phone = jsonobject.F_XAY_Phone;
                            stockUpBillModel.F_XAY_Logistics = jsonobject.F_XAY_Logistics;
                            stockUpBillModel.XAY_StockUpOrderEntry = jsonobject.XAY_StockUpOrderEntry;
                        }
                    }
                    else if (mode == 2)
                    {

                    }
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
                //Vibration.Vibrate();
                //var duration = TimeSpan.FromMilliseconds(100);
                //Vibration.Vibrate(duration);
                List<object> Parameters = new List<object>();
                Parameters.Add("5ab05fc34e03d1");
                Parameters.Add("WLBHD201806220001");
                InvokeHelper.Login();
                string result = apiHelper.InvokeHelper.AbstractWebApiBusinessService("Kingdee.BOS.WebAPI.ServiceExtend.ServicesStub.CustomBusinessService.StockUpExecuteService", Parameters);
                if (result == "err")
                {
                    await DisplayAlert("提示", "系统无此单号", "OK");
                }
                else
                {
                    var json = JsonConvert.DeserializeObject<StockUpBillModel>(result);
                    StockUpBillModel stockUpBillModel = (StockUpBillModel)BindingContext;
                    stockUpBillModel.FBillNo = json.FBillNo;
                    stockUpBillModel.F_XAY_Custom = json.F_XAY_Custom;
                    stockUpBillModel.F_XAY_Phone = json.F_XAY_Phone;
                    stockUpBillModel.F_XAY_Logistics = json.F_XAY_Logistics;
                    stockUpBillModel.XAY_StockUpOrderEntry = json.XAY_StockUpOrderEntry;
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

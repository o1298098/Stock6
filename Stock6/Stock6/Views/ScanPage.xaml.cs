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
using Lottie.Forms;
using System.ComponentModel;
using Stock6.ViewModels;
using Stock6.CustomControls;

namespace Stock6.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ScanPage : ContentPage
	{
        private ZXingScannerView zxing;
        private ZXingOverlay overlay;
        private Label label;
        private Button button;
        private AnimationView Loadinganimation;
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
                        StockUpBillModel stockUpBillModel = (StockUpBillModel)BindingContext;
                        Loadinganimation.IsVisible = true;
                        //BackgroundWorker worker = new BackgroundWorker();

                       await Task.Run(() => { 
                            List<object> Parameters = new List<object>();
                            Parameters.Add(App.Context.DataCenterId);
                            Parameters.Add(result.ToString());
                            string json = apiHelper.InvokeHelper.AbstractWebApiBusinessService("Kingdee.BOS.WebAPI.ServiceExtend.ServicesStub.CustomBusinessService.StockUpExecuteService", Parameters);
                            if (json == "err" || string.IsNullOrWhiteSpace(json))
                            {
                                return;
                            }
                            else
                            {

                                var jsonobject = JsonConvert.DeserializeObject<StockUpBillModel>(json);
                                stockUpBillModel.Id = jsonobject.Id;
                                stockUpBillModel.FBillNo = jsonobject.FBillNo;
                                stockUpBillModel.F_XAY_Custom = jsonobject.F_XAY_Custom;
                                stockUpBillModel.F_XAY_Phone = jsonobject.F_XAY_Phone;
                                stockUpBillModel.F_XAY_Logistics = jsonobject.F_XAY_Logistics;
                                stockUpBillModel.XAY_StockUpOrderEntry = jsonobject.XAY_StockUpOrderEntry;

                            }
                        });
                        await Navigation.PopAsync();
                        //worker.DoWork += delegate
                        //{};
                        //worker.RunWorkerAsync();
                        //worker.RunWorkerCompleted += async delegate { await Navigation.PopAsync(); };
                    }
                    else if (mode == 2)
                    {
                        string qrstring = BaseToString(result.ToString());
                        StockUpBillModel stockUpBillModel = (StockUpBillModel)BindingContext;
                        if (qrstring.Substring(0, 2) != "#%")
                        {
                            label.Text= "二维码数据格式有误";
                            return;
                        }
                        string jsonstring = qrstring.Substring(2, qrstring.Length - 2);
                        JObject jObject = (JObject)JsonConvert.DeserializeObject(jsonstring);
                        if (jObject.ContainsKey("Id"))
                        {                           
                            string ID = jObject["Id"].ToString();
                            if ((bool)jObject["isgroup"])
                            {
                                var MaterialInfo = jObject["MaterialInfo"];
                                for (int i = 0; i < MaterialInfo.Count(); i++)
                                {
                                    string subID = MaterialInfo[i]["Id"].ToString();
                                    List<object> Parameters = new List<object>();
                                    Parameters.Add(App.Context.DataCenterId);
                                    Parameters.Add(2);
                                    Parameters.Add(subID);
                                    string apiresult = InvokeHelper.AbstractWebApiBusinessService("Kingdee.BOS.WebAPI.ServiceExtend.ServicesStub.CustomBusinessService.UpdateStockUpScanState", Parameters);
                                    if (apiresult == "1")
                                    {
                                        var sd = stockUpBillModel.XAY_StockUpOrderEntry.Single(o => o.Id == ID);
                                        var s = sd.XAY_t_StockUpOrderSubEntry.Single(o => o.id == subID);
                                        s.F_XAY_IsCScan = true;
                                        var scancount = (from q in sd.XAY_t_StockUpOrderSubEntry
                                                         where q.F_XAY_IsCScan == false
                                                         select new { q.F_XAY_IsCScan }).Count();
                                        if (scancount == 0)
                                        {
                                          await  UpdateScanStateAsync(stockUpBillModel, ID, 1);
                                        }
                                    }
                                    else
                                    {
                                        label.Text = "err";
                                    }
                                }
                            }
                            else
                            {
                               await UpdateScanStateAsync(stockUpBillModel, ID, 1);
                                label.Text = "扫描成功";
                            }
                        }
                    }
                    else if (mode == 3)
                    {
                        StockUpPageModel Model = (StockUpPageModel)BindingContext;
                        Model.LogisticsNum = result.ToString();
                        List<object> Parameters = new List<object>();
                        Parameters.Add(App.Context.DataCenterId);
                        Parameters.Add(mode);
                        Parameters.Add(Model.Id);
                        Parameters.Add(Model.LogisticsNum);
                        string backresult = InvokeHelper.AbstractWebApiBusinessService("Kingdee.BOS.WebAPI.ServiceExtend.ServicesStub.CustomBusinessService.UpdateStockUpScanState", Parameters);
                        if (backresult == "1")
                        {
                            Model.isscan = true;
                            await Navigation.PopAsync();
                        }
                        else { label.Text = "err"; }
                    }

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
             Loadinganimation = new AnimationView
            {
                Animation = "loading.json",
                HorizontalOptions = LayoutOptions.Center,
                WidthRequest = 200,
                AutoPlay = true,
                Loop = true,
                IsVisible = false
            };
            #region
            button.Clicked += async delegate {
                //Vibration.Vibrate();
                //var duration = TimeSpan.FromMilliseconds(100);
                //Vibration.Vibrate(duration);

                if (mode == 1)
                {
                    StockUpBillModel stockUpBillModel = (StockUpBillModel)BindingContext;
                    Loadinganimation.IsVisible = true;
                    await Task.Run(() => {
                        List<object> Parameters = new List<object>();
                        Parameters.Add(App.Context.DataCenterId);
                        Parameters.Add("WLBHD201806220001");
                        string result = apiHelper.InvokeHelper.AbstractWebApiBusinessService("Kingdee.BOS.WebAPI.ServiceExtend.ServicesStub.CustomBusinessService.StockUpExecuteService", Parameters);
                        if (result == "err"||string.IsNullOrWhiteSpace(result))
                        {
                            return;
                        }
                        else
                        {

                            var json = JsonConvert.DeserializeObject<StockUpBillModel>(result);
                            stockUpBillModel.FBillNo = json.FBillNo;
                            stockUpBillModel.F_XAY_Custom = json.F_XAY_Custom;
                            stockUpBillModel.F_XAY_Phone = json.F_XAY_Phone;
                            stockUpBillModel.F_XAY_Logistics = json.F_XAY_Logistics;
                            stockUpBillModel.XAY_StockUpOrderEntry = json.XAY_StockUpOrderEntry;

                        }
                    });
                    
                  await Navigation.PopAsync(); 
                }
                else if (mode == 2)
                {
                    StockUpBillModel stockUpBillModel = (StockUpBillModel)BindingContext;
                    string a = "#%{'Id':'100049','isSpareParts':false,'isgroup':true,'MaterialInfo':[{'Id':'100040','MaterialId':'4154077','Piece':1,'Qty':7,'Unit':'个'}]}";
                    if (a.Substring(0, 2) != "#%")
                    {
                        await DisplayAlert("提示", "二维码数据格式有误", "OK");
                        return;
                    }
                    string jsonstring = a.Substring(2, a.Length - 2);
                    JObject jObject = (JObject)JsonConvert.DeserializeObject(jsonstring);
                    if (jObject.ContainsKey("Id") && jObject.ContainsKey("BillNo"))
                    {
                        if (stockUpBillModel.FBillNo != jObject["BillNo"].ToString())
                            return;
                        string ID = jObject["Id"].ToString();
                        if ((bool)jObject["isgroup"])
                        {
                            var MaterialInfo = jObject["MaterialInfo"];
                            for (int i = 0; i < MaterialInfo.Count(); i++)
                            {
                                string subID = MaterialInfo[i]["Id"].ToString();
                                List<object> Parameters = new List<object>();
                                Parameters.Add(App.Context.DataCenterId);
                                Parameters.Add(2);
                                Parameters.Add(subID);
                                string result = InvokeHelper.AbstractWebApiBusinessService("Kingdee.BOS.WebAPI.ServiceExtend.ServicesStub.CustomBusinessService.UpdateStockUpScanState", Parameters); ;
                                if (result == "1")
                                {
                                    var sd = stockUpBillModel.XAY_StockUpOrderEntry.Single(o => o.Id == ID && o.XAY_t_StockUpOrderSubEntry[i].id == subID);
                                    sd.XAY_t_StockUpOrderSubEntry[i].F_XAY_IsCScan = true;
                                    var scancount = (from q in sd.XAY_t_StockUpOrderSubEntry
                                                     where q.F_XAY_IsCScan == false
                                                     select new { q.F_XAY_IsCScan }).Count();
                                    label.Text = "扫描成功";
                                    if (scancount == 0)
                                    {
                                      await  UpdateScanStateAsync(stockUpBillModel, ID, 1);
                                    }
                                }
                            }
                        }
                        else  
                        {
                         await  UpdateScanStateAsync(stockUpBillModel, ID, 1);
                        }
                    }
                }
                else if (mode == 3)
                {
                    StockUpPageModel Model = (StockUpPageModel)BindingContext;
                    Model.LogisticsNum = "123123123123";
                    List<object> Parameters = new List<object>();
                    Parameters.Add(App.Context.DataCenterId);
                    Parameters.Add(mode);
                    Parameters.Add(Model.Id);
                    Parameters.Add(Model.LogisticsNum);
                    string result = InvokeHelper.AbstractWebApiBusinessService("Kingdee.BOS.WebAPI.ServiceExtend.ServicesStub.CustomBusinessService.UpdateStockUpScanState", Parameters);
                    if (result == "1")
                    {
                        Model.isscan = true;
                        await Navigation.PopAsync();
                    }
                    else { label.Text = "dadad"; }
                }
            };
            #endregion
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
            //overlay.Children.Add(button, 0, 0);
            overlay.Children.Add(Loadinganimation,0,1);
            overlay.Children.Add(label, 0, 2);
            grid.Children.Add(overlay);
            Content = grid;
        }

       

        private async Task UpdateScanStateAsync(StockUpBillModel ob, string ID,int mode)
        {
            List<object> Parameters = new List<object>();
            Parameters.Add(App.Context.DataCenterId);
            Parameters.Add(mode);
            Parameters.Add(ID);
            string result = InvokeHelper.AbstractWebApiBusinessService("Kingdee.BOS.WebAPI.ServiceExtend.ServicesStub.CustomBusinessService.UpdateStockUpScanState", Parameters);
            if (result == "1")
            {
                var sd = ob.XAY_StockUpOrderEntry.Single(o => o.Id == ID);
                sd.F_XAY_IsScan = true;
                var scancount = (from q in ob.XAY_StockUpOrderEntry
                                 where q.F_XAY_IsScan == false
                                 select new { q.F_XAY_IsScan }).Count();
                if (scancount == 0)
                {
                    Parameters = new List<object>();
                    Parameters.Add(App.Context.DataCenterId);
                    Parameters.Add(4);
                    Parameters.Add(ob.Id);
                    Parameters.Add(1);
                    result = InvokeHelper.AbstractWebApiBusinessService("Kingdee.BOS.WebAPI.ServiceExtend.ServicesStub.CustomBusinessService.UpdateStockUpScanState", Parameters);
                    await Navigation.PopToRootAsync();
                }
            }
         }

        private static string BaseToString(string basetext)
        {
            try
            {
                byte[] by = Convert.FromBase64String(basetext);
                string result = Encoding.UTF8.GetString(by);
                return result;
            }
            catch { return "err"; }
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

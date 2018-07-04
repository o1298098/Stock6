using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Lottie.Forms;
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
            listview.ItemTapped += delegate {
                var a=listview.ItemTemplate.Values;
            };
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
                QRStacklayout.IsVisible = false;
                //listview.IsVisible = true;
                listview.ItemsSource = stockUpBillModel.XAY_StockUpOrderEntry;
                Logistics.SetBinding(Label.TextProperty, new Binding("Value") { Source = stockUpBillModel.F_XAY_Logistics.SimpleName[0]});
                UpdateUI();
                QrResultstacklayout.IsVisible = true;

            }
            base.OnAppearing();
           
        }
        private void UpdateUI()
        {
            foreach (var q in stockUpBillModel.XAY_StockUpOrderEntry)
            {

                StackLayout sLayout = new StackLayout{
                    BackgroundColor = Color.White
                };
                StackLayout title = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    Padding =new Thickness(5,5,5,5),
                };
                AnimationView animation = new AnimationView
                {
                    Animation = "more.json",
                    HorizontalOptions=LayoutOptions.Center,
                    WidthRequest=30,
                    HeightRequest=30
                };
                StackLayout cLayout = new StackLayout {
                    IsVisible = false,
                    Padding = new Thickness(30, 2, 5, 5),
                };
                Label FMaterial = new Label {
                    WidthRequest=100,
                    HeightRequest = 30,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    VerticalTextAlignment = TextAlignment.Center,
                    HorizontalTextAlignment=TextAlignment.Start
                };
                Label FCount = new Label
                {
                    WidthRequest = 100,
                    HeightRequest = 30,
                    HorizontalOptions = LayoutOptions.StartAndExpand,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    VerticalTextAlignment = TextAlignment.Center,
                    HorizontalTextAlignment=TextAlignment.Start
                };

                FMaterial.Text = q.F_XAY_FMaterial.Name[0].Value;
                FCount.Text = string.Format("{0} {1}",q.F_XAY_Count,q.F_XAY_Mark);
                foreach (var c in q.XAY_t_StockUpOrderSubEntry)
                {

                    Label CMaterial = new Label();
                    CMaterial.Text = string.Format("{0} {1} {2}", c.F_XAY_CMaterial.Name[0].Value,q.F_XAY_SpareParts? c.F_XAY_CQty:c.F_XAY_SubCount, c.F_XAY_SubUnit);
                    cLayout.Children.Add(CMaterial);
                }
                bool clickstate = false;
                animation.OnClick += delegate
                  {
                      clickstate = !clickstate;
                      if (clickstate)
                      {
                          animation.PlayProgressSegment(0.15f, 0.3f);
                      }
                      else
                      {
                          animation.PlayProgressSegment(0.37f, 0.5f);
                      }
                      cLayout.IsVisible = clickstate;
                  };
                title.Children.Add(FMaterial);
                title.Children.Add(FCount);
                if (q.XAY_t_StockUpOrderSubEntry.Count!=0)
                    title.Children.Add(animation);
                sLayout.Children.Add(title);
                sLayout.Children.Add(cLayout);
                QrResultstacklayout.Children.Add(sLayout);
            }


        }
    }
}
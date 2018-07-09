using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Lottie.Forms;
using Stock6.Models;
using Stock6.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Stock6.Views.StockUp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class StockUpStep : ContentPage
	{
        private static StockUpBillModel stockUpBillModel;
        private bool stepstate;
        public StockUpStep ()
		{
			InitializeComponent ();            
                stockUpBillModel = new StockUpBillModel();
                stepstate = false;
                BillNo.SetBinding(Label.TextProperty, new Binding("FBillNo") { Source = stockUpBillModel });
                Name.SetBinding(Label.TextProperty, new Binding("F_XAY_Custom") { Source = stockUpBillModel });
                Phone.SetBinding(Label.TextProperty, new Binding("F_XAY_Phone") { Source = stockUpBillModel });
                listview.ItemTapped += delegate
                {
                    var a = listview.ItemTemplate.Values;
                };
                scanBarAnimation.OnClick += async delegate
                {
                    ScanPage scanPage = new ScanPage(1);
                    scanPage.BindingContext = stockUpBillModel;
                    await Navigation.PushAsync(scanPage);
                //scanstacklayout.IsVisible = false;
                //resultstacklayout.IsVisible = true;
            };
                scanQrAnimation.OnClick += async delegate
                {
                    if (string.IsNullOrWhiteSpace(stockUpBillModel.FBillNo))
                    {
                        await scanQrAnimation.TranslateTo(20, 0, 50);
                        await scanQrAnimation.TranslateTo(0, 0, 50);
                        await scanQrAnimation.TranslateTo(-20, 0, 50);
                        await scanQrAnimation.TranslateTo(0, 0, 50);
                        await scanQrAnimation.TranslateTo(20, 0, 50);
                        await scanQrAnimation.TranslateTo(0, 0, 50);
                        DependencyService.Get<IToast>().LongAlert("请先完成Step1");
                    }
                    else
                    {
                        ScanPage scanPage = new ScanPage(2);
                        scanPage.BindingContext = stockUpBillModel;
                        await Navigation.PushAsync(scanPage);
                    }
                };
                scanbtn.Clicked += async delegate
                {
                    ScanPage scanPage;

                    if (string.IsNullOrWhiteSpace(stockUpBillModel.FBillNo))
                    {
                        scanPage = new ScanPage(1);
                    }
                    else
                    {
                        scanPage = new ScanPage(2);
                    }
                    scanPage.BindingContext = stockUpBillModel;
                    await Navigation.PushAsync(scanPage);
                };
            picbtn.Clicked += async delegate {
                StockUpPhoto picpage = new StockUpPhoto();
                picpage.BindingContext = stockUpBillModel.FBillNo;
                await Navigation.PushAsync(picpage);
            };

        }
        protected override void OnAppearing()
        {
            if (!string.IsNullOrWhiteSpace(stockUpBillModel.FBillNo))
            {
                scanstacklayout.IsVisible = false;
                resultstacklayout.IsVisible = true;                
                Logistics.SetBinding(Label.TextProperty, new Binding("Value") { Source = stockUpBillModel.F_XAY_Logistics.SimpleName[0] });
                if (stepstate) { 
                QRStacklayout.IsVisible = false;
                //listview.IsVisible = true;
                listview.ItemsSource = stockUpBillModel.XAY_StockUpOrderEntry;
                UpdateUI();
                QrResultstacklayout.IsVisible = true;
                }
                stepstate = stepstate? stepstate:!stepstate;
            }
            base.OnAppearing();
           
        }
        protected override void OnDisappearing()
        {
            GC.Collect();
            base.OnDisappearing();
        }
        private void UpdateUI()
        {
            QrResultstacklayout.Children.Clear();
            foreach (var q in stockUpBillModel.XAY_StockUpOrderEntry)
            {

                StackLayout sLayout = new StackLayout{
                    BackgroundColor = Color.White,
                    Spacing=0
                };
                StackLayout title = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    Padding =new Thickness(15,2,5,2),
                };
                Grid subtitle = new Grid();
                subtitle.ColumnDefinitions.Add(new ColumnDefinition { Width=new GridLength(1,GridUnitType.Star) });
                subtitle.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                subtitle.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                subtitle.RowDefinitions.Add(new RowDefinition { Height=new GridLength(1,GridUnitType.Auto) });
                subtitle.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                subtitle.RowSpacing = 0;
                AnimationView animation = new AnimationView
                {
                    Animation = "more.json",
                    HorizontalOptions=LayoutOptions.Center,
                    WidthRequest=30,
                    HeightRequest=30
                };
                StackLayout cLayout = new StackLayout {
                    IsVisible = true,
                    Padding = new Thickness(30, 2, 5, 5),
                };
                Label FMaterial = new Label {
                    WidthRequest=100,
                    HeightRequest = 30,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    HorizontalOptions=LayoutOptions.StartAndExpand,
                    VerticalTextAlignment = TextAlignment.Center,
                    HorizontalTextAlignment=TextAlignment.Start,
                    TextColor = Color.Black
                };
                Label CountTitle = new Label { Text = "件数" ,Margin=new Thickness(15,0,0,0),FontSize=12};
                Label UnitTitle = new Label {Text="单位", Margin = new Thickness(15, 0, 0, 0), FontSize = 12 };
                Label ScanStateTitle = new Label {Text="扫描状态", Margin = new Thickness(15, 0, 0, 0), FontSize = 12 };
                Label Count = new Label {
                    Text = q.F_XAY_Count.ToString(),
                    Margin = new Thickness(15, 0, 0, 5),
                    TextColor = Color.Black,
                    FontSize = 12
                };
                Label Unit = new Label {
                    Text = q.F_XAY_Mark,
                    Margin = new Thickness(15, 0, 0, 5),
                    TextColor = Color.Black,
                    FontSize = 12
                };
                Label ScanState = new Label {
                    Text = q.F_XAY_IsScan?"是":"否",
                    Margin = new Thickness(15, 0, 0, 5),
                    TextColor = Color.Black,
                    FontSize = 12
                };
                FMaterial.Text = q.F_XAY_FMaterial.Name[0].Value;
                foreach (var c in q.XAY_t_StockUpOrderSubEntry)
                {

                    Label CMaterial = new Label {
                        FontSize = 11
                    };
                    CMaterial.Text = string.Format("{0} {1} {2}", c.F_XAY_CMaterial.Name[0].Value,q.F_XAY_SpareParts? c.F_XAY_CQty:c.F_XAY_SubCount, c.F_XAY_SubUnit);
                    cLayout.Children.Add(CMaterial);
                }
                bool clickstate = true;
                animation.PlayProgressSegment(0.37f, 0.5f);
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
                if (q.XAY_t_StockUpOrderSubEntry.Count != 0)
                    title.Children.Add(animation);
                subtitle.Children.Add(CountTitle,0,0);
                subtitle.Children.Add(UnitTitle,1,0);
                subtitle.Children.Add(ScanStateTitle,2,0);
                subtitle.Children.Add(Count, 0, 1);
                subtitle.Children.Add(Unit, 1, 1);
                subtitle.Children.Add(ScanState, 2, 1);                
                sLayout.Children.Add(title);
                sLayout.Children.Add(subtitle);
                sLayout.Children.Add(cLayout);
                QrResultstacklayout.Children.Add(sLayout);
            }


        }
    }
}
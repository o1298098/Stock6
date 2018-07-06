using Lottie.Forms;
using Stock6.action;
using Stock6.ViewModels;
using Stock6.Views.StockUp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Stock6.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class StockUpPage : ContentPage
	{
        private static ObservableCollection<StockUpPageModel> model;
        public StockUpPage ()
		{
			InitializeComponent ();
            scanbtn.Clicked += async delegate {
                await Navigation.PushAsync(new StockUpStep());
            };
            model= new ObservableCollection<StockUpPageModel>();
            listview.ItemsSource = model;
            listview.IsPullToRefreshEnabled = true;
            listview.IsRefreshing = true;
            DataTemplate template = new DataTemplate(() => {
                Grid grid = new Grid()
                {
                    Margin = new Thickness(0, 5, 0, 5),
                    BackgroundColor = Color.White
                };
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(6, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(6, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(30) });
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(20) });
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(20) });
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(20) });
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(20) });
                grid.ColumnSpacing = 0;
                grid.RowSpacing = 0;
                AnimationView animation = new AnimationView
                {
                    Animation = "more.json",
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    VerticalOptions=LayoutOptions.CenterAndExpand,
                    WidthRequest = 30,
                    HeightRequest = 30,

                };
                Style titlestyle = new Style(typeof(Label)) {
                    Setters = {
                        new Setter{Property=Label.FontSizeProperty,Value=11 },
                        new Setter{Property=Label.VerticalOptionsProperty,Value=LayoutOptions.End },
                        new Setter{Property=Label.HorizontalOptionsProperty,Value=LayoutOptions.Start },
                        new Setter{Property=Label.MarginProperty,Value=new Thickness(15,0,0,0) },
                    }
                };
                Style valuestyle = new Style(typeof(Label))
                {
                    Setters = {
                        new Setter{Property=Label.FontSizeProperty,Value=11 },
                        new Setter{Property=Label.VerticalOptionsProperty,Value=LayoutOptions.Start },
                        new Setter{Property=Label.HorizontalOptionsProperty,Value=LayoutOptions.Start },
                        new Setter{Property=Label.MarginProperty,Value=new Thickness(15,0,0,0) },
                    }
                };
                Label BillNoTitle = new Label { Text="备货单号",Style= titlestyle };
                Label CustomTitle = new Label { Text = "客户", Style = titlestyle };
                Label PhoneTitle = new Label { Text = "联系电话", Style = titlestyle };
                Label LogisticsTitle = new Label { Text = "物流公司", Style = titlestyle };
                Label BillNo = new Label { Style = valuestyle };
                Label Custom = new Label { Style = valuestyle };
                Label Phone = new Label { Style = valuestyle };
                Label Logistics = new Label { Style = valuestyle };
                BillNo.SetBinding(Label.TextProperty, new Binding("BillNo"));
                Custom.SetBinding(Label.TextProperty, new Binding("Custom"));
                Phone.SetBinding(Label.TextProperty, new Binding("Phone"));
                Logistics.SetBinding(Label.TextProperty, new Binding("Logistics"));
                animation.SetBinding(AnimationView.ClassIdProperty, new Binding("Id"));
                bool clickstate = false;
                animation.OnClick += delegate
                {
                    if (!clickstate)
                    {
                        string content = "{\"FormId\":\"9d0a72f2a1104fe1881969ad5a1fc22d\",\"FieldKeys\":\"F_XAY_FMATERIAL.FName,F_XAY_FQTY,F_XAY_MARK\",\"FilterString\":\"FID =" + animation.ClassId + " and FBillStatus='A'\",\"OrderString\":\"\",\"TopRowCount\":\"0\",\"StartRow\":\"0\",\"Limit\":\"0\"}";
                        string[] lists = Jsonhelper.JsonToString(content);
                        if (lists != null)
                        {
                            animation.PlayProgressSegment(0.15f, 0.3f);
                            for (int i = 0; i < lists.Count(); i++)
                            {
                                string billstring = lists[i].Replace("[", "");
                                billstring = billstring.Replace("]", "");
                                string[] bill = billstring.Split(',');
                                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1,GridUnitType.Auto) });
                                int rowcount = grid.RowDefinitions.Count;
                                Label addrowlabel = new Label { Text =string.Format("{0} {1}{2}", bill[0] , (Convert.ToSingle( bill[1])).ToString("f0") , bill[2]) ,FontSize=10,Margin=new Thickness(15,0,0,2) };
                                grid.Children.Add(addrowlabel, 0, rowcount - 1);
                                Grid.SetColumnSpan(addrowlabel, 3);


                            }
                            clickstate = !clickstate;
                        }

                    }
                    else {
                        for (int i = grid.RowDefinitions.Count - 1; i >=4; i--)
                        {
                            grid.Children.RemoveAt(i+5);
                            grid.RowDefinitions.RemoveAt(i);
                        }
                        animation.PlayProgressSegment(0.37f, 0.5f);
                        clickstate = !clickstate;
                    }
                };
                grid.Children.Add(BillNoTitle, 0, 0);
                grid.Children.Add(CustomTitle, 0, 2);
                grid.Children.Add(PhoneTitle, 1, 2);
                grid.Children.Add(LogisticsTitle, 1, 0);
                grid.Children.Add(BillNo, 0, 1);
                grid.Children.Add(Custom, 0, 3);
                grid.Children.Add(Phone, 1, 3);
                grid.Children.Add(Logistics, 1, 1);
                grid.Children.Add(animation, 2, 0);
                Grid.SetRowSpan(animation, 4);
                return new ViewCell
                {
                    View = grid
                };
            });
            listview.ItemTemplate = template;
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += delegate {
                    modelrefresh();
            };
            worker.RunWorkerCompleted += delegate {
                listview.EndRefresh();
            };
            listview.Refreshing += delegate {
                modelrefresh();
                listview.IsRefreshing = false;
            };
            listview.ItemTapped += async (sender, e) => {

                ScanPage scanPage = new ScanPage(3);
                scanPage.BindingContext = e.Item;
                await Navigation.PushAsync(scanPage);
            };
            worker.RunWorkerAsync();
        }
        private void modelrefresh()
        {
            model.Clear();
            string content = "{\"FormId\":\"9d0a72f2a1104fe1881969ad5a1fc22d\",\"FieldKeys\":\"FBillNo,F_XAY_Custom,F_XAY_Phone,F_XAY_Logistics.FSimpleName,FID\",\"FilterString\":\"F_XAY_LogisticsNum ='' and FBillStatus='A'\",\"OrderString\":\"\",\"TopRowCount\":\"0\",\"StartRow\":\"0\",\"Limit\":\"0\"}";
            string[] lists = Jsonhelper.JsonToString(content);
            if (lists != null)
            {
                for (int i = 0; i < lists.Count(); i++)
                {
                    string billstring = lists[i].Replace("[", "");
                    billstring = billstring.Replace("]", "");
                    string[] bill = billstring.Split(',');
                    model.Add(
                        new StockUpPageModel
                        {
                            BillNo = bill[0].ToString(),
                            Custom = bill[1].ToString(),
                            Phone = bill[2].ToString(),
                            Logistics = bill[3].ToString(),
                            Id = bill[4].ToString()
                        });
                }

            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            var scan = from q in model
                       where q.isscan == true
                       select q;
            if (scan.Count() > 0)
            {
               for(int i=scan.Count()-1;i>=0; i--)
                { model.Remove(scan.ElementAt(i)); }
                    
            }
        }
    }
}
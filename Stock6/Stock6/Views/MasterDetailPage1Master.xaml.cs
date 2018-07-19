using Stock6.CustomControls;
using Stock6.Views.StockUp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Stock6.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterDetailPage1Master : ContentPage
    {
        public ListView ListView;
        public CircleImage LoginBtn;
        public Image OptionBtn;
        public Label usernameLabel;

        public MasterDetailPage1Master()
        {
            InitializeComponent();

            BindingContext = new MasterDetailPage1MasterViewModel();
            ListView = MenuItemsListView;
            LoginBtn = Userimg;
            OptionBtn = Option;
            usernameLabel = Username;
        }

        class MasterDetailPage1MasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<MasterDetailPage1MenuItem> MenuItems { get; set; }
            
            public MasterDetailPage1MasterViewModel()
            {
                MenuItems = new ObservableCollection<MasterDetailPage1MenuItem>(new[]
                {
                    new MasterDetailPage1MenuItem { Id = 0, Title = "备货扫描",TargetType=typeof(StockUpPage),Icon="apple.png" },
                    new MasterDetailPage1MenuItem { Id = 1, Title = "图片上传",TargetType=typeof(PackingPage),Icon="mango.png"},
                    new MasterDetailPage1MenuItem { Id = 2, Title = "备货相册",TargetType=typeof(SearchPhoto),Icon="Thaumatin.png" },
                    new MasterDetailPage1MenuItem { Id = 3, Title = "组装拆卸",Icon="mangosteen.png" },
                    //new MasterDetailPage1MenuItem { Id = 4, Title = "待定",TargetType=typeof(TestWebview),Icon="emoji_shock.json" },
                });
            }
            
            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Stock6.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterDetailPage1 : MasterDetailPage
    {
        public MasterDetailPage1()
        {
            InitializeComponent();
            MasterPage.ListView.ItemSelected += ListView_ItemSelected;
            TapGestureRecognizer recognizer = new TapGestureRecognizer();
            recognizer.Tapped += async delegate
            {
                await Navigation.PushAsync(new AsseccPage());
            };
            MasterPage.LoginBtn.GestureRecognizers.Add(recognizer);
            TapGestureRecognizer recognizer2 = new TapGestureRecognizer();
            recognizer2.Tapped += async delegate {
                await Navigation.PushAsync(new OptionPage());
            };
            MasterPage.OptionBtn.GestureRecognizers.Add(recognizer2);
        }

        

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MasterDetailPage1MenuItem;
            if (item == null)
                return;

            var page = (Page)Activator.CreateInstance(item.TargetType);
            page.Title = item.Title;

            Detail = new NavigationPage(page);
            IsPresented = false;

            MasterPage.ListView.SelectedItem = null;
        }
    }
}
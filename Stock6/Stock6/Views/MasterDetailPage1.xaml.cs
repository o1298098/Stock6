using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
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
                if (App.Context.user.token == "")
                {
                    await Navigation.PushAsync(new AccessPage());
                }
                else
                {
                    await Navigation.PushAsync(new OptionPage());
                }
                
            };
            MasterPage.LoginBtn.GestureRecognizers.Add(recognizer);
            TapGestureRecognizer recognizer2 = new TapGestureRecognizer();
            recognizer2.Tapped += async delegate {
                await Navigation.PushAsync(new OptionPage());
            };
            MasterPage.OptionBtn.GestureRecognizers.Add(recognizer2);
            MasterPage.usernameLabel.BindingContext = App.Context.user;
            MasterPage.usernameLabel.SetBinding(Label.TextProperty, new Binding("name"));
            MasterPage.usernameLabel.BackgroundColor = Color.FromRgba(255, 255, 255, 0.8);
            MasterPage.usernameLabel.BindingContextChanged += delegate
            {
                Device.BeginInvokeOnMainThread(()=> { MasterPage.usernameLabel.Text = App.Context.user.name; });
                
            };
            this.Appearing += async delegate {               
                    var cameraStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
                    var storageStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);

                    if (cameraStatus != PermissionStatus.Granted || storageStatus != PermissionStatus.Granted)
                    {
                        var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Camera, Permission.Storage });
                        cameraStatus = results[Permission.Camera];
                        storageStatus = results[Permission.Storage];
                    }
               
            };
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
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
	public partial class AccessPage : ContentPage
	{
		public AccessPage()
		{
			InitializeComponent ();
            animationView.HorizontalOptions=LayoutOptions.FillAndExpand;
            animationView.VerticalOptions = LayoutOptions.FillAndExpand;          
            login.Clicked += async delegate
            {
                var loginPage = new LoginPage();
                await Navigation.PushAsync(loginPage);
            };
            register.Clicked += async delegate
            {
                var registerPage = new RegisterPage();
                await Navigation.PushAsync(registerPage);
            };
        }
        protected override void OnAppearing()
        {

        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            GC.Collect();
        }
    }
}
using Stock6.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace Stock6.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TestWebview : ContentPage
	{
		public TestWebview ()
		{
			InitializeComponent ();
            Xamarin.Forms.Application.Current.On<Xamarin.Forms.PlatformConfiguration.Android>().UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Unspecified);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (Device.RuntimePlatform== "Android")
            {
                DependencyService.Get<IKeyboardHelper>().HideKeyboard();
                
            }
        }
    }
}
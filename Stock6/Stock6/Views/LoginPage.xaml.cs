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
	public partial class LoginPage : ContentPage
	{
		public LoginPage ()
		{
			InitializeComponent ();
            login.Style = new Style(typeof(Button))
            {
                Setters = { new Setter { Property = Button.CornerRadiusProperty, Value = 40 } }
            };
            register.Style = new Style(typeof(Button))
            {
                Setters = { new Setter { Property = Button.CornerRadiusProperty, Value = 40 } }
            };
            login.Clicked += delegate
            {
                animationView.Play();
            };
		}
	}
}
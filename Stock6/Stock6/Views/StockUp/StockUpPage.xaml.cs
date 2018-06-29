using Stock6.Views.StockUp;
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
	public partial class StockUpPage : ContentPage
	{
		public StockUpPage ()
		{
			InitializeComponent ();
            scanbtn.Clicked += async delegate {
                await Navigation.PushAsync(new StockUpStep());
            };
		}
	}
}
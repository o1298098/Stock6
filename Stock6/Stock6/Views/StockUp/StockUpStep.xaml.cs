using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Stock6.Views.StockUp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class StockUpStep : ContentPage
	{
		public StockUpStep ()
		{
			InitializeComponent ();
            scanBarAnimation.OnClick += delegate
            {
                scanstacklayout.IsVisible = false;
                resultstacklayout.IsVisible = true;
            };
		}
	}
}
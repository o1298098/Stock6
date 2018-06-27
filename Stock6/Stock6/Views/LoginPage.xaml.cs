﻿using System;
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
            go.Clicked +=async delegate
            {
                App.Current.MainPage = new NavigationPage(new MasterDetailPage1());
                var pages = (NavigationPage)this.Parent;
                Navigation.InsertPageBefore(new MasterDetailPage1(), pages.RootPage);                
                await Navigation.PopAsync(true);
            };
		}
	}
}
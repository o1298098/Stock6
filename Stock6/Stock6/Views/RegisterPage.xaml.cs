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
	public partial class RegisterPage : ContentPage
	{
		public RegisterPage ()
		{
			InitializeComponent ();
            Grid grid = new Grid
            {
                RowSpacing=20
            };
            grid.ColumnDefinitions = new ColumnDefinitionCollection{
               new ColumnDefinition{Width=new GridLength(20) },
               new ColumnDefinition{Width=new GridLength(1,GridUnitType.Star) },
               new ColumnDefinition{Width=new GridLength(20) },
            };
            grid.RowDefinitions = new RowDefinitionCollection {
                 new RowDefinition{Height=new GridLength(200) },
                new RowDefinition{Height=new GridLength(50) },
                new RowDefinition{Height=new GridLength(50) },
                new RowDefinition{Height=new GridLength(50) },
                new RowDefinition{Height=new GridLength(50) },
            };
            Entry Nametext = new Entry
            {
                Placeholder="用户名"
            };
            Entry password = new Entry
            {
                IsPassword = true,
                Placeholder="密码"
            };
            Entry passwordt = new Entry
            {
                IsPassword = true,
                Placeholder="确认密码"
            };
            Button submit = new Button
            {
                Text = "SIGN UP",
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center

            };
            submit.Clicked += async delegate
            {
                await Navigation.PopAsync();
            };
            grid.Children.Add(Nametext, 1, 1);
            grid.Children.Add(password, 1, 2);
            grid.Children.Add(passwordt, 1, 3);
            grid.Children.Add(submit, 1, 4);
            Content = grid;
        }
	}
}
﻿using System;
using System.Threading.Tasks;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Stock6.Models;
using Stock6.Views;
using Xamarin.Forms;

namespace Stock6
{
	public partial class App : Application
	{
        public static  Context Context;
        public App ()
		{
#if DEBUG
            LiveReload.Init();
#endif
            InitializeComponent();
            Context = new Context();
            MainPage =new NavigationPage( new MasterDetailPage1());
           
        }

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}

    }
}

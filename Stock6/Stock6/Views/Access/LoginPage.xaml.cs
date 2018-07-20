using Lottie.Forms;
using Stock6.apiHelper;
using Stock6.Models;
using Stock6.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Stock6.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
        bool finsh;
        public LoginPage ()
		{
			InitializeComponent ();
            bool s = false;
            finsh = false;
            loadingAnimation.OnFinish += async delegate {
                if (!finsh)
                {
                    loadingAnimation.PlayProgressSegment(0, 0.59f);
                }
                else
                {
                    if (!s)
                    {
                        loadingAnimation.PlayProgressSegment(0.59f, 1);
                        s = true;
                    }
                    else
                    {
                        loadingAnimation.IsVisible = false;
                        await Navigation.PopToRootAsync();
                    }


                }
            };
            submit.Clicked += Submit_Clicked;
		}

        private void Submit_Clicked(object sender, EventArgs e)
        {
            loadingAnimation.IsVisible = true;
            loadingAnimation.PlayProgressSegment(0, 0.59f);
            if (!string.IsNullOrWhiteSpace(password.Text))
            {
                string json = string.Format("{{ \"name\": \"{0}\",\"password\": \"{1}\"}}", nametext.Text, password.Text);
                HttpClient httpClient = new HttpClient();
                httpClient.Content = json;
                httpClient.Url = "http://outlet.ergochefcn.com/candaapi/api/Login";
                Task.Run(() =>
                {
                    string callback = httpClient.sendPost();
                    callback = callback.Replace("\"", "");
                    if (callback == "err")
                    {
                    Device.BeginInvokeOnMainThread(() => {
                        DependencyService.Get<IToast>().LongAlert("用户名或密码错误");
                        loadingAnimation.IsVisible = false;
                    });                       
                    }
                    else
                    {
                        App.Context.user.name = nametext.Text;
                        App.Context.user.token = callback;
                        Preferences.Set("User", nametext.Text);
                        Preferences.Set("UserToken", callback);
                        finsh = true;
                    }
                });               
            }
            else
            {
                DependencyService.Get<IToast>().LongAlert("密码为空");
                loadingAnimation.IsVisible = false;              
            }
        }
    }
}
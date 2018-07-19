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
                        loadingAnimation.Pause();
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
            Device.BeginInvokeOnMainThread(()=> {
                if (password.Text != "")
                {
                    string json = string.Format("{{ \"name\": \"{0}\",\"password\": \"{1}\"}}", nametext.Text, password.Text);
                    HttpClient httpClient = new HttpClient();
                    httpClient.Content = json;
                    httpClient.Url = "http://outlet.ergochefcn.com/candaapi/api/Login";
                    string callback = httpClient.sendPost();
                    callback = callback.Replace("\"", "");
                    if (callback == "err")
                    {
                        DependencyService.Get<IToast>().LongAlert("用户名或密码错误");
                        loadingAnimation.IsVisible = false;
                    }
                    else
                    {
                        User user = new User();
                        user.name = nametext.Text;
                        user.token = callback;
                        App.Context.user = user;
                        Preferences.Set("User", nametext.Text);
                        Preferences.Set("UserToken", callback);
                        finsh = true;
                    }

                }
                else
                {
                    DependencyService.Get<IToast>().LongAlert("密码为空");
                    loadingAnimation.IsVisible = false;
                }
            });
            
        }      
    }
}
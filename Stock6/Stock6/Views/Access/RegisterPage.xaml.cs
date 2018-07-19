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
	public partial class RegisterPage : ContentPage
	{
        bool finsh;
		public RegisterPage ()
		{
			InitializeComponent ();
            finsh = false;
            passwordt.TextChanged += Passwordt_TextChanged;
            submit.Clicked += Submit_Clicked;
            bool s = false;
            loadingAnimation.OnFinish +=async delegate {
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
        }

      

       

        private void Submit_Clicked(object sender, EventArgs e)
        {
            loadingAnimation.IsVisible = true;
            loadingAnimation.PlayProgressSegment(0,0.59f);
            Device.BeginInvokeOnMainThread(() =>
            {
                if (string.IsNullOrWhiteSpace(nametext.Text) || string.IsNullOrWhiteSpace(password.Text))
                {
                    DependencyService.Get<IToast>().LongAlert("请填完信息");
                    loadingAnimation.IsVisible = false;
                    return;
                }
                if (password.Text == passwordt.Text)
                {
                    string json = string.Format("{{ \"name\": \"{0}\",\"password\": \"{1}\"}}", nametext.Text, password.Text);
                    HttpClient httpClient = new HttpClient();
                    httpClient.Content = json;
                    httpClient.Url = "http://outlet.ergochefcn.com/candaapi/api/Register";
                    string callback = httpClient.sendPost();
                    callback = callback.Replace("\"", "");
                    if (callback == "1")
                    {
                        DependencyService.Get<IToast>().LongAlert("用户名已被注册");
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
                    DependencyService.Get<IToast>().LongAlert("密码不匹配");
                    loadingAnimation.IsVisible = false;
                }
            });           
        }

        private void Passwordt_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (password.Text != passwordt.Text)
            {
                VisualStateManager.GoToState(passwordt, "Wrong");
            }
            else
            {
                VisualStateManager.GoToState(passwordt, "Normal");
            }
        }       
    }
}
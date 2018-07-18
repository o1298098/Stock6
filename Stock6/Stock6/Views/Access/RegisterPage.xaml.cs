using Lottie.Forms;
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
	public partial class RegisterPage : ContentPage
	{
        bool finsh;
		public RegisterPage ()
		{
			InitializeComponent ();
            finsh = false;
            passwordt.TextChanged += Passwordt_TextChanged;
            submit.Clicked += Submit_Clicked;            
            loadingAnimation.OnFinish +=delegate {
                if (!finsh)
                {
                    loadingAnimation.PlayProgressSegment(0, 0.59f);
                }
                else
                {
                    loadingAnimation.PlayProgressSegment(0.59f, 1);
                    loadingAnimation.Pause();
                }
            };
        }

      

       

        private void Submit_Clicked(object sender, EventArgs e)
        {
            loadingAnimation.IsVisible = true;
            loadingAnimation.PlayProgressSegment(0,0.59f);
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
                finsh = true;
            }
        }
    }
}
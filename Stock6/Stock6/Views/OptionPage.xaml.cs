using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Stock6.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class OptionPage : ContentPage
	{
		public OptionPage ()
		{
			InitializeComponent ();
                userlabel.Title= Preferences.Get("User", "Guest");
            FtpUrlEntry.ValueText = Preferences.Get("FtpURL", "ftp://canda.f3322.net:8066/STOCKPIC/");
                FtpUserEntry.ValueText = Preferences.Get("FtpUser", "administrator");
                FtpPasswordEntry.ValueText = Preferences.Get("FtpPassword", "ergochef@2018");
                KDUrlEntry.ValueText = Preferences.Get("KDURL", "http://canda.f3322.net:8003/K3CLOUD/");
                KDDataCenterIDEntry.ValueText = Preferences.Get("KDDataCenterID", "59a12c8ba824d2");
                KDUserEntry.ValueText = Preferences.Get("KDUser", "kingdee");
                KDPasswordEntry.ValueText = Preferences.Get("KDPassword", "kd!123456");
                validateNum.On=Convert.ToBoolean(Preferences.Get("ValidateLogisticsNum", "false"));
                ScanHardMode.On= Convert.ToBoolean(Preferences.Get("ScanHardMode", "false"));
            Savebtn.Clicked += delegate {
                 Preferences.Set("FtpURL", FtpUrlEntry.ValueText);
                Preferences.Set("FtpUser", FtpUserEntry.ValueText);
                Preferences.Set("FtpPassword", FtpPasswordEntry.ValueText);
                Preferences.Set("KDURL", KDUrlEntry.ValueText);
                Preferences.Set("KDUser", KDUserEntry.ValueText);
                Preferences.Set("KDPassword", KDPasswordEntry.ValueText);
                Preferences.Set("ValidateLogisticsNum", validateNum.On.ToString());
                Preferences.Set("ScanHardMode", ScanHardMode.On.ToString());
                DependencyService.Get<Services.IToast>().LongAlert("保存成功");
                App.Context = new Models.Context();
             };
            Logout.Tapped += delegate {
                App.Context.user.name = "Guest";
                App.Context.user.token = "";
                Preferences.Remove("User");
                Preferences.Remove("UserToken");
                userlabel.Title = Preferences.Get("User", "Guest");
            };
        }
	}
}
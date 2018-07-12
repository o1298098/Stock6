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
          
            Task.Run(async () => {
                string FtpURL = await SecureStorage.GetAsync("FtpURL");
                string FtpUser = await SecureStorage.GetAsync("FtpUser");
                string FtpPassword = await SecureStorage.GetAsync("FtpPassword");
                string KDURL = await SecureStorage.GetAsync("KDURL");
                string KDDataCenterID = await SecureStorage.GetAsync("KDDataCenterID");
                string KDUser = await SecureStorage.GetAsync("KDUser");
                string KDPassword = await SecureStorage.GetAsync("KDPassword");
                FtpUrlEntry.ValueText = string.IsNullOrWhiteSpace(FtpURL) ? "ftp://canda.f3322.net:8066/STOCKPIC/" : FtpURL;
                FtpUserEntry.ValueText = string.IsNullOrWhiteSpace(FtpUser) ? "administrator" : FtpUser;
                FtpPasswordEntry.ValueText = string.IsNullOrWhiteSpace(FtpPassword) ? "ergochef@2018" : FtpPassword;
                KDUrlEntry.ValueText= string.IsNullOrWhiteSpace(KDURL) ? "http://canda.f3322.net:8003/K3CLOUD/" : KDURL;
                KDDataCenterIDEntry.ValueText = string.IsNullOrWhiteSpace(KDDataCenterID) ? "59a12c8ba824d2" : KDDataCenterID;
                KDUserEntry.ValueText = string.IsNullOrWhiteSpace(KDUser) ? "kingdee" : KDUser;
                KDPasswordEntry.ValueText = string.IsNullOrWhiteSpace(KDPassword) ? "kd!123456" : KDPassword;
            });
            Savebtn.Clicked += async delegate {
                await SecureStorage.SetAsync("FtpURL", FtpUrlEntry.ValueText);
                await SecureStorage.SetAsync("FtpUser", FtpUserEntry.ValueText);
                await SecureStorage.SetAsync("FtpPassword", FtpPasswordEntry.ValueText);
                await SecureStorage.SetAsync("KDURL", KDUrlEntry.ValueText);
                await SecureStorage.SetAsync("KDUser", KDUserEntry.ValueText);
                await SecureStorage.SetAsync("KDPassword", KDPasswordEntry.ValueText);
                DependencyService.Get<Services.IToast>().LongAlert("保存成功");
                App.Context = new Models.Context();
             };
        }
	}
}
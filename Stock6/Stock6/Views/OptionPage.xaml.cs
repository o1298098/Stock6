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
            TableView tableView = new TableView
            {
                Intent = TableIntent.Settings,
                Root = new TableRoot()
            };

            TableSection section = new TableSection();
            TableSection section1 = new TableSection();
            TableSection section2 = new TableSection();
            EntryCell cellFtpURL = new EntryCell { Label = "FtpURL"};
            EntryCell cellFtpUser = new EntryCell { Label = "FtpUser"};
            EntryCell cellFtpPassword = new EntryCell { Label = "FtpPassword"};
            Task.Run(async () => {
                string FtpURL = await SecureStorage.GetAsync("FtpURL");
                string FtpUser = await SecureStorage.GetAsync("FtpUser");
                string FtpPassword = await SecureStorage.GetAsync("FtpPassword");
                cellFtpURL.Text = string.IsNullOrWhiteSpace(FtpURL) ? "ftp://canda.f3322.net:8066/STOCKPIC/" : FtpURL;
                cellFtpUser.Text = string.IsNullOrWhiteSpace(FtpUser) ? "administrator" : FtpUser;
                cellFtpPassword.Text = string.IsNullOrWhiteSpace(FtpPassword) ? "ergochef@2018" : FtpPassword;
            });
            section.Add(cellFtpURL);
            section1.Add(cellFtpUser);
            section2.Add(cellFtpPassword);
            tableView.Root.Add(section);
            tableView.Root.Add(section1);
            tableView.Root.Add(section2);
            Savebtn.Clicked += async delegate {
                await SecureStorage.SetAsync("FtpURL", cellFtpURL.Text);
                await SecureStorage.SetAsync("FtpUser", cellFtpUser.Text);
                await SecureStorage.SetAsync("FtpPassword", cellFtpPassword.Text);
                DependencyService.Get<Services.IToast>().LongAlert("保存成功");
                App.Context = new Models.Context();
             };
            Content = tableView;
        }
	}
}
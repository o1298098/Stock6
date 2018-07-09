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
            Task.Run(async () => {
                EntryCell cellURL = new EntryCell { Label = "FtpURL", Text = await SecureStorage.GetAsync("FtpURL"), };
                EntryCell cellUser = new EntryCell { Label = "FtpUser", Text = await SecureStorage.GetAsync("FtpUser"), };
                EntryCell cellPassword = new EntryCell { Label = "FtpPassword", Text = await SecureStorage.GetAsync("FtpPassword") };
                section.Add(cellURL);
                section1.Add(cellUser);
                section2.Add(cellPassword);
                tableView.Root.Add(section);
                tableView.Root.Add(section1);
                tableView.Root.Add(section2);
                Savebtn.Clicked += async delegate {
                    await SecureStorage.SetAsync("FtpURL", cellURL.Text);
                    await SecureStorage.SetAsync("FtpUser", cellUser.Text);
                    await SecureStorage.SetAsync("FtpPassword", cellPassword.Text);
                    DependencyService.Get<Services.IToast>().LongAlert("保存成功");
                    App.Context = new Models.Context();

                };
            });

            Content = tableView;
        }
	}
}
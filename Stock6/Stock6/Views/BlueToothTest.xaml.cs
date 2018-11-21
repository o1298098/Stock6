using Plugin.BluetoothLE;
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
	public partial class BlueToothTest : ContentPage
	{
		public BlueToothTest ()
		{
			InitializeComponent ();
		}
        IScanResult ScanResult;
        IDevice device;
        byte[] bytes;
        string Result;
        private async void scanbtn_Clicked(object sender, EventArgs e)
        {
            CrossBleAdapter.Current.Scan().Subscribe(scanResult => { ScanResult = scanResult; });

            // Once finding the device/scanresult you want
            ScanResult.Device.Connect();
            device = ScanResult.Device;

            device.WhenAnyCharacteristicDiscovered().Subscribe(characteristic => {
                // read, write, or subscribe to notifications here
                var result = characteristic.Read(); // use result.Data to see response
                 characteristic.Write(bytes);

                characteristic.EnableNotifications();
                characteristic.WhenNotificationReceived().Subscribe(r => {
                    Result = r.ToString();
                });
            });
        }
       

        private async void listview_ItemSelectedAsync(object sender, ItemTappedEventArgs e)
        {
           
        }

        private async void gsbtn_Clicked(object sender, EventArgs e)
        {
           
        }

        private async void gcbtn_Clicked(object sender, EventArgs e)
        {
           
        }

        private async void rcbtn_Clicked(object sender, EventArgs e)
        {
           
        }
    }
}
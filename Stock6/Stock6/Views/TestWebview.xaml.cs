using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.Extensions;
using Stock6.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace Stock6.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TestWebview : ContentPage
	{
        IBluetoothLE ble;
        IAdapter adapter;
        IDevice device;
        ObservableCollection<IDevice> devices;
        public TestWebview ()
		{
			InitializeComponent ();
            ble = CrossBluetoothLE.Current;
            adapter = CrossBluetoothLE.Current.Adapter;
            devices = new ObservableCollection<IDevice>();
            listview.ItemsSource = devices;
            
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (Device.RuntimePlatform== "Android")
            {
                DependencyService.Get<IKeyboardHelper>().HideKeyboard();
               
            }
            var systemDevices = adapter.GetSystemConnectedOrPairedDevices();

            foreach (var device in systemDevices)
            {
                await adapter.ConnectToDeviceAsync(device);
            }
        }

        private async void scanbtn_Clicked(object sender, EventArgs e)
        {
            devices.Clear();
            adapter.ScanMode = Plugin.BLE.Abstractions.Contracts.ScanMode.Balanced;
            adapter.DeviceDiscovered += (s, a) =>
            {
                if(!devices.Contains(a.Device))
                    devices.Add(a.Device);
            };
            if (!ble.Adapter.IsScanning)
            {
                await adapter.StartScanningForDevicesAsync();
            }
            try
            {
                
            }
            catch { }
        }
        IList<ICharacteristic> Characteristics;
        ICharacteristic Characteristic;
        IDescriptor descriptor;
        IList<IDescriptor> descriptors;
        IList<IService> Services;
        IService Service;
        byte[] bytes;

        private async void listview_ItemSelectedAsync(object sender, ItemTappedEventArgs e)
        {
            device = listview.SelectedItem as IDevice;
            if (device != null)
            {
                await adapter.ConnectToDeviceAsync(device);
                
            }
        }

        private async void gsbtn_Clicked(object sender, EventArgs e)
        {
           if (device != null)
            Service = await device.GetServiceAsync(device.Id);
        }

        private async void gcbtn_Clicked(object sender, EventArgs e)
        {
            Guid guid = Guid.Parse("guid");
            if(Service!=null)
                Characteristic = await Service.GetCharacteristicAsync(Service.Id);
        }

        private async void rcbtn_Clicked(object sender, EventArgs e)
        {
                var bytes = await Characteristic.ReadAsync();
        }
    }
}
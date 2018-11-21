using Plugin.BLE.Abstractions.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Stock6.ViewModels
{
    public class BlueToothModel : INotifyPropertyChanged
    {
        private IDevice _device;
        public event PropertyChangedEventHandler PropertyChanged;
        public IDevice NativeDevice
        {
            get {
                return _device;
            }
            set {
                _device = value;
                OnPropertyChanged();

            }
        }
        protected void OnPropertyChanged([CallerMemberName] string caller="")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Stock6.Models
{
    public class User : INotifyPropertyChanged
    {
        private string _name;
        public string name { get=>_name; set {
                _name = value;
                OnPropertyChanged("name");
            } }
        public string icon { get; set; }
        public string password { get; set; }
        public string token { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

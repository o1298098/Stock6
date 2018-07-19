using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Stock6.Models
{
    public class User : INotifyPropertyChanged
    {
        public string name { get; set; }
        public string icon { get; set; }
        public string password { get; set; }
        public string token { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

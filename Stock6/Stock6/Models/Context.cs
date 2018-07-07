using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Stock6.Models
{
    public class Context : INotifyPropertyChanged
    {
        /// <summary>
        /// 金蝶服务器地址
        /// </summary>
         public string ServerUrl { get; }
        /// <summary>
        /// 金蝶数据中心ID
        /// </summary>
        public  string DataCenterId { get; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string User { get; }

        public Context()
        {
            ServerUrl = "http://canda.f3322.net:8003/k3cloud/";
            DataCenterId = "59a12c8ba824d2";//帐套Id 测试5ab05fc34e03d1 正式59a12c8ba824d2
        }         

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Stock6.Models
{
    public class Context : INotifyPropertyChanged
    {
        /// <summary>
        /// 金蝶服务器地址
        /// </summary>
        public string ServerUrl { get; private set; }
        /// <summary>
        /// 金蝶数据中心ID
        /// </summary>
        public string DataCenterId { get; private set; }
        /// <summary>
        /// 金蝶用户
        ///</summary>
        public string KDUser { get; private set; }
        /// <summary>
        /// 金蝶用户登录密码
        /// </summary>
        public string KDPassword { get; private set; }
        /// <summary>
        /// FTP服务器地址
        /// </summary>
        public string FtpUrl { get; private set; }
        /// <summary>
        /// FTP用户名
        /// </summary>
        public string FtpUser { get; private set; }
        /// <summary>
        /// FTP登陆密码
        /// </summary>
        public string FtpPassword { get; private set; }
        /// <summary>
        /// 扫描增强模式
        /// </summary>
        public bool ScanHardMode { get; private set; }

        public User user { get; set; }

        public  Context()
        {
            User _user = new User();
            _user.name = Preferences.Get("User", "Guest");
            _user.token = Preferences.Get("UserToken", "");
            user = _user;
            //ServerUrl = "http://canda.f3322.net:8003/k3cloud/";
            //DataCenterId = "59a12c8ba824d2";//帐套Id 测试5ab05fc34e03d1 正式59a12c8ba824d2
            FtpUrl = Preferences.Get("FtpURL", "ftp://canda.f3322.net:8066/STOCKPIC/");
            FtpUser = Preferences.Get("FtpUser", "administrator");
            FtpPassword = Preferences.Get("FtpPassword", "ergochef@2018");
            ServerUrl = Preferences.Get("KDURL", "http://canda.f3322.net:8003/k3cloud/");
            DataCenterId = Preferences.Get("KDDataCenterID", "59a12c8ba824d2");
            KDUser = Preferences.Get("KDUser", "kingdee");
            KDPassword = Preferences.Get("KDPassword", "kd!123456");
            ScanHardMode = Convert.ToBoolean(Preferences.Get("ScanHardMode", "false"));
         
        }         

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

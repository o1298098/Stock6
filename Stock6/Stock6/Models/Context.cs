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

        public  Context()
        {
            //ServerUrl = "http://canda.f3322.net:8003/k3cloud/";
            //DataCenterId = "59a12c8ba824d2";//帐套Id 测试5ab05fc34e03d1 正式59a12c8ba824d2
            Task.Run(async () =>
            {
                string furl = await SecureStorage.GetAsync("FtpURL");
                string fuser = await SecureStorage.GetAsync("FtpUser");
                string fpassword = await SecureStorage.GetAsync("FtpPassword");
                string kdurl = await SecureStorage.GetAsync("KDURL");
                string kddataCenterID = await SecureStorage.GetAsync("KDDataCenterID");
                string kduser = await SecureStorage.GetAsync("KDUser");
                string kdpassword = await SecureStorage.GetAsync("KDPassword");
                ServerUrl = string.IsNullOrWhiteSpace(kdurl) ? "http://canda.f3322.net:8003/k3cloud/" : kdurl;
                DataCenterId = string.IsNullOrWhiteSpace(kddataCenterID) ? "59a12c8ba824d2" : kddataCenterID;
                KDUser = string.IsNullOrWhiteSpace(kduser) ? "kingdee" : kduser;
                KDPassword = string.IsNullOrWhiteSpace(kdpassword) ? "kd!123456" : kdpassword;
                FtpUrl = string.IsNullOrEmpty(furl) ? "ftp://canda.f3322.net:8066/STOCKPIC/" : furl;
                FtpUser = string.IsNullOrEmpty(fuser) ? "administrator" : fuser;
                FtpPassword = string.IsNullOrEmpty(fpassword) ? "ergochef@2018" : fpassword;
            });
        }         

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

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
        private string _FtpUrl;
        private string _FtpUser;
        private string _FtpPassword;
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
        /// <summary>
        /// FTP服务器地址
        /// </summary>
        public string FtpUrl { get=>_FtpUrl; }
        /// <summary>
        /// FTP用户名
        /// </summary>
        public string FtpUser { get=>_FtpUser; }
        /// <summary>
        /// FTP登陆密码
        /// </summary>
        public string FtpPassword { get=>_FtpPassword; }

        public Context()
        {
            ServerUrl = "http://canda.f3322.net:8003/k3cloud/";
            DataCenterId = "59a12c8ba824d2";//帐套Id 测试5ab05fc34e03d1 正式59a12c8ba824d2
            Task.Run(async ()=>{
                string furl= await SecureStorage.GetAsync("FtpURL");
                string fuser= await SecureStorage.GetAsync("FtpUser");
                string fpassword= await SecureStorage.GetAsync("FtpPassword");
                _FtpUrl = string.IsNullOrEmpty(furl) ? "ftp://canda.f3322.net:8066/STOCKPIC/" : furl;
                _FtpUser = string.IsNullOrEmpty(fuser) ? "administrator" : fuser;
                _FtpPassword = string.IsNullOrEmpty(fpassword) ? "ergochef@2018" : fpassword;
            });
        }         

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

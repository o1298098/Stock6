using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Stock6.Models
{
   public class StockUpBillModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string _billno;
        private string _name;
        private string _phone;
        private string _logistics;
        /// <summary>
        /// 备货单号
        /// </summary>
        public string billno {
            get{
                return _billno;
            }
            set {
                _billno = value;
                OnPropertyChanged("billno");
            }
        }
        /// <summary>
        /// 客户名称
        /// </summary>
        public string name {
            get {
                return _name;
            }
            set {
                _name = value;
                OnPropertyChanged("name");
            }
        }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string phone {
            get
            {
                return _phone;
            }
            set
            {
                _phone = value;
                OnPropertyChanged("phone");
            }
        }
        /// <summary>
        /// 物流公司
        /// </summary>
        public string logistics
        {
            get
            {
                return _logistics;
            }
            set
            {
                _logistics = value;
                OnPropertyChanged("logistics");
            }
        }

        public StockUpBillModel() { }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

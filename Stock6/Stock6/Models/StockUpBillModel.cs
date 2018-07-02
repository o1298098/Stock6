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
        public class XAY_StockUpOrderEntry
        {
            public int Id { get; set; }           
            /// <summary>
            /// 主物料
            /// </summary>
            public FMaterial F_XAY_FMaterial { get; set; }
            /// <summary>
            /// 数量
            /// </summary>
            public int F_XAY_FQty { get; set; }
            /// <summary>
            /// 件数
            /// </summary>
            public int F_XAY_Count { get; set; }
            /// <summary>
            /// 单位
            /// </summary>
            public int F_XAY_Mart { get; set; }

        }
        public class FMaterial
        {
            /// <summary>
            /// ID
            /// </summary>
            public int Id { get; set; }
            public Name Name { get; set; }
            public string Number { get; set; }

        }
        public class Name
        {
            public string Value { get; set; }

        }

        public StockUpBillModel() { }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

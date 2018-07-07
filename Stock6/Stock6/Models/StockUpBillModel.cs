using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Newtonsoft.Json;

namespace Stock6.Models
{
   public class StockUpBillModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string _FBillNo;
        private string _F_XAY_Custom;
        private string _F_XAY_Phone;
        private Logistics _F_XAY_Logistics;
        private ObservableCollection<StockUpOrderEntry> _XAY_StockUpOrderEntry;
        [JsonProperty("Id")]
        public int Id
        {
            get;set;
        }
            [JsonProperty("FBillNo")]
        /// <summary>
        /// 备货单号
        /// </summary>
        public string FBillNo
        {
            get{
                return _FBillNo;
            }
            set {
                _FBillNo = value;
                OnPropertyChanged("FBillNo");
            }
        }
        [JsonProperty("F_XAY_Custom")]
        /// <summary>
        /// 客户名称
        /// </summary>
        public string F_XAY_Custom
        {
            get {
                return _F_XAY_Custom;
            }
            set {
                _F_XAY_Custom = value;
                OnPropertyChanged("F_XAY_Custom");
            }
        }
        [JsonProperty("F_XAY_Phone")]
        /// <summary>
        /// 联系电话
        /// </summary>
        public string F_XAY_Phone
        {
            get
            {
                return _F_XAY_Phone;
            }
            set
            {
                _F_XAY_Phone = value;
                OnPropertyChanged("F_XAY_Phone");
            }
        }
        [JsonProperty("F_XAY_Logistics")]
        /// <summary>
        /// 物流公司
        /// </summary>
        public Logistics F_XAY_Logistics
        {
            get
            {
                return _F_XAY_Logistics;
            }
            set
            {
                _F_XAY_Logistics = value;
                OnPropertyChanged("logistics");
            }
        }
        public ObservableCollection<StockUpOrderEntry> XAY_StockUpOrderEntry
        {
            get
            {
                return _XAY_StockUpOrderEntry;
            }
            set
            {
                _XAY_StockUpOrderEntry = value;
                OnPropertyChanged("XAY_StockUpOrderEntry");
            }
        }
        public class StockUpOrderEntry
        {
            private decimal _F_XAY_FQty;
            private decimal _F_XAY_Count;
            public string Id { get; set; }           
            /// <summary>
            /// 主物料
            /// </summary>
            public Material F_XAY_FMaterial { get; set; }
            /// <summary>
            /// 数量
            /// </summary>
            public decimal F_XAY_FQty {
                get
                {
                    return Convert.ToInt32(_F_XAY_FQty);
                }
                set
                {
                    _F_XAY_FQty = value;
                }
            }            
            /// <summary>
            /// 件数
            /// </summary>
            public decimal F_XAY_Count {
                get
                {
                    return Convert.ToInt32(_F_XAY_Count);
                }
                set
                {
                    _F_XAY_Count = value;
                }
            }
            /// <summary>
            /// 单位
            /// </summary>
            public string F_XAY_Mark { get; set; }
            /// <summary>
            /// 是否套餐
            /// </summary>
            public bool F_XAY_Isgroup { get; set; }
            /// <summary>
            /// 是否散件
            /// </summary>
            public bool F_XAY_SpareParts { get; set; }
            /// <summary>
            /// 是否扫描
            /// </summary>
            public bool F_XAY_IsScan { get; set; }
            public List<StockUpOrderSubEntry> XAY_t_StockUpOrderSubEntry { get; set; }

        }
        public class Material
        {
            /// <summary>
            /// ID
            /// </summary>
            public int Id { get; set; }
            public List<Name> Name { get; set; }
            public string Number { get; set; }

        }
        public class Logistics
        {

            public string Id { get; set; }
            public string Number { get; set; }

            [JsonProperty("SimpleName")]
            public List<Name> SimpleName { get; set; }
        }
      
        public class Name
        {
            public string Value { get; set; }
        }

        public class StockUpOrderSubEntry
        {
            private decimal _F_XAY_CQty;
            private decimal _F_XAY_SubCount;
            public string id { get; set; }
            /// <summary>
            /// 子物料
            /// </summary>
            public Material F_XAY_CMaterial { get; set; }
            /// <summary>
            /// 数量
            /// </summary>
            public decimal F_XAY_CQty {
                get
                {
                    return Convert.ToInt32(_F_XAY_CQty);
                }
                set
                {
                    _F_XAY_CQty = value;
                }
            }
            /// <summary>
            /// 件数
            /// </summary>
            public decimal F_XAY_SubCount {
                get
                {
                    return Convert.ToInt32(_F_XAY_SubCount);
                }
                set
                {
                    _F_XAY_SubCount = value;
                }
            }
            /// <summary>
            /// 单位
            /// </summary>
            public string F_XAY_SubUnit { get; set; }
            /// <summary>
            /// 子件扫描
            /// </summary>
            public bool F_XAY_IsCScan { get; set; }
        }

        public StockUpBillModel() {
            F_XAY_Logistics = new Logistics();
            F_XAY_Logistics.SimpleName = new List<Name>();
            XAY_StockUpOrderEntry = new ObservableCollection<StockUpOrderEntry>();
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

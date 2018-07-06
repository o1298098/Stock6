using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stock6.ViewModels
{
    public class StockUpPageModel
    {

        [JsonProperty("FID")]
        public string Id { get; set; }

        [JsonProperty("FBillNo")]
        public string BillNo { get; set; }

        [JsonProperty("F_XAY_Custom")]
        public string Custom { get; set; }
        [JsonProperty("F_XAY_Phone")]
        public string Phone { get; set; }
        public string Logistics { get; set; }
        public bool isscan { get; set; }
        [JsonProperty("F_XAY_LogisticsNum")]
        public string LogisticsNum { get; set; }
       public StockUpPageModel(){ isscan = false; }
    }
    
}

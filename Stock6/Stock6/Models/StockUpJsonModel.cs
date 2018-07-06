using Newtonsoft.Json;
using Stock6.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stock6.Models
{
    class StockUpJsonModel
    {
        [JsonProperty("Creator")]
        public string Creator { get; set; }
        [JsonProperty("NeedUpDateFields")]
        public string[] NeedUpDateFields { get; set; }
        [JsonProperty("NeedReturnFields")]
        public string[] NeedReturnFields { get; set; }
        [JsonProperty("IsDeleteEntry")]
        public bool IsDeleteEntry { get; set; }
        [JsonProperty("SubSystemId")]
        public string SubSystemId { get; set; }
        [JsonProperty("Model")]
        public StockUpPageModel Model { get; set; }
        public StockUpJsonModel()
        {
            NeedUpDateFields = new string[] { "F_XAY_LogisticsNum" };
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stock6.Models
{
    public class KingdeeJsonResultModel
    {
        public class ResponseStatus
        {
            [JsonProperty("IsSuccess")]
            public string IsSuccess { get; set; }
        }
        public class result
        {
            [JsonProperty("ResponseStatus")]
            public ResponseStatus ResponseStatus
            { get; set; }
        }

        [JsonProperty("Result")]
        public result Result { get; set; }

        public KingdeeJsonResultModel()
        {
            Result = new result();
            Result.ResponseStatus = new ResponseStatus();
        }
    }
}

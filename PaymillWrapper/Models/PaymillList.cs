using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymillWrapper.Models
{
    public class PaymillList<T>
    {
        public PaymillList()
        {
            Data = new List<T>();
        }

        [JsonProperty("mode")]
        public string Mode { get; set; }

        [JsonProperty("data")]
        public List<T> Data { get; set; }

        [JsonProperty("data_count")]
        public int DataCount { get; set; }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymillWrapper.Utils
{
    internal class SingleResult<T>
    {
        [JsonProperty("data")]
        public T Data { get; set; }

        [JsonProperty("mode")]
        public string Mode { get; set; }
    }

    internal class MultipleResults<T>
    {
        [JsonProperty("data")]
        public List<T> Data { get; set; }

        [JsonProperty("mode")]
        public string Mode { get; set; }

        [JsonProperty("data_count")]
        public int Count { get; set; }
    }
}

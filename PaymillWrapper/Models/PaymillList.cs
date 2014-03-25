using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PaymillWrapper.Models
{
    [DataContract]
    public sealed class PaymillList<T>
    {
        public PaymillList()
        {
            Data = new List<T>();
        }

        [DataMember(Name = "mode")]
        public string Mode { get; set; }

        [DataMember(Name = "data")]
        public List<T> Data { get; set; }

        [DataMember(Name = "data_count")]
        public int DataCount { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PaymillWrapper.Models
{
    public sealed class PaymillList<T>
    {
        public PaymillList()
        {
            Data = new List<T>();
        }

        public List<T> Data { get; set; }

        [DataMember(Name = "data_count")]
        public int DataCount { get; set; }
    }
}

using Newtonsoft.Json;
using PaymillWrapper.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PaymillWrapper.Models
{
    public class Fee
    {
        public enum FeeType
        {
            APPLICATION
        };
        [DataMember(Name = "type")]
        public FeeType Type { get; set; }

        [DataMember(Name = "application")]
        public String Application { get; set; }

        [DataMember(Name = "payment")]
        public String Payment { get; set; }

        [DataMember(Name = "amount")]
        public int? Amount { get; set; }

        [DataMember(Name = "billed_at")]
        public DateTime? BilledAt { get; set; }

        [DataMember(Name = "currency")]
        public String Currency { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;


namespace PaymillWrapper.Models
{
    public class Fee
    {
        public enum FeeType
        {
            APPLICATION
        };
        [DataMember(Name = "Type")]
        public FeeType Type { get; set; }

        [DataMember(Name = "Application")]
        public String Application { get; set; }

        [DataMember(Name = "Payment")]
        public String Payment { get; set; }

        [DataMember(Name = "Amount")]
        public int Amount { get; set; }
    }
}

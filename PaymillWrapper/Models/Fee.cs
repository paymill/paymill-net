using Newtonsoft.Json;
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
        [JsonPropertyAttribute("Type")]
        public FeeType Type { get; set; }

        [JsonPropertyAttribute("Application")]
        public String Application { get; set; }

        [JsonPropertyAttribute("Payment")]
        public String Payment { get; set; }

        [JsonPropertyAttribute("Amount")]
        public int Amount { get; set; }
    }
}

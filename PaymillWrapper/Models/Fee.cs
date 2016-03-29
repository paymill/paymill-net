using Newtonsoft.Json;
using PaymillWrapper.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymillWrapper.Models
{
    public class Fee
    {
        [Newtonsoft.Json.JsonConverter(typeof(StringToBaseEnumTypeConverter<FeeType>))]
        public sealed class FeeType : EnumBaseType
        {
            public static readonly Fee.FeeType APPLICATION = new FeeType("application");
            public static readonly Fee.FeeType UNKNOWN = new FeeType("", true);

            private FeeType(String value, Boolean unknowValue = false)
                : base(value, unknowValue)
            {

            }
            public FeeType()
                : base("", false)
            {
            }
        }

        [JsonProperty("type")]
        public FeeType Type { get; set; }

        [JsonProperty("application")]
        public String Application { get; set; }

        [JsonProperty("payment")]
        public String Payment { get; set; }

        [JsonProperty("amount")]
        public int? Amount { get; set; }

        [JsonProperty("billed_at")]
        public DateTime? BilledAt { get; set; }

        [JsonProperty("currency")]
        public String Currency { get; set; }
    }
}

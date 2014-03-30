using Newtonsoft.Json;
using PaymillWrapper.Utils;
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

        [DataMember(Name = "application")]
        public String application{ get; set; }
    }
}

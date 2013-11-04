using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PaymillWrapper.Models
{
    public class Webhook : BaseModel
    {
        public enum EventType
        {
            CHARGEBACK_EXECUTED,
            REFUND_SUCCEEDED,
            REFUND_FAILED,
            SUBSCRIPTION_SUCCEEDED,
            SUBSCRIPTION_FAILED,
            TRANSACTION_SUCCEEDED,
            TRANSACTION_FAILED
        }
        [DataMember(Name = "Url")]
        public Uri Url { get; set; }

        [DataMember(Name = "Email")]
        public String Email { get; set; }

        [DataMember(Name = "EventTypes")]
        public EventType[] EventTypes { get; set; }
    }
}

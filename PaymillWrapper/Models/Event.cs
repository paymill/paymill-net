using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PaymillWrapper.Models
{
    public class Event
    {
        [DataMember(Name = "Subscription")]
        public Subscription Subscription;

        [DataMember(Name = "Transaction")]
        public Transaction Transaction;

        [DataMember(Name = "Event_Type")]
        public PaymillWrapper.Models.Webhook.EventType Type { get; set; }
    }
}

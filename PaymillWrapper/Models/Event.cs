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
    public class Event
    {
        [DataMember(Name = "subscription")]
        public Subscription Subscription;

        [DataMember(Name = "transaction")]
        public Transaction Transaction;

        [DataMember(Name = "event_type")]
        public PaymillWrapper.Models.EventType Type { get; set; }
    }
}

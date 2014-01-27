using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;


namespace PaymillWrapper.Models
{
    public class Event
    {
        [JsonPropertyAttribute("Subscription")]
        public Subscription Subscription;

        [JsonPropertyAttribute("Transaction")]
        public Transaction Transaction;

        [JsonPropertyAttribute("Event_Type")]
        public PaymillWrapper.Models.EventType Type { get; set; }
    }
}

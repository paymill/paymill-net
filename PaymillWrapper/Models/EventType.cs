using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PaymillWrapper.Models
{
    [DataContract]
    public sealed class EventType
    {
        private readonly String name;
        private static List<EventType> createdEvents = new List<EventType>();
        public static readonly EventType CHARGEBACK_EXECUTED = new EventType("chargeback.executed");
        public static readonly EventType REFUND_SUCCEEDED = new EventType("refund.succeeded");
        public static readonly EventType REFUND_FAILED = new EventType("refund.failed");
        public static readonly EventType SUBSCRIPTION_SUCCEEDED = new EventType("subscription.succeeded");
        public static readonly EventType SUBSCRIPTION_FAILED = new EventType("subscription.failed");
        public static readonly EventType TRANSACTION_SUCCEEDED = new EventType("transaction.succeeded");
        public static readonly EventType TRANSACTION_FAILED = new EventType("transaction.failed");
        private EventType(String name)
        {
            this.name = name;
            createdEvents.Add(this);
        }
        public override String ToString()
        {
            return name;
        }
        public static EventType GetEventByName(String name)
        {
            return EventType.createdEvents.Find(x => String.Compare(x.name, name) == 0);
        }

    }
}

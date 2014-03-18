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
        [DataMember(Name = "url"),
        Updateable(Name = "url")]
        public Uri Url { get; set; }

        [DataMember(Name = "livemode")]
        public Boolean livemode { get; set; }

        [DataMember(Name = "email"),
        Updateable(Name = "email")]
        public String Email { get; set; }

        [DataMember(Name = "event_types")]
         public EventType[] EventTypes { get; set; }

        [DataContract]
        public sealed class EventType : EventBaseType
        {
            public static readonly Webhook.EventType CHARGEBACK_EXECUTED = new EventType("chargeback.executed");
            public static readonly Webhook.EventType REFUND_SUCCEEDED = new EventType("refund.succeeded");
            public static readonly Webhook.EventType REFUND_CREATED = new EventType("refund.created");
            public static readonly Webhook.EventType REFUND_FAILED = new EventType("refund.failed");
            public static readonly Webhook.EventType SUBSCRIPTION_SUCCEEDED = new EventType("subscription.succeeded");
            public static readonly Webhook.EventType SUBSCRIPTION_CREATED = new EventType("subscription.created");
            public static readonly Webhook.EventType SUBSCRIPTION_FAILED = new EventType("subscription.failed");
            public static readonly Webhook.EventType SUBSCRIPTION_UPDATED = new EventType("subscription.updated");
            public static readonly Webhook.EventType SUBSCRIPTION_DELETED = new EventType("subscription.deleted");
            public static readonly Webhook.EventType TRANSACTION_SUCCEEDED = new EventType("transaction.succeeded");
            public static readonly Webhook.EventType TRANSACTION_FAILED = new EventType("");
            public static readonly Webhook.EventType TRANSACTION_CREATED = new EventType("transaction.created");
            public static readonly Webhook.EventType PAYOUT_TRANSFERRED = new EventType("payout.transferred");
            public static readonly Webhook.EventType INVOICE_AVAILABLE = new EventType("invoice.available");
            public static readonly Webhook.EventType APP_MERCHANT_ACTIVATED = new EventType("app.merchant.activated");
            public static readonly Webhook.EventType APP_MERCHANT_DEACTIVATED = new EventType("app.merchant.deactivated");
            public static readonly Webhook.EventType APP_MERCHANT_REJECTED = new EventType("app.merchant.rejected");
            public static readonly Webhook.EventType CLIENT_UPDATED = new EventType("client.updated");
            private EventType (String name):base(name)
	        {

	        }
        }

    }
}

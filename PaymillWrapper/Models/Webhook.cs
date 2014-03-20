using Newtonsoft.Json;
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

        [DataMember(Name = "app_id" )]
        public String  AppId{get; private set;}

        [DataMember(Name = "event_types")]
        public WebhookEventType[] EventTypes { get; set; }

        [DataContract]
        public sealed class WebhookEventType : EnumBaseType
        {
            public static readonly Webhook.WebhookEventType CHARGEBACK_EXECUTED = new WebhookEventType("chargeback.executed");
            public static readonly Webhook.WebhookEventType REFUND_SUCCEEDED = new WebhookEventType("refund.succeeded");
            public static readonly Webhook.WebhookEventType REFUND_CREATED = new WebhookEventType("refund.created");
            public static readonly Webhook.WebhookEventType REFUND_FAILED = new WebhookEventType("refund.failed");
            public static readonly Webhook.WebhookEventType SUBSCRIPTION_SUCCEEDED = new WebhookEventType("subscription.succeeded");
            public static readonly Webhook.WebhookEventType SUBSCRIPTION_CREATED = new WebhookEventType("subscription.created");
            public static readonly Webhook.WebhookEventType SUBSCRIPTION_FAILED = new WebhookEventType("subscription.failed");
            public static readonly Webhook.WebhookEventType SUBSCRIPTION_UPDATED = new WebhookEventType("subscription.updated");
            public static readonly Webhook.WebhookEventType SUBSCRIPTION_DELETED = new WebhookEventType("subscription.deleted");
            public static readonly Webhook.WebhookEventType TRANSACTION_SUCCEEDED = new WebhookEventType("transaction.succeeded");
            public static readonly Webhook.WebhookEventType TRANSACTION_FAILED = new WebhookEventType("transaction.failed");
            public static readonly Webhook.WebhookEventType TRANSACTION_CREATED = new WebhookEventType("transaction.created");
            public static readonly Webhook.WebhookEventType PAYOUT_TRANSFERRED = new WebhookEventType("payout.transferred");
            public static readonly Webhook.WebhookEventType INVOICE_AVAILABLE = new WebhookEventType("invoice.available");
            public static readonly Webhook.WebhookEventType APP_MERCHANT_ACTIVATED = new WebhookEventType("app.merchant.activated");
            public static readonly Webhook.WebhookEventType APP_MERCHANT_DEACTIVATED = new WebhookEventType("app.merchant.deactivated");
            public static readonly Webhook.WebhookEventType APP_MERCHANT_REJECTED = new WebhookEventType("app.merchant.rejected");
            public static readonly Webhook.WebhookEventType CLIENT_UPDATED = new WebhookEventType("client.updated");
            private WebhookEventType(String name)
                : base(name)
            {

            }
        }

    }
}

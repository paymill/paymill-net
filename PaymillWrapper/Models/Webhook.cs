using Newtonsoft.Json;
using PaymillWrapper.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymillWrapper.Models
{
    public class Webhook : BaseModel
    {
        [JsonProperty("url"),
        Updateable(Name = "url")]
        public Uri Url { get; set; }

        [JsonProperty("livemode")]
        public Boolean livemode { get; set; }

        [JsonProperty("email"),
        Updateable(Name = "email")]
        public String Email { get; set; }

        [JsonProperty("app_id")]
        public String AppId { get; private set; }

        [JsonProperty("event_types")]
        public WebhookEventType[] EventTypes { get; set; }

        [JsonConverter(typeof(StringToBaseEnumTypeConverter<WebhookEventType>))]
        public sealed class WebhookEventType : EnumBaseType
        {
            public static readonly Webhook.WebhookEventType CHARGEBACK_EXECUTED;
            public static readonly Webhook.WebhookEventType REFUND_SUCCEEDED;
            public static readonly Webhook.WebhookEventType REFUND_CREATED;
            public static readonly Webhook.WebhookEventType REFUND_FAILED;
            public static readonly Webhook.WebhookEventType SUBSCRIPTION_SUCCEEDED;
            public static readonly Webhook.WebhookEventType SUBSCRIPTION_CREATED;
            public static readonly Webhook.WebhookEventType SUBSCRIPTION_FAILED;
            public static readonly Webhook.WebhookEventType SUBSCRIPTION_UPDATED;
            public static readonly Webhook.WebhookEventType SUBSCRIPTION_DELETED;
            public static readonly Webhook.WebhookEventType TRANSACTION_SUCCEEDED;
            public static readonly Webhook.WebhookEventType TRANSACTION_FAILED;
            public static readonly Webhook.WebhookEventType TRANSACTION_CREATED;
            public static readonly Webhook.WebhookEventType PAYOUT_TRANSFERRED;
            public static readonly Webhook.WebhookEventType INVOICE_AVAILABLE;
            public static readonly Webhook.WebhookEventType APP_MERCHANT_ACTIVATED;
            public static readonly Webhook.WebhookEventType APP_MERCHANT_DEACTIVATED;
            public static readonly Webhook.WebhookEventType APP_MERCHANT_REJECTED;
            public static readonly Webhook.WebhookEventType CLIENT_UPDATED;
            public static readonly Webhook.WebhookEventType UNKNOWN;
            public static readonly Webhook.WebhookEventType SUBSCRIPTION_EXPIRING;
            public static readonly Webhook.WebhookEventType SUBSCRIPTION_DEACTIVATED;
            public static readonly Webhook.WebhookEventType SUBSCRIPTION_ACTIVATED;
            public static readonly Webhook.WebhookEventType SUBSCRIPTION_CANCELED;
            public static readonly Webhook.WebhookEventType PAYMENT_EXPIRED;
            public static readonly Webhook.WebhookEventType APP_MERCHANT_LIVE_REQUESTS_ALLOWED;
            public static readonly Webhook.WebhookEventType APP_MERCHANT_LIVE_REQUESTS_NOT_ALLOWED;
            public static readonly Webhook.WebhookEventType APP_MERCHANT_APP_DISABLED;

            private WebhookEventType(String value, Boolean unknowValue = false)
                : base(value, unknowValue)
            {

            }
            public WebhookEventType()
                : base("", false)
            {
            }
            static WebhookEventType()
            {
                CHARGEBACK_EXECUTED = new WebhookEventType("chargeback.executed");
                REFUND_SUCCEEDED = new WebhookEventType("refund.succeeded");
                REFUND_CREATED = new WebhookEventType("refund.created");
                REFUND_FAILED = new WebhookEventType("refund.failed");
                SUBSCRIPTION_SUCCEEDED = new WebhookEventType("subscription.succeeded");
                SUBSCRIPTION_CREATED = new WebhookEventType("subscription.created");
                SUBSCRIPTION_FAILED = new WebhookEventType("subscription.failed");
                SUBSCRIPTION_UPDATED = new WebhookEventType("subscription.updated");
                SUBSCRIPTION_DELETED = new WebhookEventType("subscription.deleted");
                TRANSACTION_SUCCEEDED = new WebhookEventType("transaction.succeeded");
                TRANSACTION_FAILED = new WebhookEventType("transaction.failed");
                TRANSACTION_CREATED = new WebhookEventType("transaction.created");
                PAYOUT_TRANSFERRED = new WebhookEventType("payout.transferred");
                INVOICE_AVAILABLE = new WebhookEventType("invoice.available");
                APP_MERCHANT_ACTIVATED = new WebhookEventType("app.merchant.activated");
                APP_MERCHANT_DEACTIVATED = new WebhookEventType("app.merchant.deactivated");
                APP_MERCHANT_REJECTED = new WebhookEventType("app.merchant.rejected");
                CLIENT_UPDATED = new WebhookEventType("client.updated");
                UNKNOWN = new WebhookEventType("", true);
                SUBSCRIPTION_EXPIRING = new WebhookEventType("subscription.expiring");
                SUBSCRIPTION_DEACTIVATED = new WebhookEventType("subscription.deactivated");
                SUBSCRIPTION_ACTIVATED = new WebhookEventType("subscription.activated");
                SUBSCRIPTION_CANCELED = new WebhookEventType("subscription.activated");
                PAYMENT_EXPIRED = new WebhookEventType("payment.expired");
                APP_MERCHANT_LIVE_REQUESTS_ALLOWED = new WebhookEventType("app.merchant.live_requests_allowed");
                APP_MERCHANT_LIVE_REQUESTS_NOT_ALLOWED = new WebhookEventType("app.merchant.live_requests_not_allowed");
                APP_MERCHANT_APP_DISABLED = new WebhookEventType("app.merchant.app.disabled");

            }
        }

        public static Webhook.Filter CreateFilter()
        {
            return new Webhook.Filter();
        }

        public static Webhook.Order CreateOrder()
        {
            return new Webhook.Order();
        }

        public sealed class Filter : BaseFilter
        {

            [SnakeCase(Value = "url")]
            private String url;

            [SnakeCase(Value = "email")]
            private String email;


            internal Filter()
            {
            }

            public Webhook.Filter ByUrl(String url)
            {
                this.url = url;
                return this;
            }

            public Webhook.Filter ByEmail(String email)
            {
                this.email = email;
                return this;
            }
        }

        public sealed class Order : BaseOrder
        {

            [SnakeCase(Value = "url")]
            private Boolean url;

            [SnakeCase(Value = "email")]
            private Boolean email;

            [SnakeCase(Value = "created_at")]
            private Boolean createdAt;


            internal Order()
            {

            }

            public Webhook.Order Asc()
            {
                base.setAsc();
                return this;
            }

            public Webhook.Order Desc()
            {
                base.setDesc();
                return this;
            }

            public Webhook.Order ByCreatedAt()
            {
                this.email = false;
                this.createdAt = true;
                this.url = false;
                return this;
            }

            public Webhook.Order ByUrl()
            {
                this.email = false;
                this.createdAt = false;
                this.url = true;
                return this;
            }

            public Webhook.Order ByEmail()
            {
                this.email = true;
                this.createdAt = false;
                this.url = false;
                return this;
            }

        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaymillWrapper;
using PaymillWrapper.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using PaymillWrapper.Utils;
namespace ResolvingWebhooks
{
    public class WebhookResolver
    {

        public Webhook.WebhookEventType EventType { get; private set; }
        public Client Client { get; private set; }
        public Transaction Transaction { get; private set; }
        public Subscription Subscription { get; private set; }
        public Refund Refund { get; private set; }
        public Payment Payment { get; private set; }
        public Invoice Invoice { get; private set; }
        public Merchant Merchant { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public String AppId { get; private set; }

        internal WebhookResolver()
        {
            EventType = new Webhook.WebhookEventType();
        }
        public static WebhookResolver FromString(String requestBody)
        {
            try
            {
                WebhookResolver resolver = new WebhookResolver();
                var jsonArray = JObject.Parse(requestBody);
                var eventNode = jsonArray["event"];
                if (eventNode != null && eventNode["event_type"] != null)
                {
                    String data = eventNode["event_type"].ToString();
                    resolver.EventType = (Webhook.WebhookEventType)Webhook.WebhookEventType.GetItemByValue(data, resolver.EventType.GetType());
                }
                else
                {
                    throw new SystemException("Invalid webhook:" + requestBody);
                }


                if (resolver.EventType == Webhook.WebhookEventType.CHARGEBACK_EXECUTED
                    || resolver.EventType == Webhook.WebhookEventType.TRANSACTION_CREATED
                    || resolver.EventType == Webhook.WebhookEventType.TRANSACTION_SUCCEEDED
                    || resolver.EventType == Webhook.WebhookEventType.TRANSACTION_FAILED)
                {
                    var eventResource = eventNode["event_resource"].ToString();
                    resolver.Transaction = PaymillWrapper.Utils.Parser.ReadValue<Transaction>(eventResource);
                }

                if (resolver.EventType == Webhook.WebhookEventType.SUBSCRIPTION_CREATED
                      || resolver.EventType == Webhook.WebhookEventType.SUBSCRIPTION_UPDATED
                      || resolver.EventType == Webhook.WebhookEventType.SUBSCRIPTION_DELETED
                      || resolver.EventType == Webhook.WebhookEventType.SUBSCRIPTION_EXPIRING
                      || resolver.EventType == Webhook.WebhookEventType.SUBSCRIPTION_DEACTIVATED
                      || resolver.EventType == Webhook.WebhookEventType.SUBSCRIPTION_ACTIVATED
                      || resolver.EventType == Webhook.WebhookEventType.SUBSCRIPTION_CANCELED)
                {
                    var eventResource = eventNode["event_resource"].ToString();
                    resolver.Subscription = PaymillWrapper.Utils.Parser.ReadValue<Subscription>(eventResource);
                }

                if (resolver.EventType == Webhook.WebhookEventType.REFUND_CREATED
                    || resolver.EventType == Webhook.WebhookEventType.REFUND_SUCCEEDED
                    || resolver.EventType == Webhook.WebhookEventType.REFUND_FAILED)
                {
                    var eventResource = eventNode["event_resource"].ToString();
                    resolver.Refund = PaymillWrapper.Utils.Parser.ReadValue<Refund>(eventResource);
                }

                if (resolver.EventType == Webhook.WebhookEventType.CLIENT_UPDATED)
                {
                    var eventResource = eventNode["event_resource"].ToString();
                    resolver.Client = PaymillWrapper.Utils.Parser.ReadValue<Client>(eventResource);
                }
                if (resolver.EventType == Webhook.WebhookEventType.SUBSCRIPTION_SUCCEEDED
                    || resolver.EventType == Webhook.WebhookEventType.SUBSCRIPTION_FAILED)
                {
                    var eventResource = eventNode["event_resource"];
                    var subscription = eventResource["subscription"].ToString();
                    var transaction = eventResource["transaction"].ToString();
                    resolver.Subscription = PaymillWrapper.Utils.Parser.ReadValue<Subscription>(subscription);
                    resolver.Transaction = PaymillWrapper.Utils.Parser.ReadValue<Transaction>(transaction);
                }

                if (resolver.EventType == Webhook.WebhookEventType.PAYMENT_EXPIRED)
                {
                    var eventResource = eventNode["event_resource"];
                    resolver.Payment = PaymillWrapper.Utils.Parser.ReadValue<Payment>(eventResource.ToString());
                }
                if (resolver.EventType == Webhook.WebhookEventType.PAYOUT_TRANSFERRED
                      || resolver.EventType == Webhook.WebhookEventType.INVOICE_AVAILABLE)
                {
                    var eventResource = eventNode["event_resource"];
                    resolver.Invoice = PaymillWrapper.Utils.Parser.ReadValue<Invoice>(eventResource.ToString());
                }
                if (resolver.EventType == Webhook.WebhookEventType.APP_MERCHANT_ACTIVATED
                      || resolver.EventType == Webhook.WebhookEventType.APP_MERCHANT_DEACTIVATED
                      || resolver.EventType == Webhook.WebhookEventType.APP_MERCHANT_REJECTED
                      || resolver.EventType == Webhook.WebhookEventType.APP_MERCHANT_LIVE_REQUESTS_ALLOWED
                      || resolver.EventType == Webhook.WebhookEventType.APP_MERCHANT_LIVE_REQUESTS_NOT_ALLOWED
                      || resolver.EventType == Webhook.WebhookEventType.APP_MERCHANT_APP_DISABLED)
                {
                    var eventResource = eventNode["event_resource"];
                    resolver.Merchant = PaymillWrapper.Utils.Parser.ReadValue<Merchant>(eventResource.ToString());
                }

                if (eventNode["created_at"] != null)
                {
                    resolver.CreatedAt = new DateTime(long.Parse(eventNode["created_at"].ToString()) * 1000);
                }
                if (eventNode["app_id"] != null)
                {
                    resolver.AppId = eventNode["app_id"].ToString();
                }

                return resolver;

            }
            catch (Exception e)
            {
                throw new SystemException(e.Message);
            }

        }

    }
}

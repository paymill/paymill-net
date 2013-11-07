using PaymillWrapper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace PaymillWrapper.Net
{
    public class URLEncoder
    {
        private Encoding charset;

        public URLEncoder()
        {
            charset = Encoding.UTF8;
        }

        public string Encode<T>(Object data)
        {
            var props = typeof(T).GetProperties();

            if (!data.GetType().ToString().StartsWith("PaymillWrapper.Models"))
                throw new PaymillException(
                    String.Format("Unknown object type '{0}'. Only objects in package " +
                    "'PaymillWrapper.Paymill.Model' are supported.", data.GetType().ToString())
                    );

            StringBuilder sb = new StringBuilder();
            foreach (var prop in props)
            {
                object value = prop.GetValue(data, null);
                if (value != null)
                    this.addKeyValuePair(sb, prop.Name.ToLower(), value);
            }

            return sb.ToString();
        }
        public string EncodeObject(Object data)
        {
            var props = data.GetType().GetProperties();

            
            StringBuilder sb = new StringBuilder();
            foreach (var prop in props)
            {
                object value = prop.GetValue(data, null);
                if (value != null)
                    this.addKeyValuePair(sb, prop.Name.ToLower(), value);
            }

            return sb.ToString();
        }
        public string EncodeTransaction(Transaction data)
        {
            StringBuilder sb = new StringBuilder();

            this.addKeyValuePair(sb, "amount", data.Amount);
            this.addKeyValuePair(sb, "currency", data.Currency);

            if (!string.IsNullOrEmpty(data.Token))
                this.addKeyValuePair(sb, "token", data.Token);

            if (data.Client != null && !string.IsNullOrEmpty(data.Client.Id))
                this.addKeyValuePair(sb, "client", data.Client.Id);

            if (data.Payment != null && !string.IsNullOrEmpty(data.Payment.Id))
                this.addKeyValuePair(sb, "payment", data.Payment.Id);

            this.addKeyValuePair(sb, "description", data.Description);
            
            return sb.ToString();
        }
        public string EncodePreauthorization(Preauthorization data)
        {
            StringBuilder sb = new StringBuilder();

            this.addKeyValuePair(sb, "amount", data.Amount);
            this.addKeyValuePair(sb, "currency", data.Currency);

            if (!string.IsNullOrEmpty(data.Token))
                this.addKeyValuePair(sb, "token", data.Token);

            if (data.Payment != null && !string.IsNullOrEmpty(data.Payment.Id))
                this.addKeyValuePair(sb, "payment", data.Payment.Id);

            return sb.ToString();
        }
        public string EncodeRefund(Refund data)
        {
            StringBuilder sb = new StringBuilder();

            this.addKeyValuePair(sb, "amount", data.Amount);
            this.addKeyValuePair(sb, "description", data.Description);

            return sb.ToString();
        }
        public string EncodeOfferAdd(Offer data)
        {
            StringBuilder sb = new StringBuilder();

            this.addKeyValuePair(sb, "amount", data.Amount);
            this.addKeyValuePair(sb, "currency", data.Currency);
            this.addKeyValuePair(sb, "interval", data.Interval);
            this.addKeyValuePair(sb, "name", data.Name);

            return sb.ToString();
        }
        public string EncodeOfferUpdate(Offer data)
        {
            StringBuilder sb = new StringBuilder();

            this.addKeyValuePair(sb, "name", data.Name);

            return sb.ToString();
        }
        public string EncodeSubscriptionAdd(Subscription data)
        {
            StringBuilder sb = new StringBuilder();

            if (data.Client != null)
                this.addKeyValuePair(sb, "client", data.Client.Id);
            if (data.Offer != null)
                this.addKeyValuePair(sb, "offer", data.Offer.Id);
            if (data.Payment != null)
                this.addKeyValuePair(sb, "payment", data.Payment.Id);

            return sb.ToString();
        }
        public string EncodeSubscriptionUpdate(Subscription data)
        {
            StringBuilder sb = new StringBuilder();

            this.addKeyValuePair(sb, "cancel_at_period_end", data.Cancel_At_Period_End);

            return sb.ToString();
        }
        public static String ConvertEventsArr(params PaymillWrapper.Models.EventType[] eventTypes)
        {
            List<String> typesList = new List<String>();
            foreach (PaymillWrapper.Models.EventType evt in eventTypes)
            {
                typesList.Add(evt.ToString());
            }

            return String.Join(",", typesList.ToArray()); ;
        }
        public string EncodeWebhookUpdate(Webhook data)
        {
            StringBuilder sb = new StringBuilder();
            if (data.Url != null) { 
                this.addKeyValuePair(sb, "url", data.Url.AbsolutePath);
            }
            if (data.Email != null)
            {
                this.addKeyValuePair(sb, "email", data.Email);
            }
            return sb.ToString();
        }
        public string EncodeClientUpdate(Client data)
        {
            StringBuilder sb = new StringBuilder();
            this.addKeyValuePair(sb, "email", data.Email);
            this.addKeyValuePair(sb, "description", data.Description);
            return sb.ToString();
        }

        private void addKeyValuePair(StringBuilder sb, string key, object value)
        {
            string reply = "";

            if (value == null) return;

            try
            {

                key = HttpUtility.UrlEncode(key.ToLower(), this.charset);

                if (value.GetType().IsEnum)
                {
                    reply = value.ToString().ToLower();
                }
                else if (value.GetType().Equals(typeof(DateTime)))
                {
                    if (value.Equals(DateTime.MinValue)) reply="";
                }
                else
                {
                    reply = HttpUtility.UrlEncode(value.ToString(), this.charset);
                }

                if (!string.IsNullOrEmpty(reply))
                {
                    if (sb.Length > 0)
                        sb.Append("&");

                    sb.Append(String.Format("{0}={1}", key, reply));
                }

            }
            catch
            {
                throw new PaymillException(
                    String.Format("Unsupported or invalid character set encoding '{0}'.", charset));
            }

        }
    }
}
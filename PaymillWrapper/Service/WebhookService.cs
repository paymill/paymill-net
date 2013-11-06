using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PaymillWrapper.Models;
using PaymillWrapper.Net;

namespace PaymillWrapper.Service
{
    public class WebhookService : AbstractService<Webhook>
    {
        public WebhookService(HttpClientRest webhook)
            : base(webhook)
        {
        }

        public Webhook Create(Webhook obj)
        {
            if (obj.Url == null)
            {
                return CreateEmail(obj.Email, obj.EventTypes);
            }

            return CreateUrl(obj.Url, obj.EventTypes);
        }
        public Webhook Get(String webhookID)
        {
            return get<Webhook>(Resource.Webhooks, webhookID);
        }
      
        public Webhook CreateUrl(Uri url, params PaymillWrapper.Models.EventType[] eventTypes)
        {
           
            if (url == null)
            {
                throw new NullReferenceException("url");
            }
            if (eventTypes == null)
            {
                throw new NullReferenceException("eventTypes");
            }
            return create<Webhook>(
                   Resource.Webhooks,
                   null,
                   new URLEncoder().EncodeObject(new { url = url.AbsoluteUri, event_types = convertEventsArr(eventTypes) }));
        }

        /// <summary>
        /// This funcion create webhook by email
        /// </summary>
        /// <param name="email"></param>
        /// <param name="eventTypes"></param>
        /// <returns></returns>
        public Webhook CreateEmail(String email, params  PaymillWrapper.Models.EventType[] eventTypes)
        {
            if (email == null)
            {
                throw new NullReferenceException("email");
            }
            if (eventTypes == null)
            {
                throw new NullReferenceException("eventTypes");
            }
            return create<Webhook>(
                   Resource.Webhooks,
                   null,
                   new URLEncoder().EncodeObject(new { email = email, event_types = convertEventsArr(eventTypes) }));
        }
        /// <summary>
        /// This function update the webhook
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public Webhook Update(Webhook webhook)
        {
            return update<Webhook>(
                Resource.Webhooks,
                webhook,
                webhook.Id,
                new URLEncoder().EncodeWebhookUpdate(webhook));
        }
       /// <summary>
       /// This function remove webhook
       /// </summary>
       /// <param name="webhookID"></param>
       /// <returns></returns>
        public Boolean Remove(string webhookID)
        {
            return remove<Webhook>(Resource.Webhooks, webhookID);
        }
        /// <summary>
        /// this function return all webhooks
        /// </summary>
        /// <returns></returns>
        public List<Webhook> GetWebhooks()
        {
            return getList<Webhook>(Resource.Webhooks, null);
        }
        private String convertEventsArr(params PaymillWrapper.Models.EventType[] eventTypes)
        {
            List<String> typesList = new List<String>();
            foreach (PaymillWrapper.Models.EventType evt in eventTypes)
            {
                typesList.Add(evt.ToString());
            }

            return String.Join(",", typesList.ToArray()); ;
        }
    }
}
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


        public Webhook CreateUrl(Uri url, params PaymillWrapper.Models.Webhook.EventType[] eventTypes)
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
                   new URLEncoder().EncodeObject(new { url = url.AbsoluteUri, event_types = eventTypes }));
        }


        public Webhook CreateEmail(String email, params  PaymillWrapper.Models.Webhook.EventType[] eventTypes)
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
                   new URLEncoder().EncodeObject(new { email = email, event_types = eventTypes }));
        }

        public Webhook Update(Webhook obj)
        {
            //Map<String, Object> params = new HashMap<String, Object>();
            //String url = obj.getUrl() == null ? null : obj.getUrl().toString();
            //String email = obj.getEmail() == null ? null : obj.getEmail();
            //if (url != null) {
            //    params.put("url", url);
            //}
            //if (email != null) {
            //    params.put("email", email);
            //}
            //params.put("event_types", obj.getEventTypes());
            //return client.put(resource, obj.getId(), params, modelClass);
            return null;
        }
    }
}
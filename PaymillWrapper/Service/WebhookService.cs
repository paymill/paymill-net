using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PaymillWrapper.Models;
using PaymillWrapper.Net;
using System.Threading.Tasks;

namespace PaymillWrapper.Service
{
    class WebhookService : AbstractService<Webhook>
    {
        public WebhookService(HttpClientRest webhook)
            : base(Resource.Webhooks, webhook, Paymill.ApiUrl)
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
      
      
        public Webhook CreateUrl(Uri url, params PaymillWrapper.Models.EventType[] eventTypes)
        {
           /*
            if (url == null)
            {
                throw new NullReferenceException("url");
            }
            if (eventTypes == null)
            {
                throw new NullReferenceException("eventTypes");
            }
            return Create(
                   null,
                   new UrlEncoder().EncodeObject(new {
                            url = url.AbsoluteUri,
                            event_types = UrlEncoder.ConvertEventsArr(eventTypes) 
                   }));
            * */
            return null;
        }

        /// <summary>
        /// This funcion create webhook by email
        /// </summary>
        /// <param name="email"></param>
        /// <param name="eventTypes"></param>
        /// <returns></returns>
        public Webhook CreateEmail(String email, params PaymillWrapper.Models.EventType[] eventTypes)
        {
            /*
            if (email == null)
            {
                throw new NullReferenceException("email");
            }
            if (eventTypes == null)
            {
                throw new NullReferenceException("eventTypes");
            }
            return Create(
                   null,
                   new UrlEncoder().EncodeObject(new { 
                       email = email,
                       event_types = UrlEncoder.ConvertEventsArr(eventTypes) 
                   }));*/
            return null;
        }
        
       
        /// <summary>
        /// this function return all webhooks
        /// </summary>
        /// <returns></returns>
        public async Task<List<Webhook>> GetWebhooks()
        {
            return await ListAsync(null);
        }
        protected override string GetResourceId(Webhook obj)
        {
            return obj.Id;
        }

    }
}
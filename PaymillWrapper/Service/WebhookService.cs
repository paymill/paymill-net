using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PaymillWrapper.Models;
using PaymillWrapper.Utils;
using System.Threading.Tasks;

namespace PaymillWrapper.Service
{
    public class WebhookService : AbstractService<Webhook>
    {
        public WebhookService(System.Net.Http.HttpClient webhook, string apiUrl)
            : base(Resource.Webhooks, webhook, apiUrl)
        {
        }

        public async Task<Webhook> CreateEmailWebhookAsync(String email, Webhook.WebhookEventType[] eventTypes)
        {
            String encodeParams = new UrlEncoder().EncodeObject(new
            {
                email = email,
                event_types = UrlEncoder.ConvertEventsArr(eventTypes)
            });

            return await createAsync(null, encodeParams);
        }


        public async Task<Webhook> CreateUrlWebhookAsync(Uri url, params Webhook.WebhookEventType[] eventTypes)
        {
            String encodeParams = new UrlEncoder().EncodeObject(new
            {
                url = url.AbsoluteUri,
                event_types = UrlEncoder.ConvertEventsArr(eventTypes)
            });

            return await createAsync(null, encodeParams);
        }

        
        protected override string GetResourceId(Webhook obj)
        {
            return obj.Id;
        }

    }
}
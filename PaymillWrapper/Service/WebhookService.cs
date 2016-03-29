using PaymillWrapper.Models;
using PaymillWrapper.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
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

        /// This function returns a <see cref="PaymillList"/>of PAYMILL Client objects. In which order this list is returned depends on the
        /// </summary>
        /// <param name="filter">Filter or null</param>
        /// <param name="order">Order or null.</param>
        /// <returns>PaymillList which contains a List of PAYMILL Client object and their total count.</returns>
        public async Task<PaymillWrapper.Models.PaymillList<Webhook>> ListAsync(Webhook.Filter filter, Webhook.Order order)
        {
            return await base.listAsync(filter, order, null, null);
        }

        /// <summary>
        /// This function returns a <see cref="PaymillList"/> of PAYMILL objects. In which order this list is returned depends on the
        /// optional parameters. If null is given, no filter or order will be applied, overriding the default count and
        /// offset.
        /// </summary>
        /// <param name="filter">Filter or null</param>
        /// <param name="order">Order or null.</param>
        /// <param name="count">Max count of returned objects in the PaymillList</param>
        /// <param name="offset">The offset to start from.</param>
        /// <returns>PaymillList which contains a List of PAYMILL objects and their total count.</returns>
        public async Task<PaymillWrapper.Models.PaymillList<Webhook>> ListAsync(Webhook.Filter filter, Webhook.Order order, int? count, int? offset)
        {
            return await base.listAsync(filter, order, count, offset);
        }
        protected override string GetResourceId(Webhook obj)
        {
            return obj.Id;
        }

    }
}

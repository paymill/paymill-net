using System.Net.Http;
using PaymillWrapper.Models;
using PaymillWrapper.Net;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PaymillWrapper.Service
{
    public class SubscriptionService : AbstractService<Subscription>
    {
        public SubscriptionService(HttpClient client, string apiUrl)
            : base(Resource.Subscriptions, client, apiUrl)
        {
        }
       
        /// <summary>
        /// This function allows request a subscription list
        /// </summary>
        /// <param name="filter">Result filtered in the required way</param>
        /// <returns>Returns a list subscription-object</returns>
        public async Task< List<Subscription>> GetSubscriptionsByFilter(Filter filter)
        {
            return await ListAsync(filter);
        }

        protected override string GetResourceId(Subscription obj)
        {
            return obj.Id;
        }

        /// <summary>
        /// This function creates a subscription object
        /// </summary>
        /// <param name="offer"></param>
        /// <param name="client"></param>
        /// <param name="pay"></param>
        /// <returns></returns>
        public Subscription Subscribe(Offer offer, Client client, Payment pay)
        {
            /*
            Subscription subscription = new Subscription();
            subscription.Client = client;
            subscription.Offer = offer;
            subscription.Payment = pay;

            return Create(
                null,
                new UrlEncoder().EncodeSubscriptionAdd(subscription));
             * */
            return null;
        }
    }
}
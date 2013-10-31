using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PaymillWrapper.Models;
using PaymillWrapper.Net;

namespace PaymillWrapper.Service
{
    public class SubscriptionService : AbstractService<Subscription>
    {
        public SubscriptionService(HttpClientRest client):base(client)
        {
        }

        /// <summary>
        /// This function allows request a subscription list
        /// </summary>
        /// <returns>Returns a list subscriptions-object</returns>
        public List<Subscription> GetSubscriptions()
        {
            return getList<Subscription>(Resource.Subscriptions);
        }

        /// <summary>
        /// This function allows request a subscription list
        /// </summary>
        /// <param name="filter">Result filtered in the required way</param>
        /// <returns>Returns a list subscription-object</returns>
        public List<Subscription> GetSubscriptions(Filter filter)
        {
            return getList<Subscription>(Resource.Subscriptions, filter);
        }

        /// <summary>
        /// This function creates a subscription object
        /// </summary>
        /// <param name="client">Object-subscription</param>
        /// <returns>New object-subscription just add</returns>
        public Subscription CreateSubscription(Subscription subscription)
        {
            return create<Subscription>(
                Resource.Subscriptions,
                null,
                new URLEncoder().EncodeSubscriptionAdd(subscription));
        }

        /// <summary>
        /// To get the details of an existing subscription you’ll need to supply the subscription ID
        /// </summary>
        /// <param name="clientID">Subscription identifier</param>
        /// <returns>Subscription-object</returns>
        public Subscription GetSubscription(string subscriptionID)
        {
            return get<Subscription>(Resource.Subscriptions, subscriptionID);
        }

        /// <summary>
        /// This function deletes a subscription
        /// </summary>
        /// <param name="clientID">Subscription identifier</param>
        /// <returns>Return true if remove was ok, false if not possible</returns>
        public bool RemoveSubscription(string subscriptionID)
        {
            return remove<Subscription>(Resource.Subscriptions, subscriptionID);
        }

        /// <summary>
        /// This function updates the data of a subscription
        /// </summary>
        /// <param name="client">Object-subscription</param>
        /// <returns>Object-subscription just updated</returns>
        public Subscription UpdateSubscription(Subscription subscription)
        {
            return update<Subscription>(
                Resource.Subscriptions,
                subscription,
                subscription.Id,
                new URLEncoder().EncodeSubscriptionUpdate(subscription));
        }
    }
}
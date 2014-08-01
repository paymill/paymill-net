using System.Net.Http;
using PaymillWrapper.Models;
using PaymillWrapper.Utils;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace PaymillWrapper.Service
{
    public class SubscriptionService : AbstractService<Subscription>
    {
        public SubscriptionService(HttpClient client, string apiUrl)
            : base(Resource.Subscriptions, client, apiUrl)
        {
        }

        /// <summary>
        /// This function returns a <see cref="PaymillList"/>of PAYMILL Client objects. In which order this list is returned depends on the
        /// </summary>
        /// <param name="filter">Filter or null</param>
        /// <param name="order">Order or null.</param>
        /// <returns>PaymillList which contains a List of PAYMILL Client object and their total count.</returns>
        public async Task<PaymillWrapper.Models.PaymillList<Subscription>> ListAsync(Subscription.Filter filter, Subscription.Order order)
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
        public async Task<PaymillWrapper.Models.PaymillList<Subscription>> ListAsync(Subscription.Filter filter, Subscription.Order order, int? count, int? offset)
        {
            return await base.listAsync(filter, order, count, offset);
        }

        /// 
        ///  This function creates a {@link Subscription}. Use any of the static create methods in {@link Subscription} and include
        ///  additional options.<br />
        ///  <strong>Example:</strong><br />
        ///  <blockquote>
        ///  
        ///  <pre>
        ///  paymill.getSubscriptionService().create( Subscription.create( "pay_123", "offer_123" ).withClient( "client_123" ))
        ///  paymill.getSubscriptionService().create( Subscription.create( "pay_123", "offer_123" ).withAmount( 100 )) overrides the amount of "offer_123" and sets
        ///  it to 100 for this subscription
        /// </pre>
        /// 
        /// </blockquote>
        /// @param creator
        ///          see {@link Subscription.Creator}.
        /// @return the subscription.
        //

        public async Task<Subscription> CreateAsync(PaymillWrapper.Models.Subscription.Creator creator)
        {
            return await CreateAsync(creator.Payment, creator.Client, creator.Offer, creator.Amount, creator.Currency, creator.Interval,
                creator.StartAt, creator.Name, creator.PeriodOfValidity);
        }

       
        public async Task<Subscription> CreateAsync(Payment payment, Client client, Offer offer, int? amount, String currency, Interval.PeriodWithChargeDay interval, DateTime startAt,
            String name, Interval.Period periodOfValidity)
        {

            if (offer == null && (amount == null || currency == null || interval == null))
            {
                throw new ArgumentException("Either an offer or amount, currency and interval must be set, when creating a subscription");
            }

            ValidationUtils.ValidatesPayment(payment);
            if (client != null)
            {
                ValidationUtils.ValidatesClient(client);
            }
            if (offer != null)
            {
                ValidationUtils.ValidatesOffer(offer);
            }
            if (amount != null)
            {
                ValidationUtils.ValidatesAmount(amount);
                //params.add( "amount", String.valueOf( amount ) );
            }
            if (currency != null)
            {
                ValidationUtils.ValidatesCurrency(currency);
            }
            if (interval != null)
            {
                ValidationUtils.ValidatesIntervalPeriodWithChargeDay(interval);
            }
            if (startAt != null)
            {
                // params.add( "start_at", String.valueOf( startAt.getTime() / 1000 ) );
            }

            if (periodOfValidity != null)
            {
                ValidationUtils.ValidatesIntervalPeriod(periodOfValidity);
                //params.add( "period_of_validity", periodOfValidity.toString() );
            }
            return await createAsync(null,
                        new UrlEncoder().EncodeObject(new
                        {
                            payment = payment.Id,
                            client = client != null ? client.Id : null,
                            offer = offer != null ? offer.Id : null,
                            amount = amount.HasValue ? amount.Value.ToString() : null,
                            currency = currency != null ? currency : null,
                            interval = interval != null ? interval.ToString() : null,
                            startAt = startAt != null ? (startAt.Millisecond / 1000).ToString() : null,
                            name = name != null ? name : null,
                            periodOfValidity = periodOfValidity != null ? periodOfValidity.ToString() : null

                        }));
            //return RestfulUtils.create( SubscriptionService.PATH, params, Subscription.class, super.httpClient );
        }

        protected override string GetResourceId(Subscription obj)
        {
            return obj.Id;
        }


    }
}
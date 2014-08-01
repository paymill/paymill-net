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



        public async Task<Subscription> CreateAsync(String paymentId, String clientId, String offerId, int amount, String currency, Interval.PeriodWithChargeDay interval,
   DateTime startAt, String name, Interval.Period periodOfValidity)
        {
            return await CreateAsync(new Payment(paymentId), new Client(clientId), new Offer(offerId), amount, currency, interval, startAt, name, periodOfValidity);
        }


        public async Task<Subscription> PauseAsync(Subscription subscription)
        {
            return await UpdateParamAsync(subscription, new { param = true });
        }
        public async Task<Subscription> PauseAsync(String subscriptionId)
        {
            return await this.PauseAsync(new Subscription() { Id = subscriptionId });
        }

        public async Task<Subscription> UnpauseAsync(Subscription subscription)
        {
            return await UpdateParamAsync(subscription, new { param = false });
        }


        public async Task<Subscription> UnpauseAsync(String subscriptionId)
        {
            return await this.UnpauseAsync(new Subscription() { Id = subscriptionId });
        }

        public async Task<Subscription> ChangeAmountAsync(String subscriptionId, int amount)
        {
            return await ChangeAmountAsync(new Subscription(subscriptionId), amount);
        }
        public async Task<Subscription> ChangeAmountAsync(String subscriptionId, int amount, String currency, Interval.PeriodWithChargeDay interval)
        {
            return await ChangeAmountAsync(new Subscription(subscriptionId), amount, currency, interval);
        }
        public async Task<Subscription> ChangeAmountAsync(Subscription subscription, int amount)
        {
            return await changeAmountAsync(subscription, amount, 1, null, null);
        }

        public async Task<Subscription> ChangeAmountAsync(Subscription subscription, int amount, String currency, Interval.PeriodWithChargeDay interval)
        {
            return await changeAmountAsync(subscription, amount, 1, currency, interval);
        }
        public async Task<Subscription> ChangeAmountTemporaryAsync(Subscription subscription, int amount)
        {
            return await changeAmountAsync(subscription, amount, 0, null, null);
        }

        public async Task<Subscription> ChangeAmountTemporaryAsync(String subscriptionId, int amount)
        {
            return await ChangeAmountTemporaryAsync(new Subscription(subscriptionId), amount);
        }

        private async Task<Subscription> changeAmountAsync(Subscription subscription, int amount, int type, String currency, Interval.PeriodWithChargeDay interval)
        {
              Dictionary<String, String> param = new Dictionary<String, String>();
              param["amount"] = amount.ToString();
              param["amount_change_type"] = type.ToString(); 
            if (currency != null)
            {
                ValidationUtils.ValidatesCurrency(currency);
                param["currency"] = currency; 
            }
            if (interval != null)
            {
                ValidationUtils.ValidatesIntervalPeriodWithChargeDay(interval);
                param["interval"] = interval.ToString(); 
            }
            return await UpdateParamAsync(subscription, param);
        }


        protected override string GetResourceId(Subscription obj)
        {
            return obj.Id;
        }


    }
}
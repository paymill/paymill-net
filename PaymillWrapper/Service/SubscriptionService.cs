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

        /// <summary>
        ///  This function creates a <see cref="Subscription"/>. Use any of the static create methods in <see cref="Subscription} and include
        ///  additional options.
        ///  </summary>
        ///  Example:
        ///  
        ///  paymill.getSubscriptionService().create( Subscription.create( "pay_123", "offer_123" ).withClient( "client_123" ))
        ///  paymill.getSubscriptionService().create( Subscription.create( "pay_123", "offer_123" ).withAmount( 100 )) overrides the amount of "offer_123" and sets
        ///  it to 100 for this subscription
        /// 
        /// 
        /// 
        /// <param name="creator">Subscription.Creator</param>
        /// <param name="the subscription"></param>
        // <returns>the subscription.</returns>

        public async Task<Subscription> CreateAsync(PaymillWrapper.Models.Subscription.Creator creator, String mandateReference  = null)
        {
            return await CreateAsync(creator.Payment, creator.Client, creator.Offer, creator.Amount, creator.Currency, creator.Interval,
                creator.StartAt, creator.Name, creator.PeriodOfValidity, mandateReference);
        }
        /// <summary>
        /// This function creates a <see cref="Subscription" /> between a  <see cref="Client" /> and an  <see cref="Offer" />. A  <see cref="Client" /> can have several
        ///  <see cref="Subscription" />s to different <see cref="Offer" />s, but only one <see cref="Subscription" /> to the same <see cref="Offer" />. The
        ///  <see cref="Client" />s is charged for each billing interval entered.
        /// NOTE
        /// 
        /// 
        /// As the Subscription create method has a lot of options, we recommend you to use a Subscription.Creator.
        /// <param name="payment">
        ///          A Payment used for charging.</param>
        /// <param name="client"></param>
        /// <param name="offer">
        ///          An Offer to subscribe to. Mandatory only if amount, curreny and interval are not set</param>
        /// <param name="amount">
        ///          Amount to be charged. Mandatory if offer is null.</param>
        /// <param name="currency">
        ///          Currency in which to be charged. Mandatory if offer is null.</param>
        /// <param name="interval">
        ///          Interval of charging. Mandatory if offer is null.</param>
        /// <param name="startAt">
        ///          The date, when the subscription will start charging. If longer than 10 minutes in the future, a preauthorization
        ///          will occur automatically to verify the payment.</param>
        /// <param name="name">
        ///          A name for this subscription</param>
        /// <param name="periodOfValidity">
        ///          if set, the subscription will expire after this period.</param>
        /// <returns>the subscription.</returns>
        ///
        public async Task<Subscription> CreateAsync(Payment payment, Client client, Offer offer, int? amount, String currency, Interval.PeriodWithChargeDay interval, DateTime? startAt,
            String name, Interval.Period periodOfValidity, String mandateReference = null)
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
            }
            if (currency != null)
            {
                ValidationUtils.ValidatesCurrency(currency);
            }
            if (interval != null)
            {
                ValidationUtils.ValidatesIntervalPeriodWithChargeDay(interval);
            }
            if (periodOfValidity != null)
            {
                ValidationUtils.ValidatesIntervalPeriod(periodOfValidity);
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
                            start_at = startAt.HasValue ? DateTimeExtensions.ToUnixTimestamp(startAt.Value).ToString() : null,
                            name = name != null ? name : null,
                            period_of_validity = periodOfValidity != null ? periodOfValidity.ToString() : null,
                            mandate_reference = mandateReference != null ? mandateReference: null
                        }));
        }


        /// <summary>
        /// This function creates a<see cref="Subscription" /> between a <see cref="Client" /> and an <see cref="Offer" />. A <see cref="Client" /> can have several
        /// <see cref="Subscription" />s to different <see cref="Offer" />s, but only one <see cref="Subscription" /> to the same <see cref="Offer" />. The
        /// <see cref="Client" />s is charged for each billing interval entered. 
        /// </summary>
        /// 
        /// NOTE
        /// As the Subscription create method has a lot of options, we recommend you to use a <see cref="Subscription.Creator" />.
        /// 
        /// <param name="paymentId">
        ///          A Payment used for charging.</param>
        /// <param name="clientId"></param>
        /// <param name="offerId">
        ///          An link Offer to subscribe to. Mandatory only if amount, curreny and interval are not set</param>
        /// <param name="amount">
        ///          Amount to be charged. Mandatory if offer is null.</param>
        /// <param name="currency">
        ///          Currency in which to be charged. Mandatory if offer is null.</param>
        /// <param name="interval">
        ///          Interval of charging. Mandatory if offer is null.</param>
        /// <param name="startAt">
        ///          The date, when the subscription will start charging. If longer than 10 minutes in the future, a preauthorization
        ///          will occur automatically to verify the payment.</param>
        /// <param name="name">
        ///          A name for this subscription</param>
        /// <param name="periodOfValidity">
        ///          if set, the subscription will expire after this period.</param>
        /// <returns>the subscription</returns>
        ///
        public async Task<Subscription> CreateAsync(String paymentId, String clientId, String offerId, int? amount, String currency, Interval.PeriodWithChargeDay interval,
   DateTime? startAt, String name, Interval.Period periodOfValidity, String mandateReference = null)
        {
            return await CreateAsync(new Payment(paymentId), new Client(clientId), new Offer(offerId), amount, currency, interval, startAt, name, periodOfValidity, mandateReference);
        }

        /// <summary>
        /// Temporary pauses a subscription. 
        /// </summary>
        /// NOTE
        /// 
        /// Pausing is permitted until one day (24 hours) before the next charge date.
        ///<param name="subscription">the subscription</param>
        ///
        /// <returns> the updated subscription</returns>
        ///
        public async Task<Subscription> PauseAsync(Subscription subscription)
        {
            return await updateParamAsync(subscription.Id, new { pause = "true" });
        }
        ///
        /// Temporary pauses a subscription. 
        ///  NOTE 
        /// Pausing is permitted until one day (24 hours) before the next charge date.
        /// <param name="subscriptionId">
        ///          the Id of the subscription</param>
        ///  <returns> the updated subscription
        /// 
        public async Task<Subscription> PauseAsync(String subscriptionId)
        {
            return await PauseAsync(new Subscription() { Id = subscriptionId });
        }
        /// <summary>
        /// Unpauses a subscription. Next charge will occur according to the defined interval. 
        ///  NOTE 
        /// if the nextCaptureAt is the date of reactivation: a charge will happen 
        /// if the next_capture_at is in the past: it will be set to: reactivationdate + interval  
        ///  </summary>
        ///  
        ///  IMPORTANT 
        /// An inactive subscription can reactivated within 13 month from the date of pausing. After this period, the subscription will
        /// expire and cannot be re-activated. 
        /// <param name="subscription">
        ///          the subscription</param>
        ///  <returns> the updated subscription</returns>
        /// 
        public async Task<Subscription> UnpauseAsync(Subscription subscription)
        {
            return await updateParamAsync(subscription.Id, new { pause = "false" });
        }

        /// <summary>
        /// Unpauses a subscription. Next charge will occur according to the defined interval. 
        ///  NOTE 
        /// if the nextCaptureAt is the date of reactivation: a charge will happen 
        /// if the next_capture_at is in the past: it will be set to: reactivationdate + interval  
        ///  </summary>
        ///  
        ///  IMPORTANT 
        /// An inactive subscription can reactivated within 13 month from the date of pausing. After this period, the subscription will
        /// expire and cannot be re-activated. 
        /// <param name="subscriptionId">
        ///          the Id of the subscription</param>
        ///  <returns> the updated subscription</returns>
        /// 
        public async Task<Subscription> UnpauseAsync(String subscriptionId)
        {
            return await UnpauseAsync(new Subscription() { Id = subscriptionId });
        }

        /// <summary>
        /// Changes the amount of a subscription. The new amount is valid until the end of the subscription. If you want to set a
        /// temporary one-time amount use <see cref="SubscriptionService" />
        ///  </summary>
        ///  
        /// <param name="subscriptionId">
        ///          the Id of the subscription.</param>
        /// <param name="amount"
        ///          the new amount. </param>
        ///  <returns> the updated subscription.</returns>
        /// 
        public async Task<Subscription> ChangeAmountAsync(String subscriptionId, int amount)
        {
            return await ChangeAmountAsync(new Subscription() { Id = subscriptionId }, amount);
        }

        /// <summary>
        /// Changes the amount of a subscription. The new amount is valid until the end of the subscription. If you want to set a
        /// temporary one-time amount use <see cref="SubscriptionService#changeAmountTemporary" />
        ///  </summary>
        ///  
        /// <param name="subscriptionId">
        ///          the Id of the subscription.</param>
        /// <param name="amount">
        ///          the new amount.</param>
        /// <param name="currency">
        ///          optionally, a new currency or null.</param>
        /// <param name="interval">
        ///          optionally, a new interval or null.</param>
        ///  <returns> the updated subscription.</returns>
        /// 
        public async Task<Subscription> ChangeAmountAsync(String subscriptionId, int amount, String currency, Interval.PeriodWithChargeDay interval)
        {
            return await ChangeAmountAsync(new Subscription() { Id = subscriptionId }, amount, currency, interval);
        }

        /// <summary>
        /// Changes the amount of a subscription. The new amount is valid until the end of the subscription. If you want to set a
        /// temporary one-time amount use <see cref="SubscriptionService#changeAmountTemporary" />
        ///  </summary>
        ///  
        /// <param name="subscription">
        ///          the subscription.</param>
        /// <param name="amount">
        ///          the new amount.</param>
        ///  <returns> the updated subscription.</returns>
        /// 
        public async Task<Subscription> ChangeAmountAsync(Subscription subscription, int amount)
        {
            return await changeAmountAsync(subscription, amount, 1, null, null);
        }
        /// <summary>
        /// Changes the amount of a subscription. The new amount is valid until the end of the subscription. If you want to set a
        /// temporary one-time amount use <see cref="SubscriptionService#changeAmountTemporary" />
        ///  </summary>
        ///  
        /// <param name="subscription">
        ///          the subscription.</param>
        /// <param name="amount">
        ///          the new amount.</param>
        /// <param name="currency">
        ///          optionally, a new currency or null.</param>
        /// <param name="interval">
        ///          optionally, a new interval or null.</param>
        ///  <returns> the updated subscription.</returns>
        /// 
        public async Task<Subscription> ChangeAmountAsync(Subscription subscription, int amount, String currency, Interval.PeriodWithChargeDay interval)
        {
            return await changeAmountAsync(subscription, amount, 1, currency, interval);
        }
        /// <summary>
        /// Changes the amount of a subscription. The new amount is valid one-time only after which the original subscription amount will
        /// be charged again. If you want to permanently change the amount use <see cref="SubscriptionService#changeAmount" />
        ///  </summary>
        ///  
        /// <param name="subscription">
        ///          the subscription.</param>
        /// <param name="amount">
        ///          the new amount.</param>
        ///  <returns> the updated subscription.</returns>
        /// 
        public async Task<Subscription> ChangeAmountTemporaryAsync(Subscription subscription, int amount)
        {
            return await changeAmountAsync(subscription, amount, 0, null, null);
        }
        /// <summary>
        /// Changes the amount of a subscription. The new amount is valid one-time only after which the original subscription amount will
        /// be charged again. If you want to permanently change the amount use <see cref="SubscriptionService#changeAmount" />
        ///  </summary>
        ///  
        /// <param name="subscriptionId">
        ///          the Id of the subscription.</param>
        /// <param name="amount">
        ///          the new amount.</param>
        ///  <returns> the updated subscription.</returns>
        /// 
        public async Task<Subscription> ChangeAmountTemporaryAsync(String subscriptionId, int amount)
        {
            return await ChangeAmountTemporaryAsync(new Subscription() { Id = subscriptionId }, amount);
        }

        private async Task<Subscription> changeAmountAsync(Subscription subscription, int amount, int type, String currency, Interval.PeriodWithChargeDay interval)
        {
            if (currency != null)
            {
                ValidationUtils.ValidatesCurrency(currency);
            }
            if (interval != null)
            {
                ValidationUtils.ValidatesIntervalPeriodWithChargeDay(interval);
            }
            return await updateParamAsync(subscription.Id, new {
                amount = amount,
                amount_change_type = type,
                currency = currency != null ? currency : null,
                interval = interval != null ? interval: null
            });
        }

        /// <summary>
        /// Change the offer of a subscription.  
        /// The plan will be changed immediately. The next_capture_at will change to the current date (immediately). A refund will be
        /// given if due.  
        /// If the new amount is higher than the old one, a pro-rata charge will occur. The next charge date is immediate i.e. the
        /// current date. If the new amount is less then the old one, a pro-rata refund will occur. The next charge date is immediate
        /// i.e. the current date.  
        ///  </summary>
        ///  
        ///  IMPORTANT 
        /// Permitted up only until one day (24 hours) before the next charge date.  
        /// <param name="subscription">
        ///          the subscription</param>
        /// <param name="offer">
        ///          the new offer</param>
        ///  <returns> the updated subscription</returns>
        /// 
        public async Task<Subscription> ChangeOfferChangeCaptureDateAndRefundAsync(Subscription subscription, Offer offer)
        {
            return await changeOfferAsync(subscription, offer, 2);
        }

        /// <summary>
        /// Change the offer of a subscription.  
        /// The plan will be changed immediately.The next_capture_at date will remain unchanged. A refund will be given if due.  
        /// If the new amount is higher than the old one, there will be no additional charge. The next charge date will not change. If
        /// the new amount is less then the old one, a refund happens. The next charge date will not change. 
        ///  </summary>
        ///  
        ///  IMPORTANT 
        /// Permitted up only until one day (24 hours) before the next charge date.  
        /// <param name="subscription">
        ///          the subscription</param>
        /// <param name="offer">
        ///          the new offer</param>
        ///  <returns> the updated subscription</returns>
        /// 
        public async Task<Subscription> ChangeOfferKeepCaptureDateAndRefundAsync(Subscription subscription, Offer offer)
        {
            return await changeOfferAsync(subscription, offer, 1);
        }

        /// <summary>
        /// Change the offer of a subscription.  
        /// the plan will be changed immediately. The next_capture_at date will remain unchanged. No refund will be given  
        ///  </summary>
        ///  
        ///  IMPORTANT 
        /// Permitted up only until one day (24 hours) before the next charge date.  
        /// <param name="subscription">
        ///          the subscription</param>
        /// <param name="offer">
        ///          the new offer</param>
        ///  <returns> the updated subscription</returns>
        /// 
        public async Task<Subscription> ChangeOfferKeepCaptureDateNoRefundAsync(Subscription subscription, Offer offer)
        {
            return await changeOfferAsync(subscription, offer, 0);
        }

        private async Task<Subscription> changeOfferAsync(Subscription subscription, Offer offer, int type)
        {
            ValidationUtils.ValidatesOffer(offer);
            return await updateParamAsync(subscription.Id, new {
                offer = offer.Id,
                offer_change_type = type
            });
        }

        /// <summary>
        /// Stop the trial period of a subscription and charge immediately.
        ///  </summary>
        ///  
        /// <param name="subscription">
        ///          the subscription.</param>
        ///  <returns> the updated subscription.</returns>
        /// 
        public async Task<Subscription> EndTrialAsync(Subscription subscription)
        {
            return await updateParamAsync(subscription.Id, new { trial_end = "false" });
        }

        /// <summary>
        /// Change the period of validity for a subscription.
        ///  </summary>
        ///  
        /// <param name="subscription">
        ///          the subscription.</param>
        /// <param name="newValidity">
        ///          the new validity.</param>
        ///  <returns> the updated subscription.</returns>
        /// 
        public async Task<Subscription> LimitValidityAsync(Subscription subscription, Interval.Period newValidity)
        {
            ValidationUtils.ValidatesIntervalPeriod(newValidity);
            return await updateParamAsync(subscription.Id, new { period_of_validity = newValidity.ToString() });
        }

        /// <summary>
        /// Change the period of validity for a subscription.
        ///  </summary>
        ///  
        /// <param name="subscription">
        ///          the subscription.</param>
        /// <param name="newValidity">
        ///          the new validity .</param>
        ///  <returns> the updated subscription.</returns>
        /// 
        public async Task<Subscription> LimitValidityAsync(Subscription subscription, String newValidity)
        {
            return await LimitValidityAsync(subscription, new Interval.Period(newValidity));
        }

        /// <summary>
        /// Change the validity of a subscription to unlimited
        ///  </summary>
        ///  
        /// <param name="subscription">
        ///          the subscription.</param>
        ///  <returns> the updated subscription.</returns>
        /// 
        public async Task<Subscription> UnlimitValidityAsync(Subscription subscription)
        {
            return await updateParamAsync(subscription.Id, new { period_of_validity = "remove" });
        }


        /// <summary>
        /// This function removes an existing subscription. The subscription will be deleted and no pending transactions will be charged.
        /// Deleted subscriptions will not be displayed.
        ///  </summary>
        ///  
        /// <param name="subscription">
        ///          A Subscription with Id to be deleted.</param>
        ///  <returns> the deleted subscription.</returns>
        /// 
        public async Task<Subscription> DeleteAsync(Subscription subscription)
        {
            return await deleteAsync(subscription, true);
        }

        /// <summary>
        /// This function removes an existing subscription. The subscription will be deleted and no pending transactions will be charged.
        /// Deleted subscriptions will not be displayed.
        ///  </summary>
        ///  
        /// <param name="subscriptionId">
        ///          Id of the Subscription, which has to be deleted.</param>
        ///  <returns> the deleted subscription.</returns>
        /// 
        public async Task<Subscription> DeleteAsync(String subscriptionId)
        {
            return await DeleteAsync(new Subscription() { Id = subscriptionId });
        }

        /// <summary>
        /// This function cancels an existing subscription. The subscription will be directly terminated and no pending transactions will
        /// be charged.
        ///  </summary>
        ///  
        /// <param name="subscription">
        ///          A Subscription with Id to be canceled.</param>
        ///  <returns> the canceled subscription.</returns>
        /// 
        public async Task<Subscription> CancelAsync(Subscription subscription)
        {
            return await deleteAsync(subscription, false);
        }

        /// <summary>
        /// This function cancels an existing subscription. The subscription will be directly terminated and no pending transactions will
        /// be charged.
        ///  </summary>
        ///  
        /// <param name="subscriptionId">
        ///          Id of the Subscription, which has to be canceled.</param>
        ///  <returns> the canceled subscription.</returns>
        /// 
        public async Task<Subscription> CancelAsync(String subscriptionId)
        {
            return await CancelAsync(new Subscription() { Id = subscriptionId });
        }
        /// <summary>
        /// Updates a subscription.Following fields will be updated: 
        /// 
        ///  interval (note, that nextCaptureAt will not change.)
        ///  currency
        ///  name
        ///  
        ///  </summary>
        ///  
        /// To update further properties of a subscription use following methods: 
        ///  
        ///  
        ///  <see cref="SubscriptionService#cancel(Subscription)" />to cancel
        ///  <see cref="SubscriptionService#changeAmount(Subscription, Integer)" /> to change the amount
        ///  <see cref="SubscriptionService#changeOfferChangeCaptureDateAndRefund(Subscription, Offer)" /> to change the offer.
        ///  <see cref="SubscriptionService#changeOfferKeepCaptureDateAndRefund(Subscription, Offer)" /> to change the offer.
        ///  <see cref="SubscriptionService#changeOfferKeepCaptureDateNoRefund(Subscription, Offer)" /> to change the offer.
        ///  <see cref="SubscriptionService#endTrial(Subscription)} to end the trial
        ///  <see cref="SubscriptionService#limitValidity(Subscription, com.paymill.models.Interval.Period" /> to change the validity.
        ///  <see cref="SubscriptionService#pause(Subscription)} to pause
        ///  <see cref="SubscriptionService#unlimitValidity(Subscription)" /> to change the validity.
        ///  <see cref="SubscriptionService#unpause(Subscription)} to unpause.
        ///  
        ///  
        /// <param name="subscription">
        ///          Subscription with Id to be updated.</param>
        /// <returns></returns>
        /// 
        private async Task<Subscription> deleteAsync(Subscription subscription, Boolean remove)
        {
            return await deleteParamAsync(subscription.Id, new { remove = remove.ToString().ToLower() });
        }


        protected override string GetResourceId(Subscription obj)
        {
            return obj.Id;
        }


    }
}
using Newtonsoft.Json;
using PaymillWrapper.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PaymillWrapper.Models
{
    /// <summary>
    /// Subscriptions allow you to charge recurring payments on a client’s credit card / to a client’s direct debit. 
    /// A subscription connects a client to the offers-object.
    /// </summary>
    [JsonConverter(typeof(StringToBaseModelConverter<Subscription>))]
    public class Subscription : BaseModel
    {
       [DataMember(Name = "amount")]
        public int? Amount { get; set; }

        [DataMember(Name = "temp_amount")]
        public int? TempAmount { get; set; }

        [DataMember(Name = "currency"),
        Updateable(Name = "currency")]
        public String Currency { get; set; }


        [DataMember(Name = "name"),
        Updateable(Name = "name")]
        public String Name { get; set; }


        [DataMember(Name = "interval"),
        Updateable(Name = "interval")]
        public Interval.PeriodWithChargeDay Interval { get; set; }


        [DataMember(Name = "is_canceled")]
        public Boolean Canceled { get; set; }

        [DataMember(Name = "is_deleted")]
        public Boolean Deleted { get; set; }

        [DataMember(Name = "period_of_validity")]
        public Interval.Period PeriodOfValidity { get; set; }


        [DataMember(Name = "end_of_periods")]
        public DateTime EndOfPeriod { get; set; }


        /// <summary>
        /// Hash describing the offer which is subscribed to the client
        /// </summary>
        [DataMember(Name = "offer"),
        Updateable(Name = "offer", OnlyProperty = "Id")]
        public Offer Offer { get; set; }

        /// <summary>
        /// Whether this subscription was issued while being in live mode or not
        /// </summary>
        [DataMember(Name = "livemode")]
        public bool Livemode { get; set; }

        /// <summary>
        /// Cancel this subscription immediately or at the end of the current period?
        /// </summary>
        [DataMember(Name = "cancel_at_period_end"),
        Updateable(Name = "cancel_at_period_end")]
        public bool CancelAtPeriodEnd { get; set; }

        [DataMember(Name = "trial_start")]
        public DateTime? TrialStart { get; set; }

        [DataMember(Name = "trial_end")]
        public DateTime? TrialEnd { get; set; }

        /// <summary>
        /// Next charge date.
        /// </summary>
        [DataMember(Name = "next_capture_at")]
        public DateTime? NextCaptureAt { get; set; }

        /// <summary>
        /// Cancel date
        /// </summary>
        [DataMember(Name = "canceled_at")]
        public DateTime? CanceledAt { get; set; }

        [DataMember(Name = "status")]
        public SubscriptionStatus Status { get; set; }
        /// <summary>
        /// Client
        /// </summary>
        [DataMember(Name = "client")]
        public Client Client { get; set; }

        /// <summary>
        /// Payment
        /// </summary>
        [DataMember(Name = "payment"),
        Updateable(Name = "payment", OnlyProperty = "Id")]
        public Payment Payment { get; set; }

        [DataMember(Name = "app_id")]
        public String AppId { get; set; }

        [JsonConverter(typeof(StringToBaseEnumTypeConverter<SubscriptionStatus>))]
        public class SubscriptionStatus : EnumBaseType
        {

            public static readonly Subscription.SubscriptionStatus ACTIVE;
            public static readonly Subscription.SubscriptionStatus INACTIVE;

            static SubscriptionStatus()
            {
                ACTIVE = new SubscriptionStatus("active");
                INACTIVE = new SubscriptionStatus("inactive");
            }

            private SubscriptionStatus(String value, Boolean unknowValue = false)
                : base(value, unknowValue)
            {

            }
            public SubscriptionStatus()
                : base("", false)
            {
            }


            public static SubscriptionStatus Create(String value)
            {
                return (SubscriptionStatus)EnumBaseType.GetItemByValue(value, typeof(SubscriptionStatus));
            }

        }

        public static Subscription.Filter CreateFilter()
        {
            return new Subscription.Filter();
        }

        public static Subscription.Order CreateOrder()
        {
            return new Subscription.Order();
        }

        public class Filter : BaseFilter
        {

            [SnakeCase(Value = "offer")]
            private String offerId;

            internal Filter()
            {

            }
            public Subscription.Filter ByOfferId(String offerId)
            {
                this.offerId = offerId;
                return this;
            }
        }
        public class Order : BaseOrder
        {
            [SnakeCase(Value = "offer")]
            private Boolean offer;

            [SnakeCase(Value = "canceled_at")]
            private Boolean canceledAt;

            [SnakeCase(Value = "created_at")]
            private Boolean createdAt;

            internal Order()
            {

            }

            public Subscription.Order Asc()
            {
                base.setAsc();
                return this;
            }

            public Subscription.Order Desc()
            {
                base.setDesc();
                return this;
            }

            public Subscription.Order ByOffer()
            {
                this.offer = true;
                this.createdAt = false;
                this.canceledAt = false;
                return this;
            }

            public Subscription.Order ByCanceledAt()
            {
                this.offer = false;
                this.createdAt = false;
                this.canceledAt = true;
                return this;
            }

            public Subscription.Order ByCreatedAt()
            {
                this.offer = false;
                this.createdAt = true;
                this.canceledAt = false;
                return this;
            }

        }
        public static Creator Create(Payment payment, int amount, String currency, String interval)
        {
            return new Creator(payment, amount, currency, new Interval.PeriodWithChargeDay(interval));

        }

        public static Creator Create(String paymentId, int amount, String currency, String interval)
        {
            return new Creator(new Payment(paymentId), amount, currency, new Interval.PeriodWithChargeDay(interval));

        }

        public static Creator Create(Payment payment, int amount, String currency, Interval.PeriodWithChargeDay interval)
        {
            return new Creator(payment, amount, currency, interval);

        }

        public static Creator Create(String paymentId, int amount, String currency, Interval.PeriodWithChargeDay interval)
        {
            return new Creator(new Payment(paymentId), amount, currency, interval);
        }

        public static Creator Create(Payment payment, Offer offer)
        {
            return new Creator(payment, offer);
        }

        public static Creator Create(String paymentId, Offer offer)
        {
            return new Creator(new Payment(paymentId), offer);

        }

        public static Creator Create(String paymentId, String offerId)
        {
            return new Creator(new Payment(paymentId), new Offer(offerId));

        }

        public static Creator Create(Payment payment, String offerId)
        {
            return new Creator(payment, new Offer(offerId));

        }

        /**
  * Due to the large number of optional parameters, this class is the recommended way to create subscriptions
  */
        public sealed class Creator
        {

            public Payment Payment { get; set; }
            public Client Client { get; set; }
            public Offer Offer { get; set; }
            public int? Amount { get; set; }
            public String Currency { get; set; }
            public Interval.PeriodWithChargeDay Interval { get; set; }
            public DateTime? StartAt { get; set; }
            public String Name { get; set; }
            public Interval.Period PeriodOfValidity { get; set; }

            internal Creator(Payment payment, int amount, String currency, Interval.PeriodWithChargeDay interval)
            {
                Payment = payment;
                Amount = amount;
                Currency = currency;
                this.Interval = interval;
            }

            internal Creator(Payment payment, Offer offer)
            {
                this.Payment = payment;
                this.Offer = offer;
            }

            public Creator WithClient(Client client)
            {
                this.Client = client;
                return this;
            }

            public Creator WithClient(String clientId)
            {
                this.Client = new Client(clientId);
                return this;
            }

            public Creator WithOffer(Offer offer)
            {
                this.Offer = offer;
                return this;
            }

            public Creator WithOffer(String offerId)
            {
                this.Offer = new Offer(offerId);
                return this;
            }

            public Creator WithAmount(int amount)
            {
                Amount = amount;
                return this;
            }

            public Creator WithCurrency(String currency)
            {
                Currency = currency;
                return this;
            }

            public Creator WithInterval(Interval.PeriodWithChargeDay interval)
            {
                Interval = interval;
                return this;
            }

            public Creator WithInterval(String interval)
            {
                Interval = new Interval.PeriodWithChargeDay(interval);
                return this;
            }

            public Creator WithStartDate(DateTime startAt)
            {
                StartAt = startAt;
                return this;
            }

            public Creator WithName(String name)
            {
                Name = name;
                return this;
            }

            public Creator WithPeriodOfValidity(Interval.Period period)
            {
                PeriodOfValidity = period;
                return this;
            }

            public Creator WithPeriodOfValidity(String period)
            {
                PeriodOfValidity = new Interval.Period(period);
                return this;
            }



        }


    }

}
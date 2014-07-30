using PaymillWrapper.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Web;

namespace PaymillWrapper.Models
{
    /// <summary>
    /// An offer is a recurring plan which a user can subscribe to. 
    /// You can create different offers with different plan attributes e.g. a monthly or a yearly based paid offer/plan.
    /// </summary>
    public class Offer : BaseModel
    {
        /// <summary>
        /// Your name for this offer
        /// </summary>
        [DataMember(Name = "name"), Updateable(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Every interval the specified amount will be charged. In test mode only even values e.g. 42.00 = 4200 are allowed
        /// </summary>
        [DataMember(Name = "amount")]
        public int Amount { get; set; }

        /// <summary>
        /// Return formatted amount, e.g. 4200 amount value return 42.00
        /// </summary>
        [IgnoreDataMember]
        public double AmountFormatted
        {
            get
            {
                return Amount / 100.0;
            }
        }

        /// <summary>
        /// Defining how often the client should be charged (week, month, year)
        /// </summary>
        [DataMember(Name = "interval")]
        public Interval.Period Interval { get; set; }

        /// <summary>
        /// Give it a try or charge directly?
        /// </summary>
        [DataMember(Name = "trial_period_days")]
        public int? TrialPeriodDays { get; set; }

        /// <summary>
        /// ISO 4217 formatted currency code
        /// </summary>
        [DataMember(Name = "currency")]
        public string Currency { get; set; }

        /// <summary>
        /// App (ID) that created this offer or null if created by yourself
        /// </summary>
        [DataMember(Name = "app_id")]
        public string AppId { get; set; }

        [DataMember(Name = "subscription_count")]
        public SubscriptionCount SubscriptionCount { get; set; }

        public Offer(String id)
        {
            Id = id;
        }
        public Offer()
        {

        }

        public static Offer.Filter CreateFilter()
        {
            return new Offer.Filter();
        }

        public static Offer.Order CreateOrder()
        {
            return new Offer.Order();
        }

        public sealed class Filter : BaseModel.BaseFilter
        {

            [SnakeCase(Value = "name")]
            private String name;

            [SnakeCase(Value = "trial_period_days")]
            private String trialPeriodDays;

            [SnakeCase(Value = "amount")]
            private String amount;


            internal Filter()
            {

            }

            public Offer.Filter ByName(String name)
            {
                this.name = name;
                return this;
            }

            public Offer.Filter ByTrialPeriodDays(int trialPeriodDays)
            {
                this.trialPeriodDays = trialPeriodDays.ToString();
                return this;
            }

            public Offer.Filter ByAmount(int amount)
            {
                this.amount = amount.ToString();
                return this;
            }

            public Offer.Filter byAmountGreaterThan(int amount)
            {
                this.amount = ">" + amount.ToString();
                return this;
            }

            public Offer.Filter ByAmountLessThan(int amount)
            {
                this.amount = "<" + amount.ToString();
                return this;
            }
            public Offer.Filter ByCreatedAt(DateTime startCreatedAt, DateTime endCreatedAt)
            {
                base.byCreatedAt(startCreatedAt, endCreatedAt);
                return this;
            }

            public Offer.Filter ByUpdatedAt(DateTime startUpdatedAt, DateTime endUpdatedAt)
            {
                base.byUpdatedAt(startUpdatedAt, endUpdatedAt);
                return this;
            }
        }

        public class Order : BaseModel.BaseOrder
        {

            [SnakeCase(Value = "interval")]
            private Boolean interval;

            [SnakeCase(Value = "amount")]
            private Boolean amount;

            [SnakeCase(Value = "created_at")]
            private Boolean createdAt;

            [SnakeCase(Value = "trial_period_days")]
            private Boolean trialPeriodDays;

            internal Order()
            {

            }

            public Offer.Order ByInterval()
            {
                this.interval = true;
                this.amount = false;
                this.createdAt = false;
                this.trialPeriodDays = false;
                return this;
            }

            public Offer.Order ByAmount()
            {
                this.interval = false;
                this.amount = true;
                this.createdAt = false;
                this.trialPeriodDays = false;
                return this;
            }

            public Offer.Order ByCreatedAt()
            {
                this.interval = false;
                this.amount = false;
                this.createdAt = true;
                this.trialPeriodDays = false;
                return this;
            }

            public Offer.Order ByTrialPeriodDays()
            {
                this.interval = false;
                this.amount = false;
                this.createdAt = true;
                this.trialPeriodDays = false;
                return this;
            }
            public Offer.Order Asc()
            {
                base.setAsc();
                return this;
            }

            public Offer.Order Desc()
            {
                base.setDesc();
                return this;
            }
        }

    }
    [DataContract]
    public class SubscriptionCount
    {
        [DataMember(Name = "active")]
        public String Аctive { get; set; }
        [DataMember(Name = "inactive")]
        public int Inactive { get; set; }
    }

   
}
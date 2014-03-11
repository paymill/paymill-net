using PaymillWrapper.Net;
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
        public Interval Interval { get; set; }

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


    }

    [DataContract]
    public class SubscriptionCount
    {
        [DataMember(Name = "active")]
        public String Аctive { get; set; }
        [DataMember(Name = "inactive")]
        public int Inactive { get; set; }
    }
    [Newtonsoft.Json.JsonConverter(typeof(StringToIntervalConverter))]
    public class Interval
    {
        public enum TypeUnit
        {
            DAY,
            WEEK,
            MONTH,
            YEAR
        }
        public int Count { get; set; }
        public TypeUnit Unit { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Count"/> class.
        /// </summary>
        /// <param name="interval">The interval.</param>
        public Interval(String interval)
        {
            String[] parts = interval.Split(' ');
            this.Count = int.Parse(parts[0]);
            this.Unit = CreateUnit(parts[1]);
        }
        /// <summary>
        /// Creates the unit.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">Invalid value for Interval.Unit</exception>
        private static TypeUnit CreateUnit(String value)
        {
            foreach (TypeUnit unit in Enum.GetValues(typeof(TypeUnit)))
            {
                if (String.Compare(unit.ToString(), value, true) == 0)
                {
                    return unit;
                }
            }
            throw new ArgumentException("Invalid value for Interval.Unit");
        }
        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        public override String ToString()
        {
            return String.Format("{0} {1}", this.Count, this.Unit);
        }

    }
}
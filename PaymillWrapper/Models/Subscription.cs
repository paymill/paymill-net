using Newtonsoft.Json;
using PaymillWrapper.Net;
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
    public class Subscription : BaseModel
    {
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
        public DateTime NextCaptureAt { get; set; }

        /// <summary>
        /// Cancel date
        /// </summary>
        [DataMember(Name = "canceled_at")]
        public DateTime? CanceledAt { get; set; }

        /// <summary>
        /// Client
        /// </summary>
        [DataMember(Name = "client")]
        public Client Client { get; set; }
       

        /// <summary>
        /// Payment
        /// </summary>
        [DataMember(Name = "payment"), 
        Updateable(Name = "payment", OnlyProperty="Id")]
        public Payment Payment { get;set;}

       [DataMember(Name = "app_id")]
        public String AppId { get; set; }
     }

}
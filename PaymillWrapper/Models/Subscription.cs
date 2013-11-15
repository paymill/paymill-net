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
    [JsonConverter(typeof(JsonParser<Subscription>))]
    public class Subscription : BaseModel
    {
        private static readonly DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).ToLocalTime();

        /// <summary>
        /// Hash describing the offer which is subscribed to the client
        /// </summary>
        [DataMember(Name = "Offer")]
        public Offer Offer { get; set; }

        /// <summary>
        /// Whether this subscription was issued while being in live mode or not
        /// </summary>
        [DataMember(Name = "Livemode")]
        public bool Livemode { get; set; }

        /// <summary>
        /// Cancel this subscription immediately or at the end of the current period?
        /// </summary>
        [DataMember(Name = "Cancel_At_Period_End")]
        public bool Cancel_At_Period_End { get; set; }

        /// <summary>
        /// Cancel date
        /// </summary>
        public DateTime CanceledAt { get; set; }

        [DataMember(Name = "Canceled_At")]
        public int? Canceled_At_Ticks
        {
            get
            {
                return (int)(this.CanceledAt - unixEpoch).TotalSeconds;
            }
            set
            {
                if (value.HasValue == true)
                {
                    this.CanceledAt = unixEpoch.AddSeconds(value.Value);
                }
            }
        }
        /// <summary>
        /// Client
        /// </summary>
        public Client Client { get; set; }
        /// <summary>
        /// Payment-object
        /// </summary>
        [DataMember(Name = "Client")]
        private Object Client_At_Object
        {
            set
            {
                if (value != null)
                {
                    this.Client = Newtonsoft.Json.JsonConvert.DeserializeObject<Client>(value.ToString());
                }
            }
        }

        /// <summary>
        /// Payment
        /// </summary>
        public Payment Payment { get;set;}

        /// <summary>
        /// Payment-object
        /// </summary>
        [DataMember(Name = "Payment")]
        private Object Payment_At_Object { set {
            if (value != null)
            {
                this.Payment = Newtonsoft.Json.JsonConvert.DeserializeObject<Payment>(value.ToString());
            }
        } }

        /// <summary>
        /// Trial Start
        /// </summary>
        public DateTime TrialStart { get; set; }

        [DataMember(Name = "Trial_Start")]
        public int? TrialStart_At_Ticks
        {
            get
            {
                return (int)(this.TrialStart - unixEpoch).TotalSeconds;
            }
            set {
                if (value.HasValue == true)
                {
                    this.TrialStart = unixEpoch.AddSeconds(value.Value);
                }
            }
        }

        /// <summary>
        /// Trial End
        /// </summary>
        public DateTime TrialEnd { get; set; }
        [DataMember(Name = "Trial_End")]
        public int? TrialEnd_At_Ticks
        {
            get
            {
                return (int)(this.TrialEnd - unixEpoch).TotalSeconds;
            }
            set
            {
                if (value.HasValue == true)
                {
                    this.TrialEnd = unixEpoch.AddSeconds(value.Value);
                }
            }
        }

        /// <summary>
        /// Next Capture At
        /// </summary>
        public DateTime NextCaptureAt { get; set; }

        [DataMember(Name = "Next_Capture_At")]
        public int? Next_Capture_At_Ticks
        {
            get
            {
                return (int)(this.NextCaptureAt - unixEpoch).TotalSeconds;
            }
            set
            {
                if (value.HasValue == true)
                {
                    this.NextCaptureAt = unixEpoch.AddSeconds(value.Value);
                }
            }
        }
    }
}
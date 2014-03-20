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
    /// Refunds are own objects with own calls for existing transactions. 
    /// The refunded amount will be credited to the account of the client.
    /// </summary>
    [JsonConverter(typeof(StringToBaseModelConverter<Refund>))]
    public class Refund : BaseModel
    {
        public enum RefundStatus
        {
            Open, 
            Refunded, 
            Failed
        }

        /// <summary>
        /// Transactions-object
        /// </summary>
        [DataMember(Name = "transaction")]
        public Transaction Transaction { get; set; }

        /// <summary>
        /// The refunded amount
        /// </summary>
        [DataMember(Name = "amount")]
        public int Amount { get; set; }

        /// <summary>
        /// The refunded formatted amount with decimals
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
        /// Indicates the current status of this transaction
        /// </summary>
        [DataMember(Name = "status")]
        public RefundStatus Status { get; set; }

        /// <summary>
        /// The description given for this refund
        /// </summary>
        [DataMember(Name = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Whether this refund happend in test- or in livemode
        /// </summary>
        [DataMember(Name = "livemode")]
        public bool Livemode { get; set; }

        [DataMember(Name = "response_code")]
        public int ResponseCode;
    }
}
using Newtonsoft.Json;
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
    public class Refund : BaseModel
    {
        public enum TypeStatus
        {
            OPEN, 
            REFUNDED, 
            FAILED
        }

        /// <summary>
        /// Transactions-object
        /// </summary>
        [JsonPropertyAttribute("Transaction")]
        public Transaction Transaction { get; set; }

        /// <summary>
        /// The refunded amount
        /// </summary>
        [JsonPropertyAttribute("Amount")]
        public int Amount { get; set; }

        /// <summary>
        /// The refunded formatted amount with decimals
        /// </summary>
        [JsonIgnore]
        public double AmountFormatted
        {
            get
            {
                return Amount / 100;
            }
        }

        /// <summary>
        /// Indicates the current status of this transaction
        /// </summary>
        [JsonPropertyAttribute("Status")]
        public TypeStatus Status { get; set; }

        /// <summary>
        /// The description given for this refund
        /// </summary>
        [JsonPropertyAttribute("Description")]
        public string Description { get; set; }

        /// <summary>
        /// Whether this refund happend in test- or in livemode
        /// </summary>
        [JsonPropertyAttribute("Livemode")]
        public bool Livemode { get; set; }
    }
}
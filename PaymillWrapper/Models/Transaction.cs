using PaymillWrapper.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PaymillWrapper.Models
{
    /// <summary>
    /// A transaction is the charging of a credit card or a direct debit.
    /// </summary>
    public class Transaction : BaseModel
    {
        public enum TypeStatus
        {
            PartialRefunded,
            Refunded,
            Closed,
            Failed,
            Pending,
            Open,
            Preauth,
            Chargeback
        }
        public Transaction(String id)
        {
            Id = id;
        }
        public Transaction()
        {
            this.Fees = new List<Fee>();
        }
        /// <summary>
        /// Amount of this transaction
        /// </summary>
        [DataMember(Name = "amount")]
        public int Amount { get; set; }

        /// <summary>
        /// Formatted amount of this transaction
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
        /// The used amount, smallest possible unit per currency (for euro, we’re calculating the amount in cents)
        /// </summary>
        [DataMember(Name = "origin_amount")]
        public int OriginAmount { get; set; }

        /// <summary>
        /// Formatted origin amount
        /// </summary>
        [IgnoreDataMember]
        public double OriginAmountFormatted
        {
            get
            {
                return OriginAmount / 100.0;
            }
        }

        /// <summary>
        /// Indicates the current status of this transaction, e.g closed means the transaction is sucessfully transfered, refunded means that the amount is fully or in parts refunded
        /// </summary>
        [DataMember(Name = "status")]
        public TypeStatus Status { get; set; }

        /// <summary>
        /// Need a additional description for this transaction? Maybe your shopping cart ID or something like that?
        /// </summary>
        [DataMember(Name = "description"),
        Updateable(Name = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Whether this transaction was issued while being in live mode or not
        /// </summary>
        [DataMember(Name = "livemode")]
        public bool Livemode { get; set; }

        /// <summary>
        /// List refunds-object
        /// </summary>
        [DataMember(Name = "refunds")]
        public List<Refund> Refunds { get; set; }

        /// <summary>
        /// Creditcard-object or directdebit-object
        /// </summary>
        [DataMember(Name = "payment")]
        public Payment Payment { get; set; }

        /// <summary>
        /// Client-object
        /// </summary>
        [DataMember(Name = "client")]
        public Client Client { get; set; }

        /// <summary>
        /// ISO 4217 formatted currency code
        /// </summary>
        [DataMember(Name = "currency")]
        public string Currency { get; set; }

        /// <summary>
        /// A token generated through JavaScript-Bridge Paymill
        /// </summary>
        [DataMember(Name = "token")]
        public string Token { get; set; }

        /// <summary>
        /// Preauthorization-object
        /// </summary>
        [DataMember(Name = "preauthorization")]
        public Preauthorization Preauthorization { get; set; }

        [DataMember(Name = "response_code")]
        public int ResponseCode { get; set; }

        [DataMember(Name = "fees")]
        public List<Fee> Fees { get; set; }

        [DataMember(Name = "is_fraud")]
        public bool IsFraud { get; set; }

        [DataMember(Name = "short_id")]
        public string ShortId { get; set; }

    }
}
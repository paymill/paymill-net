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
    public class Preauthorization : BaseModel
    {
        public enum TypeStatus
        {
            OPEN,
            PENDING,
            CLOSED,
            FAILED, 
            DELETE,
            PREAUTH
        }

        /// <summary>
        /// Amount of this transaction
        /// </summary>
        [DataMember(Name = "Amount")]
        public int Amount { get; set; }

        /// <summary>
        /// Formatted amount of this transaction
        /// </summary>
        [IgnoreDataMember]
        public double AmountFormatted
        {
            get
            {
                return Amount / 100;
            }
        }

        /// <summary>
        /// Whether this transaction was issued while being in live mode or not
        /// </summary>
        [DataMember(Name = "Livemode")]
        public bool Livemode { get; set; }

        /// <summary>
        /// Creditcard-object or directdebit-object
        /// </summary>
        [DataMember(Name = "Payment")]
        public Payment Payment { get; set; }

        /// <summary>
        /// Client-object
        /// </summary>
        [DataMember(Name = "Client")]
        public Client Client { get; set; }

        /// <summary>
        /// ISO 4217 formatted currency code
        /// </summary>
        [DataMember(Name = "Currency")]
        public string Currency { get; set; }

        /// <summary>
        /// A token generated through JavaScript-Bridge Paymill
        /// </summary>
        [DataMember(Name = "Token")]
        public string Token { get; set; }

        /// <summary>
        /// A token generated through JavaScript-Bridge Paymill
        /// </summary>
        [DataMember(Name = "status")]
        public Preauthorization.TypeStatus Status;

    }
}
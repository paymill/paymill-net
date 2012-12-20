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
    /// The Payment object represents a payment with a credit card or via direct debit.
    /// </summary>
    [JsonConverter(typeof(JsonParser<Payment>))]
    public class Payment : BaseModel
    {
        public enum TypePayment
        {
            CREDITCARD, 
            DEBIT
        }

        /// <summary>
        /// enum(creditcard,debit)
        /// </summary>
        [DataMember(Name = "Type")]
        public TypePayment Type { get; set; }

        /// <summary>
        /// The identifier of a client (client-object)
        /// </summary>
        [DataMember(Name = "Client")]
        public string Client { get; set; }

        /// <summary>
        /// Visa or Mastercard
        /// </summary>
        [DataMember(Name = "Card_Type")]
        public string Card_Type { get; set; }

        /// <summary>
        /// Country
        /// </summary>
        [DataMember(Name = "Country")]
        public string Country { get; set; }

        /// <summary>
        /// Expiry month of the credit card
        /// </summary>
        [DataMember(Name = "Expire_Month")]
        public int Expire_Month { get; set; }

        /// <summary>
        /// Expiry year of the credit card
        /// </summary>
        [DataMember(Name = "Expire_Year")]
        public int Expire_Year { get; set; }

        /// <summary>
        /// Name of the card holder
        /// </summary>
        [DataMember(Name = "Card_Holder")]
        public string Card_Holder { get; set; }

        /// <summary>
        /// The last four digits of the credit card
        /// </summary>
        [DataMember(Name = "Last4")]
        public string Last4 { get; set; }

        /// <summary>
        /// The used Bank Code
        /// </summary>
        [DataMember(Name = "Code")]
        public string Code { get; set; }

        /// <summary>
        /// Name of the account holder
        /// </summary>
        [DataMember(Name = "Holder")]
        public string Holder { get; set; }

        /// <summary>
        /// The used account number, for security reasons the number is masked
        /// </summary>
        [DataMember(Name = "Account")]
        public string Account { get; set; }

        /// <summary>
        /// Unique credit card token
        /// </summary>
        [DataMember(Name = "Token")]
        public string Token { get; set; }
    }
}
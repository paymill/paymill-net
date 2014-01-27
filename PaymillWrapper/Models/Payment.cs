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
        [JsonPropertyAttribute("Type")]
        public TypePayment Type { get; set; }

        /// <summary>
        /// The identifier of a client (client-object)
        /// </summary>
        [JsonPropertyAttribute("Client")]
        public string Client { get; set; }

        /// <summary>
        /// Visa or Mastercard
        /// </summary>
        [JsonPropertyAttribute("Card_Type")]
        public string Card_Type { get; set; }

        /// <summary>
        /// Country
        /// </summary>
        [JsonPropertyAttribute("Country")]
        public string Country { get; set; }

        /// <summary>
        /// Expiry month of the credit card
        /// </summary>
        [JsonPropertyAttribute("Expire_Month")]
        public int Expire_Month { get; set; }

        /// <summary>
        /// Expiry year of the credit card
        /// </summary>
        [JsonPropertyAttribute("Expire_Year")]
        public int Expire_Year { get; set; }

        /// <summary>
        /// Name of the card holder
        /// </summary>
        [JsonPropertyAttribute("Card_Holder")]
        public string Card_Holder { get; set; }

        /// <summary>
        /// The last four digits of the credit card
        /// </summary>
        [JsonPropertyAttribute("Last4")]
        public string Last4 { get; set; }

        /// <summary>
        /// The used Bank Code
        /// </summary>
        [JsonPropertyAttribute("Code")]
        public string Code { get; set; }

        /// <summary>
        /// Name of the account holder
        /// </summary>
        [JsonPropertyAttribute("Holder")]
        public string Holder { get; set; }

        /// <summary>
        /// The used account number, for security reasons the number is masked
        /// </summary>
        [JsonPropertyAttribute("Account")]
        public string Account { get; set; }
    
        /// <summary>
        /// App (ID) that created this offer or null if created by yourself
        /// </summary>
        [JsonPropertyAttribute("app_id")]
        public string AppId { get; set; }

     
    }
}
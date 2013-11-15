using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using PaymillWrapper.Net;

namespace PaymillWrapper.Models
{
    /// <summary>
    /// The clients object is used to edit, delete, update clients as well as to permit refunds, subscriptions, 
    /// insert credit card details for a client, edit client details and of course make transactions.
    /// </summary>
    [JsonConverter(typeof(JsonParser<Client>))]
    public class Client : BaseModel
    {
        /// <summary>
        /// Mail address of this client
        /// </summary>
        [DataMember(Name = "Email")]
        public string Email { get; set; }

        /// <summary>
        /// Additional description for this client
        /// </summary>
        [DataMember(Name = "Description")]
        public string Description { get; set; }

        /// <summary>
        /// List creditcard-object or directdebit-object
        /// </summary>
        [DataMember(Name = "Payment")]
        public List<Payment> Payment { get; set; }

        /// <summary>
        /// List suscription-object
        /// </summary>
        [DataMember(Name = "Subscription")]
        public List<Subscription> Subscription { get; set; }
    }

}
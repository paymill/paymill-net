using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using PaymillWrapper.Utils;

namespace PaymillWrapper.Models
{
    /// <summary>
    /// The clients object is used to edit, delete, update clients as well as to permit refunds, subscriptions, 
    /// insert credit card details for a client, edit client details and of course make transactions.
    /// </summary>
    [JsonConverter(typeof(StringToBaseModelConverter<Client>))]
     public class Client : BaseModel
    {
        /// <summary>
        /// Mail address of this client
        /// </summary>
        [DataMember(Name = "email"), Updateable(Name = "email")]
        public string Email { get; set; }

        /// <summary>
        /// Additional description for this client
        /// </summary>
        [DataMember(Name = "description"), Updateable(Name = "description")]
        public string Description { get; set; }

        /// <summary>
        /// List creditcard-object or directdebit-object
        /// </summary>
        [DataMember(Name = "payment")]
        public List<Payment> Payments { get; set; }

        /// <summary>
        /// List suscription-object
        /// </summary>
        [DataMember(Name = "subscription")]
        public List<Subscription> Subscriptions { get; set; }

        [DataMember(Name = "app_id")]
        private String AppId;

        public Client()
        {
        }
        public Client(String id)
        {
            Id = id;
        }

    }

}
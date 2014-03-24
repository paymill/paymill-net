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

        public static Client.Filter CreateFilter()
        {
            return new Client.Filter();
        }

        public static Client.Order CreateOrder()
        {
            return new Client.Order();
        }

        public sealed class Filter : BaseModel.BaseFilter
        {
            [SnakeCase(Value = "payment")]
            private String paymentId;

            [SnakeCase(Value = "subscription")]
            private String subscriptionId;

            [SnakeCase(Value = "offer")]
            private String offerId;

            [SnakeCase(Value = "description")]
            private String description;

            [SnakeCase(Value = "email")]
            private String email;


            internal Filter()
            {
            }

            public Client.Filter ByDescription(String description)
            {
                this.description = description;
                return this;
            }

            public Client.Filter ByEmail(String email)
            {
                this.email = email;
                return this;
            }

            public Client.Filter ByPayment(String paymentId)
            {
                this.paymentId = paymentId;
                return this;
            }

            public Client.Filter BySubscriptionId(String subscriptionId)
            {
                this.subscriptionId = subscriptionId;
                return this;
            }

            public Client.Filter ByOfferId(String offerId)
            {
                this.offerId = offerId;
                return this;
            }
            public Client.Filter ByCreatedAt(DateTime startCreatedAt, DateTime endCreatedAt)
            {
                base.byCreatedAt(startCreatedAt, endCreatedAt);
                return this;
            }

            public Client.Filter ByUpdatedAt(DateTime startUpdatedAt, DateTime endUpdatedAt)
            {
                base.byUpdatedAt(startUpdatedAt, endUpdatedAt);
                return this;
            }
        }

        public sealed class Order : BaseModel.BaseOrder
        {

            [SnakeCase(Value = "email")]
            private Boolean email;

            [SnakeCase(Value = "created_at")]
            private Boolean createdAt;

            [SnakeCase(Value = "creditcard")]
            private Boolean creditCard;

            internal Order()
            {

            }
            public Client.Order ByCreatedAt()
            {
                this.email = false;
                this.createdAt = true;
                this.creditCard = false;
                return this;
            }

            public Client.Order ByCreditCard()
            {
                this.email = false;
                this.createdAt = false;
                this.creditCard = true;
                return this;
            }

            public Client.Order ByEmail()
            {
                this.email = true;
                this.createdAt = false;
                this.creditCard = false;
                return this;
            }
            public Client.Order Asc()
            {
                base.setAsc();
                return this;
            }

            public Client.Order Desc()
            {
                base.setDesc();
                return this;
            }
        }

    }


}
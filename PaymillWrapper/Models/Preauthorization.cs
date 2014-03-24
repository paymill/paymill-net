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
        public enum PreauthorizationStatus
        {
            Open,
            Pending,
            Closed,
            Failed,
            Delete,
            Preauth
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
        /// Whether this transaction was issued while being in live mode or not
        /// </summary>
        [DataMember(Name = "livemode")]
        public bool Livemode { get; set; }

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

        [DataMember(Name = "app_id")]
        public String AppId;

        [DataMember(Name = "status")]
        public PreauthorizationStatus Status;

        public static Preauthorization.Filter CreateFilter()
        {
            return new Preauthorization.Filter();
        }

        public static Preauthorization.Order CreateOrder()
        {
            return new Preauthorization.Order();
        }

        public sealed class Filter : BaseFilter
        {

            [SnakeCase(Value = "client")]
            private String clientId;

            [SnakeCase(Value = "payment")]
            private String paymentId;

            [SnakeCase(Value = "amount")]
            private String amount;


            internal Filter()
            {
            }

            public Preauthorization.Filter ByClientId(String clientId)
            {
                this.clientId = clientId;
                return this;
            }

            public Preauthorization.Filter ByPaymentId(String paymentId)
            {
                this.paymentId = paymentId;
                return this;
            }

            public Preauthorization.Filter ByAmount(int amount)
            {
                this.amount = amount.ToString();
                return this;
            }

            public Preauthorization.Filter ByAmountGreaterThan(int amount)
            {
                this.amount = ">" + amount.ToString();
                return this;
            }

            public Preauthorization.Filter ByAmountLessThan(int amount)
            {
                this.amount = "<" + amount.ToString();
                return this;
            }
        }

        public sealed class Order : BaseOrder
        {

            [SnakeCase(Value = "created_at")]
            private Boolean createdAt;

            internal Order()
            {
            }

            public Preauthorization.Order Asc()
            {
                base.setAsc();
                return this;
            }

            public Preauthorization.Order Desc()
            {
                base.setDesc();
                return this;
            }

            public Preauthorization.Order ByCreatedAt()
            {
                this.createdAt = true;
                return this;
            }

        }

    }
}
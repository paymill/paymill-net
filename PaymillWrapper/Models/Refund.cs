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

        [Newtonsoft.Json.JsonConverter(typeof(StringToBaseEnumTypeConverter<RefundStatus>))]
        public sealed class RefundStatus : EnumBaseType
        {
            public static readonly Refund.RefundStatus OPEN;
            public static readonly Refund.RefundStatus REFUNDED;
            public static readonly Refund.RefundStatus FAILED;
            public static readonly Refund.RefundStatus UNKNOWN;
            private RefundStatus(String value, Boolean unknowValue = false)
                : base(value, unknowValue)
            {

            }
            public RefundStatus()
                : base("", false)
            {
            }
            static RefundStatus()
            {
                OPEN = new RefundStatus("open");
                REFUNDED = new RefundStatus("refunded");
                FAILED = new RefundStatus("failed");
                UNKNOWN = new RefundStatus("", true);
            }
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


        public static Refund.Filter CreateFilter()
        {
            return new Refund.Filter();
        }

        public static Refund.Order CreateOrder()
        {
            return new Refund.Order();
        }

        public sealed class Filter : BaseFilter
        {

            [SnakeCase(Value = "client")]
            private String clientId;

            [SnakeCase(Value = "transaction")]
            private String transactionId;

            [SnakeCase(Value = "amount")]
            private String amount;

            internal Filter()
            {

            }

            public Refund.Filter ByClientId(String clientId)
            {
                this.clientId = clientId;
                return this;
            }

            public Refund.Filter ByTransactionId(String transactionId)
            {
                this.transactionId = transactionId;
                return this;
            }

            public Refund.Filter ByAmount(int amount)
            {
                this.amount = amount.ToString();
                return this;
            }

            public Refund.Filter ByAmountGreaterThan(int amount)
            {
                this.amount = ">" + amount.ToString();
                return this;
            }

            public Refund.Filter ByAmountLessThan(int amount)
            {
                this.amount = "<" + amount.ToString();
                return this;
            }


        }

        public sealed class Order : BaseOrder
        {
            [SnakeCase(Value = "transaction")]
            private Boolean transaction;

            [SnakeCase(Value = "client")]
            private Boolean client;

            [SnakeCase(Value = "amount")]
            private Boolean amount;

            [SnakeCase(Value = "created_at")]
            private Boolean createdAt;


            internal Order()
            {

            }

            public Refund.Order Asc()
            {
                base.setAsc();
                return this;
            }

            public Refund.Order Desc()
            {
                base.setDesc();
                return this;
            }

            public Refund.Order ByTransaction()
            {
                this.transaction = true;
                this.client = false;
                this.amount = false;
                this.createdAt = false;
                return this;
            }

            public Refund.Order ByClient()
            {
                this.transaction = false;
                this.client = true;
                this.amount = false;
                this.createdAt = false;
                return this;
            }

            public Refund.Order ByAmount()
            {
                this.transaction = false;
                this.client = false;
                this.amount = true;
                this.createdAt = false;
                return this;
            }

            public Refund.Order ByCreatedAt()
            {
                this.transaction = false;
                this.client = false;
                this.amount = false;
                this.createdAt = true;
                return this;
            }

        }
    }
}
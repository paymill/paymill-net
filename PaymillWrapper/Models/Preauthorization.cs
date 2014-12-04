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
    /// A transaction is the charging of a credit card or a direct debit.
    /// </summary>
    [JsonConverter(typeof(StringToBaseModelConverter<Preauthorization>))]
    public class Preauthorization : BaseModel
    {
        [Newtonsoft.Json.JsonConverter(typeof(StringToBaseEnumTypeConverter<PreauthorizationStatus>))]
        public sealed class PreauthorizationStatus : EnumBaseType
        {
            public static readonly Preauthorization.PreauthorizationStatus OPEN = new PreauthorizationStatus("open");
            public static readonly Preauthorization.PreauthorizationStatus PENDING = new PreauthorizationStatus("pending");
            public static readonly Preauthorization.PreauthorizationStatus CLOSED = new PreauthorizationStatus("closed");
            public static readonly Preauthorization.PreauthorizationStatus FAILED = new PreauthorizationStatus("failed");
            public static readonly Preauthorization.PreauthorizationStatus DELETE = new PreauthorizationStatus("delete");
            public static readonly Preauthorization.PreauthorizationStatus PREAUTH = new PreauthorizationStatus("preauth");
            public static readonly Preauthorization.PreauthorizationStatus UNKNOWN = new PreauthorizationStatus("", true);
            private PreauthorizationStatus(String value, Boolean unknowValue = false)
                : base(value, unknowValue)
            {

            }
            public PreauthorizationStatus()
                : base("", false)
            {
            }
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

        [DataMember(Name = "transaction")]
        public Transaction Transaction { get; set; }

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

        [DataMember(Name = "description")]
        public String Description;

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
using Newtonsoft.Json;
using PaymillWrapper.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymillWrapper.Models
{
    /// <summary>
    /// A transaction is the charging of a credit card or a direct debit.
    /// </summary>
    public class Transaction : BaseModel
    {
        [Newtonsoft.Json.JsonConverter(typeof(StringToBaseEnumTypeConverter<TransactionStatus>))]
        public sealed class TransactionStatus : EnumBaseType
        {
            public static readonly Transaction.TransactionStatus PARTIAL_REFUNDED;
            public static readonly Transaction.TransactionStatus REFUNDED;
            public static readonly Transaction.TransactionStatus CLOSED;
            public static readonly Transaction.TransactionStatus FAILED;
            public static readonly Transaction.TransactionStatus PENDING;
            public static readonly Transaction.TransactionStatus OPEN;
            public static readonly Transaction.TransactionStatus PREAUTH;
            public static readonly Transaction.TransactionStatus CHARGEBACK;
            public static readonly Transaction.TransactionStatus UNKNOWN;
            private TransactionStatus(String value, Boolean unknowValue = false)
                : base(value, unknowValue)
            {

            }
            public TransactionStatus()
                : base("", false)
            {
            }
            static TransactionStatus()
            {
                PARTIAL_REFUNDED = new TransactionStatus("partial_refunded");
                REFUNDED = new TransactionStatus("refunded");
                CLOSED = new TransactionStatus("closed");
                FAILED = new TransactionStatus("failed");
                PENDING = new TransactionStatus("pending");
                OPEN = new TransactionStatus("open");
                PREAUTH = new TransactionStatus("preauth");
                CHARGEBACK = new TransactionStatus("chargeback");
                UNKNOWN = new TransactionStatus("", true);

            }
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
        [JsonProperty("amount")]
        public int Amount { get; set; }

        /// <summary>
        /// Formatted amount of this transaction
        /// </summary>
        [JsonIgnore]
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
        [JsonProperty("origin_amount")]
        public int OriginAmount { get; set; }

        /// <summary>
        /// Formatted origin amount
        /// </summary>
        [JsonIgnore]
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
        [JsonProperty("status")]
        public TransactionStatus Status { get; set; }

        /// <summary>
        /// Need a additional description for this transaction? Maybe your shopping cart ID or something like that?
        /// </summary>
        [JsonProperty("description"),
        Updateable(Name = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Whether this transaction was issued while being in live mode or not
        /// </summary>
        [JsonProperty("livemode")]
        public bool Livemode { get; set; }

        /// <summary>
        /// List refunds-object
        /// </summary>
        [JsonProperty("refunds")]
        public List<Refund> Refunds { get; set; }

        /// <summary>
        /// Creditcard-object or directdebit-object
        /// </summary>
        [JsonProperty("payment")]
        public Payment Payment { get; set; }

        /// <summary>
        /// Client-object
        /// </summary>
        [JsonProperty("client")]
        public Client Client { get; set; }

        /// <summary>
        /// ISO 4217 formatted currency code
        /// </summary>
        [JsonProperty("currency")]
        public string Currency { get; set; }

        /// <summary>
        /// A token generated through JavaScript-Bridge Paymill
        /// </summary>
        [JsonProperty("token")]
        public string Token { get; set; }

        /// <summary>
        /// Preauthorization-object
        /// </summary>
        [JsonProperty("preauthorization")]
        public Preauthorization Preauthorization { get; set; }

        [JsonProperty("response_code")]
        public int ResponseCode { get; set; }

        [JsonProperty("fees")]
        public List<Fee> Fees { get; set; }

        [JsonProperty("is_fraud")]
        public bool IsFraud { get; set; }

        [JsonProperty("short_id")]
        public string ShortId { get; set; }

        /**
        * Set the mandate reference. SEPA mandate reference, can be optionally specified for direct debit transactions.
        * @param mandateReference
        *          {@link String}
        */
        [JsonProperty("mandate_reference")]
        public string MandateReference { get; set; }

        public static Transaction.Filter CreateFilter()
        {
            return new Transaction.Filter();
        }

        public static Transaction.Order CreateOrder()
        {
            return new Transaction.Order();
        }

        public sealed class Filter : BaseFilter
        {

            [SnakeCase(Value = "client")]
            private String clientId;

            [SnakeCase(Value = "payment")]
            private String paymentId;

            [SnakeCase(Value = "amount")]
            private String amount;

            [SnakeCase(Value = "description")]
            private String description;


            [SnakeCase(Value = "status")]
            private String status;

            internal Filter()
            {

            }

            public Transaction.Filter ByClientId(String clientId)
            {
                this.clientId = clientId;
                return this;
            }

            public Transaction.Filter ByPaymentId(String paymentId)
            {
                this.paymentId = paymentId;
                return this;
            }

            public Transaction.Filter ByAmount(int amount)
            {
                this.amount = amount.ToString();
                return this;
            }

            public Transaction.Filter ByAmountGreaterThan(int amount)
            {
                this.amount = ">" + amount.ToString();
                return this;
            }

            public Transaction.Filter ByAmountLessThan(int amount)
            {
                this.amount = "<" + amount.ToString();
                return this;
            }

            public Transaction.Filter ByDescription(String description)
            {
                this.description = description;
                return this;
            }



            public Transaction.Filter ByStatus(Transaction.TransactionStatus status)
            {
                this.status = status.ToString();
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

            public Transaction.Order Asc()
            {
                base.setAsc();
                return this;
            }

            public Transaction.Order Desc()
            {
                base.setDesc();
                return this;
            }

            public Transaction.Order ByCreatedAt()
            {
                this.createdAt = true;
                return this;
            }

        }

    }
}

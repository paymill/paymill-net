using Newtonsoft.Json;
using PaymillWrapper.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using PaymillWrapper.Utils;
namespace PaymillWrapper.Models
{
    /// <summary>
    /// The Payment object represents a payment with a credit card or via direct debit.
    /// </summary>
    [JsonConverter(typeof(StringToBaseModelConverter<Payment>))]
    public class Payment : BaseModel
    {

        public enum PaymentType
        {
            CreditCard,
            Debit,

        }

        /// <summary>
        /// enum(creditcard,debit)
        /// </summary>
        [DataMember(Name = "type")]
        public PaymentType Type { get; set; }

        // Credit card attributes
        /// <summary>
        /// The identifier of a client (client-object)
        /// </summary>
        [DataMember(Name = "client")]
        public string Client { get; set; }

        /// <summary>
        /// Visa or Mastercard
        /// </summary>
        [DataMember(Name = "card_type")]
        public CardTypes CardType { get; set; }

        /// <summary>
        /// Country
        /// </summary>
        [DataMember(Name = "country")]
        public string Country { get; set; }

        /// <summary>
        /// Expiry month of the credit card
        /// </summary>
        [DataMember(Name = "expire_month")]
        public int ExpireMonth { get; set; }

        /// <summary>
        /// Expiry year of the credit card
        /// </summary>
        [DataMember(Name = "expire_year")]
        public int ExpireYear { get; set; }

        /// <summary>
        /// Name of the card holder
        /// </summary>
        [DataMember(Name = "card_holder")]
        public string CardHolder { get; set; }

        /// <summary>
        /// The last four digits of the credit card
        /// </summary>
        [DataMember(Name = "last4")]
        public string Last4 { get; set; }

        // Direct debit attributes

        /// <summary>
        /// The used Bank Code
        /// </summary>
        [DataMember(Name = "code")]
        public string Code { get; set; }

        /// <summary>
        /// Name of the account holder
        /// </summary>
        [DataMember(Name = "holder")]
        public string Holder { get; set; }

        /// <summary>
        /// The used account number, for security reasons the number is masked
        /// </summary>
        [DataMember(Name = "account")]
        public string Account { get; set; }

        /// <summary>
        /// App (ID) that created this offer or null if created by yourself
        /// </summary>
        [DataMember(Name = "app_id")]
        public string AppId { get; set; }

        public Payment(String id)
        {
            Id = id;
        }
        public Payment()
        {

        }

        [DataContract]
        public sealed class CardTypes : EnumBaseType
        {
            public static readonly Payment.CardTypes VISA = new CardTypes("visa");
            public static readonly Payment.CardTypes MASTERCARD = new CardTypes("mastercard");
            public static readonly Payment.CardTypes MAESTRO = new CardTypes("maestro");
            public static readonly Payment.CardTypes AMEX = new CardTypes("amex");
            public static readonly Payment.CardTypes JCB = new CardTypes("jcb");
            public static readonly Payment.CardTypes DINERS = new CardTypes("diners");
            public static readonly Payment.CardTypes DISCOVER = new CardTypes("discover");
            public static readonly Payment.CardTypes CHINA_UNION_PAY = new CardTypes("china_union_pay");
            public static readonly Payment.CardTypes UNKNOWN = new CardTypes("unknown");
            private CardTypes(String name)
                : base(name)
            {

            }
           
        }
        public static Payment.Filter createFilter()
        {
            return new Payment.Filter();
        }

        public static Payment.Order createOrder()
        {
            return new Payment.Order();
        }
        public sealed class Filter : BaseFilter
        {

            [SnakeCase(Value = "card_type")]
            private String cardType;


            internal Filter()
            {

            }

            public Payment.Filter ByCardType(Payment.CardTypes cardType)
            {
                this.cardType = cardType.ToString();
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

            public Payment.Order Asc()
            {
                base.setAsc();
                return this;
            }

            public Payment.Order Desc()
            {
                base.setDesc();
                return this;
            }

            public Payment.Order ByCreatedAt()
            {
                this.createdAt = true;
                return this;
            }
        }

    }
}
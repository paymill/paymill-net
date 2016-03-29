﻿using Newtonsoft.Json;
using PaymillWrapper.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymillWrapper.Models
{
    /// <summary>
    /// The Payment object represents a payment with a credit card or via direct debit.
    /// </summary>
    [JsonConverter(typeof(StringToBaseModelConverter<Payment>))]
    public class Payment : BaseModel
    {
        [JsonConverter(typeof(StringToBaseEnumTypeConverter<PaymentType>))]
        public sealed class PaymentType : EnumBaseType
        {
            public static readonly Payment.PaymentType CREDIT_CARD;
            public static readonly Payment.PaymentType DEBIT;
            public static readonly Payment.PaymentType UNKNOWN;

            public PaymentType()
                : base("", false)
            {
            }
            private PaymentType(String value, Boolean unknowValue = false)
                : base(value, unknowValue)
            {

            }
            static PaymentType()
            {
                CREDIT_CARD = new PaymentType("creditcard");
                DEBIT = new PaymentType("debit");
                UNKNOWN = new PaymentType("", true);
            }
        }
        [JsonConverter(typeof(StringToBaseEnumTypeConverter<CardTypes>))]
        public sealed class CardTypes : EnumBaseType
        {
            public static readonly Payment.CardTypes VISA;
            public static readonly Payment.CardTypes MASTERCARD;
            public static readonly Payment.CardTypes MAESTRO;
            public static readonly Payment.CardTypes AMEX;
            public static readonly Payment.CardTypes JCB;
            public static readonly Payment.CardTypes DINERS;
            public static readonly Payment.CardTypes DISCOVER;
            public static readonly Payment.CardTypes CHINA_UNION_PAY;
            public static readonly Payment.CardTypes UNKNOWN;
            private CardTypes(String value, Boolean unknowValue = false)
                : base(value, unknowValue)
            {

            }
            public CardTypes()
                : base("", false)
            {
            }
            static CardTypes()
            {
                VISA = new CardTypes("visa");
                MASTERCARD = new CardTypes("mastercard");
                MAESTRO = new CardTypes("maestro");
                AMEX = new CardTypes("amex");
                JCB = new CardTypes("jcb");
                DINERS = new CardTypes("diners");
                DISCOVER = new CardTypes("discover");
                CHINA_UNION_PAY = new CardTypes("china_union_pay");
                UNKNOWN = new CardTypes("unknown", true);
            }
        }
        /// <summary>
        /// creditcard, debit
        /// </summary>
        [JsonProperty("type")]
        public PaymentType Type { get; set; }

        // Credit card attributes
        /// <summary>
        /// The identifier of a client (client-object)
        /// </summary>
        [JsonProperty("client")]
        public Client Client { get; set; }

        /// <summary>
        /// Visa or Mastercard
        /// </summary>
        [JsonProperty("card_type")]
        public CardTypes CardType { get; set; }

        /// <summary>
        /// Country
        /// </summary>
        [JsonProperty("country")]
        public string Country { get; set; }

        /// <summary>
        /// Expiry month of the credit card
        /// </summary>
        [JsonProperty("expire_month")]
        public int ExpireMonth { get; set; }

        /// <summary>
        /// Expiry year of the credit card
        /// </summary>
        [JsonProperty("expire_year")]
        public int ExpireYear { get; set; }

        /// <summary>
        /// Name of the card holder
        /// </summary>
        [JsonProperty("card_holder")]
        public string CardHolder { get; set; }

        /// <summary>
        /// The last four digits of the credit card
        /// </summary>
        [JsonProperty("last4")]
        public string Last4 { get; set; }

        // Direct debit attributes

        /// <summary>
        /// The used Bank Code
        /// </summary>
        [JsonProperty("code")]
        public string Code { get; set; }

        /// <summary>
        /// Name of the account holder
        /// </summary>
        [JsonProperty("holder")]
        public string Holder { get; set; }

        /// <summary>
        /// The used account number, for security reasons the number is masked
        /// </summary>
        [JsonProperty("account")]
        public string Account { get; set; }

        /// <summary>
        /// App (ID) that created this offer or null if created by yourself
        /// </summary>
        [JsonProperty("app_id")]
        public string AppId { get; set; }

        public Payment(String id)
        {
            Id = id;
        }
        public Payment()
        {

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

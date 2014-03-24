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
       
        public enum TypePayment
        {
            CreditCard, 
            Debit,

        }
        public enum TypeCard
        {
            Visa, 
            Mastercard, 
            Mastro, 
            Amex, 
            Jcb,
            Diners, 
            Discover, 
            ChinaUnionPay, 
            Unknown
        };
        /// <summary>
        /// enum(creditcard,debit)
        /// </summary>
        [DataMember(Name = "type")]
        public TypePayment Type { get; set; }

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
        public TypeCard CardType { get; set; }

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

       /* public static Payment.Filter createFilter()
        {
            return new Payment.Filter();
        }

        public static Payment.Order createOrder()
        {
            return new Payment.Order();
        }
         public final static class Filter {

    [SnakeCase(Value ="card_type" )]
    private String cardType;

    [SnakeCase(Value ="created_at" )]
    private String createdAt;

    private Filter() {
   
    }

    public Payment.Filter ByCardType( final Payment.CardType cardType ) {
      this.cardType = cardType.getValue();
      return this;
    }

    public Payment.Filter ByCreatedAt( final Date startCreatedAt, final Date endCreatedAt ) {
      this.createdAt = String.valueOf( startCreatedAt.getTime() ) + "-" + String.valueOf( endCreatedAt.getTime() );
      return this;
    }
  }

  public final static class Order {

    [SnakeCase(Value ="created_at" )
    private boolean createdAt;

    [SnakeCase(Value =value = "asc", order = true )
    private boolean asc;

    [SnakeCase(Value =value = "desc", order = true )
    private boolean desc;

    private Order() {
      super();
    }

    public Payment.Order asc() {
      this.asc = true;
      this.desc = false;
      return this;
    }

    public Payment.Order desc() {
      this.asc = false;
      this.desc = true;
      return this;
    }

    public Payment.Order byCreatedAt() {
      this.createdAt = true;
      return this;
    }
  }
        */
    }
}
using Newtonsoft.Json;
using PaymillWrapper.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PaymillWrapper.Models
{
    [JsonConverter(typeof(StringToBaseModelConverter<Merchant>))]
    public class Invoice : BaseModel
    {
        public Invoice()
        {
        }

        public Invoice(String invoiceNumber)
        {
            this.invoiceNumber = invoiceNumber;
        }

        [DataMember(Name = "invoice_nr")]
        public String InvoiceNumber { get; set; }

        /**
        * formatted netto amount
        */
        public int Netto { get; set; }

        /**
        * formatted brutto amount
        */
        public int Brutto { get; set; }

        /**
         * The invoice status (e.g. sent, trx_ok, trx_failed, invalid_payment, success, 1st_reminder, 2nd_reminder, 3rd_reminder,
         * suspend, canceled, transferred)
         */
        public String Status { get; set; }

        /**
        * the start of this invoice period
        */
        [DataMember(Name = "period_from")]
        public DateTime From { get; set; }

         /**
         * the end of this invoice period
         */
        [DataMember(Name = "period_until")]
        public DateTime Until { get; set; }

        /**
         * ISO 4217 formatted currency code.
         */
        public String Currency { get; set; }

        /**
        * VAT rate of the brutto amount
        */
        [DataMember(Name = "vat_rate")]
        public int VatRate { get; set; }

        /**
        * the billing date
        */
        [DataMember(Name = "billing_date")]
        public DateTime BillingDate { get; set; }

        /**
        * The type: paymill, wirecard, acceptance etc. Indicates if it's a PAYMILL invoice or an acquirer payout.
        */
        [DataMember(Name = "invoice_type")]
        public String InvoiceType { get; set; }

        /**
        * the last payment reminder
        */
        [DataMember(Name = "last_reminder_date")]
        public DateTime LastReminderDate { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication.Models
{
    public class PaymillForm
    {
        public String CardNumber { get; set; }
        public String CardExpiry { get; set; }
        public String CardHoldername { get; set; }
        public String CardCvc { get; set; }
        public int Amount { get; set; }
        public String Currency { get; set; }
    }
}
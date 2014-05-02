using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PaymillWrapper;
using PaymillWrapper.Models;
using System.Collections.Specialized;
using System.Text;

namespace Paymill.Sitecore.Sublayouts
{
    /// <summary>
    /// User control( sublayout) that make Payment Transaction. 
    /// </summary>
    public partial class PaymentSublayout : System.Web.UI.UserControl
    {
        /// <summary>
        /// Delegate for parent control to receive response from Paymill . 
        /// </summary>
        public delegate void TransactionSubmitedDelegate(Transaction trans);

        /// <summary>
        /// Set this public property to receive response from Paymill. 
        /// </summary>
        public TransactionSubmitedDelegate TransactionSubmited { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
           
        }
        /// <summary>
        /// On Submit button click. 
        /// </summary>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            PaymillContext paymill = new PaymillContext("YOUR PRIVATE KEY");
            String token = hToken.Value;
            Payment payment = paymill.PaymentService.CreateWithTokenAsync(token).Result;
            int amount = int.Parse(tbAmount.Text);
            Transaction transaction = paymill.TransactionService.CreateWithPaymentAsync(payment, amount, tbCurrency.Text, "Test API c#").Result;
            /// Yout Transaction Is Complete 
            if (TransactionSubmited != null)
            {
                TransactionSubmited(transaction);
            }
        }
    }
}
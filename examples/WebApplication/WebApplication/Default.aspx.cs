using PaymillWrapper.Models;
using PaymillWrapper.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PaymillWrapper;

namespace WebApplication
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                PaymillContext paymill = new PaymillContext("YOUR PRIVATE KEY");
                String token = hToken.Value;
                Payment payment = paymill.PaymentService.CreateWithTokenAsync(token).Result;
                int amount = int.Parse(tbAmount.Text)*100;
                Transaction transaction = paymill.TransactionService.CreateWithPaymentAsync(payment, amount, tbCurrency.Text, "Test API c#").Result;
                /// Yout Transaction Is Complete 
            }
        }

       
    }
}
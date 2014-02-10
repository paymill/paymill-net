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
                Paymill.ApiKey = "YOUR PRIVATE KEY";
                Paymill.ApiUrl = "https://api.paymill.de/v2";
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                String token = hToken.Value;
                PaymentService paymentService = Paymill.GetService<PaymentService>();
                Payment payment = paymentService.Create(token);
                TransactionService transactionService = Paymill.GetService<TransactionService>();
                Transaction transaction = new Transaction();
                transaction.Amount = int.Parse(tbAmount.Text);
                transaction.Currency = tbCurrency.Text;
                transaction.Description = "Test API c#";
                transaction.Payment = payment;
                transaction = transactionService.Create(transaction, null);

                /// Yout Transaction Is Complete 

            }
        }
    }
}
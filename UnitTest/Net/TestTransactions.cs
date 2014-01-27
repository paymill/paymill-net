using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymillWrapper;
using PaymillWrapper.Service;
using System.Collections.Generic;
using PaymillWrapper.Models;

namespace UnitTest.Net
{
    [TestClass]
    public class TestTransactions
    {
        [TestInitialize]
        public void Initialize()
        {
            Paymill.ApiKey = "532bd35a7cb65eda489b2fc71bf0ca8f";
            Paymill.ApiUrl = "https://api.paymill.de/v2";
        }
       
        [TestMethod]
        public void UpdateTransaction()
        {
            TransactionService service = Paymill.GetService<TransactionService>();
            Transaction transaction = new Transaction();
            PaymentService paymentService = Paymill.GetService<PaymentService>();
            ClientService clientService = Paymill.GetService<ClientService>();
            Payment payment = paymentService.Create("098f6bcd4621d373cade4e832627b4f6");
            transaction.Amount = 3500;
            transaction.Currency = "EUR";
            transaction.Description = "Test API c#";
            transaction.Payment = payment;
            transaction = service.Create(transaction, null);
            transaction.Client = clientService.Create("javicantos22@hotmail.es", "Test API");

            Assert.IsTrue(transaction.Id != String.Empty, "Create Transaction Fail");

            transaction.Description = "My updated transaction description";
            var updatetedClient = service.Update(transaction);
            Assert.IsTrue(transaction.Description == "My updated transaction description", "Update Transaction Failed");

        }
    }
}

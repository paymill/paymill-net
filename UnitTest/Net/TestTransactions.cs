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
        Paymill _paymill = null;
        [TestInitialize]
        public void Initialize()
        {
            _paymill = new Paymill("9a4129b37640ea5f62357922975842a1");
        }

        [TestMethod]
        public void UpdateTransaction()
        {
            Payment payment = _paymill.PaymentService.CreateWithTokenAsync("098f6bcd4621d373cade4e832627b4f6").Result;
            Transaction transaction = _paymill.TransactionService.CreateWithPaymentAsync(payment, 3500, "EUR", "Test API C#").Result;
            transaction.Client = _paymill.ClientService.CreateWithEmailAndDescriptionAsync("javicantos22@hotmail.es", "Test API").Result;

            Assert.IsTrue(transaction.Id != String.Empty, "Create Transaction Fail");

            transaction.Description = "My updated transaction description";
            var updateted = _paymill.TransactionService.UpdateAsync(transaction).Result;
            Assert.IsTrue(updateted.Description == "My updated transaction description", "Update Transaction Failed");
            Assert.IsTrue(transaction.Amount == updateted.Amount, "Update Transaction Failed");
        }
        [TestMethod]
        public void ListTransaction()
        {
            var list = _paymill.TransactionService.ListAsync().Result;
            Assert.IsTrue(list.Count > 0, "List Transaction Failed");
        }
    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymillWrapper;
using PaymillWrapper.Service;
using System.Collections.Generic;
using PaymillWrapper.Models;
using PaymillWrapper.Utils;

namespace UnitTest.Net
{
    [TestClass]
    public class TestTransactions
    {
        PaymillContext _paymill = null;
        String testToken = "098f6bcd4621d373cade4e832627b4f6";
        [TestInitialize]
        public void Initialize()
        {
            _paymill = new PaymillContext("0ecdb65b3c7caeb2e10932699dacd50c");
        }
        [TestMethod]
        public void CreateTransactionWithToken()
        {
            Transaction transaction = _paymill.TransactionService.CreateWithTokenAsync(testToken, 3500, "EUR").Result;
            Assert.IsFalse(String.IsNullOrEmpty( transaction.Id));
         }
        [TestMethod]
        public void CreateTransactionWithTokenAndDescription()
        {
            Transaction transaction = _paymill.TransactionService.CreateWithTokenAsync(testToken, 3500, "EUR", "Bar").Result;
            Assert.IsFalse(String.IsNullOrEmpty(transaction.Id));
            Assert.IsTrue(transaction.Description == "Bar");
            Assert.IsTrue(transaction.Currency == "EUR");
            Assert.IsTrue(transaction.Amount == 3500);
        }
      
        [TestMethod]
        public void CreateTransactionWithPayment()
        {
            Payment payment = _paymill.PaymentService.CreateWithTokenAsync(testToken).Result;
            Transaction transaction = _paymill.TransactionService.CreateWithPaymentAsync(payment, 3500, "USD").Result;
            Assert.IsFalse(String.IsNullOrEmpty(transaction.Id));
            Assert.IsFalse(transaction.Currency == "EUR");
            Assert.IsTrue(transaction.Currency == "USD");
            Assert.IsTrue(transaction.Amount == 3500);
            Assert.IsTrue(transaction.Payment.Id == payment.Id);
        }
        [TestMethod]
         public void CreateTransactionWithPaymentAndMandateReference()
        {
            Payment payment = _paymill.PaymentService.CreateWithTokenAsync(testToken).Result;
            Transaction transaction = _paymill.TransactionService.CreateWithPaymentAsync(payment, 3500, "USD", "DE1234TEST").Result;
            Assert.IsFalse(String.IsNullOrEmpty(transaction.Id));
            Assert.IsFalse(transaction.Currency == "EUR");
            Assert.IsTrue(transaction.Currency == "USD");
            Assert.IsTrue(transaction.Amount == 3500);
            Assert.IsTrue(transaction.Payment.Id == payment.Id);
        }
        
        [TestMethod]
        public void CreateTransactionWithPaymentId()
        {
            Payment payment = _paymill.PaymentService.CreateWithTokenAsync(testToken).Result;
            Transaction transaction = _paymill.TransactionService.CreateWithPaymentAsync(payment.Id, 4500, "USD", "Bar boo").Result;
            Assert.IsFalse(String.IsNullOrEmpty(transaction.Id));
            Assert.IsFalse(transaction.Currency == "EUR");
            Assert.IsTrue(transaction.Currency == "USD");
            Assert.IsTrue(transaction.Amount == 4500);
            Assert.IsTrue(transaction.Payment.Id == payment.Id);
            Assert.IsTrue(transaction.Description == "Bar boo");
        }
        [TestMethod]
        public void CreateTransactionWithPaymentIdAndMandateReference()
        {
            Payment payment = _paymill.PaymentService.CreateWithTokenAsync(testToken).Result;
            Transaction transaction = _paymill.TransactionService.CreateWithPaymentAsync(payment.Id, 4500, "USD", "Bar boo", "DE1234TEST").Result;
            Assert.IsFalse(String.IsNullOrEmpty(transaction.Id));
            Assert.IsFalse(transaction.Currency == "EUR");
            Assert.IsTrue(transaction.Currency == "USD");
            Assert.IsTrue(transaction.Amount == 4500);
            Assert.IsTrue(transaction.Payment.Id == payment.Id);
            Assert.IsTrue(transaction.Description == "Bar boo");
        }
        [TestMethod]
        public void CreateTransactionWithPaymentAndClient()
        {
            Client client = _paymill.ClientService.CreateAsync().Result;
            Payment payment = _paymill.PaymentService.CreateWithTokenAndClientAsync(testToken, client).Result;
            Transaction transaction = _paymill.TransactionService.CreateWithPaymentAndClientAsync(payment, client, 4200, "USD").Result;
            Assert.IsFalse(String.IsNullOrEmpty(transaction.Id));
            Assert.IsFalse(transaction.Currency == "EUR");
            Assert.IsTrue(transaction.Currency == "USD");
            Assert.IsTrue(transaction.Amount == 4200);
            Assert.IsTrue(transaction.Payment.Id == payment.Id);
            Assert.IsTrue(transaction.Client.Id == client.Id);
            Assert.IsNull(transaction.Description);
        }
        [TestMethod]
        public void CreateTransactionWithPaymentIdAndClientId()
        {
            Client client = _paymill.ClientService.CreateAsync().Result;
            Payment payment = _paymill.PaymentService.CreateWithTokenAndClientAsync(testToken, client).Result;
            Transaction transaction = _paymill.TransactionService.CreateWithPaymentAndClientAsync(payment.Id, client.Id, 4200, "USD").Result;
            Assert.IsFalse(String.IsNullOrEmpty(transaction.Id));
            Assert.IsFalse(transaction.Currency == "EUR");
            Assert.IsTrue(transaction.Currency == "USD");
            Assert.IsTrue(transaction.Amount == 4200);
            Assert.IsTrue(transaction.Payment.Id == payment.Id);
            Assert.IsTrue(transaction.Client.Id == client.Id);
            Assert.IsNull(transaction.Description);
        }

        [TestMethod]
        public void CreateTransactionWithPaymentAndClientAndDescrition()
        {
            Client client = _paymill.ClientService.CreateAsync().Result;
            Payment payment = _paymill.PaymentService.CreateWithTokenAndClientAsync(testToken, client).Result;
            Transaction transaction = _paymill.TransactionService.CreateWithPaymentAndClientAsync(payment, client, 4200, "EUR", "Bar bar").Result;
            Assert.IsFalse(String.IsNullOrEmpty(transaction.Id));
            Assert.IsTrue(transaction.Currency == "EUR");
            Assert.IsTrue(transaction.Amount == 4200);
            Assert.IsTrue(transaction.Payment.Id == payment.Id);
            Assert.IsTrue(transaction.Client.Id == client.Id);
            Assert.IsTrue(transaction.Description == "Bar bar");
        }
        [TestMethod]
        public void CreateTransactionWithPreauthorization()
        {
            Preauthorization preauthorization = _paymill.PreauthorizationService.CreateWithTokenAsync(testToken, 4200, "USD", null).Result;
            Transaction transaction = _paymill.TransactionService.CreateWithPreauthorizationAsync(preauthorization, 4200, "USD").Result;
            Assert.IsFalse(String.IsNullOrEmpty(transaction.Id));
            Assert.IsTrue(transaction.Currency == "USD");
            Assert.IsTrue(transaction.Amount == 4200);
        }
        [TestMethod]
        public void CreateTransactionWithPreauthorizationAndDescription()
        {
            Preauthorization preauthorization = _paymill.PreauthorizationService.CreateWithTokenAsync(testToken, 4200, "USD", null).Result;
            Transaction transaction = _paymill.TransactionService.CreateWithPreauthorizationAsync(preauthorization, 4200, "USD", "Bar bar").Result;
            Assert.IsFalse(String.IsNullOrEmpty(transaction.Id));
            Assert.IsTrue(transaction.Currency == "USD");
            Assert.IsTrue(transaction.Amount == 4200);
            Assert.IsTrue(transaction.Description == "Bar bar");
        }
      
        [TestMethod]
        public void GetTransactions()
        {
            PaymillList<Transaction> lstTransactions = _paymill.TransactionService.ListAsync().Result;
            Assert.IsTrue(lstTransactions.DataCount > 0);
        }
        [TestMethod]
        public void ListOrderByCreatedAt()
        {
            Transaction.Order orderDesc = Transaction.CreateOrder().ByCreatedAt().Desc();
            Transaction.Order orderAsc = Transaction.CreateOrder().ByCreatedAt().Asc();

            List<Transaction> transactionsDesc = _paymill.TransactionService.ListAsync(null, orderDesc).Result.Data;
            List<Transaction> transactionsAsc = _paymill.TransactionService.ListAsync(null, orderAsc).Result.Data;
            if (transactionsDesc.Count > 1
                && transactionsAsc.Count > 1)
            {
                Assert.AreNotEqual(transactionsDesc[0].Id, transactionsAsc[0].Id);
            }
        }
        [TestMethod]
        public void GetTransactionsById()
        {
            Transaction.Order orderDesc = Transaction.CreateOrder().ByCreatedAt().Desc();
            Transaction.Order orderAsc = Transaction.CreateOrder().ByCreatedAt().Asc();

            List<Transaction> transactionsDesc = _paymill.TransactionService.ListAsync(null, orderDesc).Result.Data;
            List<Transaction> transactionsAsc = _paymill.TransactionService.ListAsync(null, orderAsc).Result.Data;
            if (transactionsDesc.Count > 1
                && transactionsAsc.Count > 1)
            {
                foreach (var tran in transactionsDesc)
                {
                   var getTr = _paymill.TransactionService.GetAsync(tran.Id).Result;
                }
             }
        }

        [TestMethod]
        public void UpdateTransaction()
        {
            Payment payment = _paymill.PaymentService.CreateWithTokenAsync(testToken).Result;
            Transaction transaction = _paymill.TransactionService.CreateWithPaymentAsync(payment, 3500, "EUR", "Test API C#").Result;
            transaction.Client = _paymill.ClientService.CreateWithEmailAndDescriptionAsync("lovely-client@example.com", "Test API").Result;

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
            Assert.IsTrue(list.DataCount > 0, "List Transaction Failed");
        }
    }
}

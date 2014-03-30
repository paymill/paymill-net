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
    public class TestRefundes
    {

        Paymill _paymill = null;
        String testToken = "098f6bcd4621d373cade4e832627b4f6";

        [TestInitialize]
        public void Initialize()
        {
            _paymill = new Paymill("9a4129b37640ea5f62357922975842a1");
        }
        [TestMethod]
        public void GetRefunds()
        {
            PaymillList<Refund> lstRefunds = _paymill.RefundService.ListAsync().Result;
            Assert.IsTrue(lstRefunds.DataCount > 0, "List Refunds failed");
        }
        [TestMethod]
        public void RefundTransactionIdWithAmount()
        {
            var rand = new Random((int)DateTime.Now.Ticks);
            int randomValue = rand.Next(1000, 9999);
            Transaction transaction = _paymill.TransactionService.CreateWithTokenAsync(testToken, randomValue, "USD").Result;
            Refund newRefund = _paymill.RefundService.RefundTransactionAsync(transaction.Id, randomValue).Result;
            Assert.IsTrue(newRefund.Status == Refund.RefundStatus.REFUNDED, "Create Refund succeed");
            Assert.IsTrue(newRefund.Transaction.Status == Transaction.TransactionStatus.REFUNDED, "Create Refund succeed");
            Assert.IsTrue(newRefund.Transaction.Amount == 0);
            Assert.IsTrue(newRefund.Transaction.ResponseCode == 20000);
        }
        [TestMethod]
        public void RefundTransactionWithAmount()
        {
            var rand = new Random((int)DateTime.Now.Ticks);
            int randomValue = rand.Next(1000, 9999);
            Transaction transaction = _paymill.TransactionService.CreateWithTokenAsync(testToken, randomValue, "USD").Result;
            Refund newRefund = _paymill.RefundService.RefundTransactionAsync(transaction, randomValue).Result;
            Assert.IsTrue(newRefund.Status == Refund.RefundStatus.REFUNDED, "Create Refund succeed");
            Assert.IsTrue(newRefund.Transaction.Status == Transaction.TransactionStatus.REFUNDED, "Create Refund succeed");
            Assert.IsTrue(newRefund.Transaction.Amount == 0);
            Assert.IsTrue(newRefund.Transaction.ResponseCode == 20000);
        }
        [TestMethod]
        public void RefundTransactionIdAndAmountAndDescrition()
        {
            var rand = new Random((int)DateTime.Now.Ticks);
            int randomValue = rand.Next(1000, 9999);
            Transaction transaction = _paymill.TransactionService.CreateWithTokenAsync(testToken, randomValue, "USD").Result;
            int refInt = randomValue/2;
            Refund newRefund = _paymill.RefundService.RefundTransactionAsync(transaction.Id, refInt, "Go to bar").Result;
            Assert.IsTrue(newRefund.Status == Refund.RefundStatus.REFUNDED, "Create Refund succeed");
            Assert.IsTrue(newRefund.Transaction.Status == Transaction.TransactionStatus.PARTIAL_REFUNDED, "Create Refund succeed");
            Assert.IsTrue(newRefund.Transaction.Amount == (randomValue - refInt));
            Assert.IsTrue(newRefund.Transaction.ResponseCode == 20000);
            Assert.IsTrue(newRefund.Description == "Go to bar");
            _paymill.RefundService.RefundTransactionAsync(transaction.Id, (randomValue - refInt), "Go to bar").Wait();
        }
        [TestMethod]
        public void RefundTransactionAndAmountAndDescrition()
        {
            var rand = new Random((int)DateTime.Now.Ticks);
            int randomValue = rand.Next(1000, 9999);
            Transaction transaction = _paymill.TransactionService.CreateWithTokenAsync(testToken, randomValue, "USD").Result;
            Refund newRefund = _paymill.RefundService.RefundTransactionAsync(transaction, randomValue, "Go to bar").Result;
            Assert.IsTrue(newRefund.Status == Refund.RefundStatus.REFUNDED, "Create Refund succeed");
            Assert.IsTrue(newRefund.Transaction.Status == Transaction.TransactionStatus.REFUNDED, "Create Refund succeed");
            Assert.IsTrue(newRefund.Transaction.Amount == 0);
            Assert.IsTrue(newRefund.Transaction.ResponseCode == 20000);
            Assert.IsTrue(newRefund.Description == "Go to bar");
        }
        [TestMethod]
        public void ListRefunds()
        {
            PaymillList<Refund> refund = _paymill.RefundService.ListAsync().Result;
            Assert.IsTrue(refund.DataCount > 0);
        }

        [TestMethod]
        public void GetRefund()
        {
            PaymillList<Refund> refunds = _paymill.RefundService.ListAsync().Result;
            Refund refund =_paymill.RefundService.GetAsync(refunds.Data[0].Id).Result;
            Assert.IsFalse(String.IsNullOrEmpty(refund.Id));
        }
        [TestMethod]
        public void ListOrderByFilterAmountGreaterThan()
        {
            int amount = 300;
            Refund.Filter filter = Refund.CreateFilter().ByAmountGreaterThan(amount);
            List<Refund> refunds = _paymill.RefundService.ListAsync(filter, null).Result.Data;
            foreach (var pre in refunds)
            {
                Assert.IsFalse(pre.Amount <= amount);
            }
        }

        [TestMethod]
        public void ListOrderByFilterAmountLessThan()
        {
            int amount = 600;
            Refund.Filter filter = Refund.CreateFilter().ByAmountLessThan(amount);
            List<Refund> refunds = _paymill.RefundService.ListAsync(filter, null).Result.Data;
            foreach (var pre in refunds)
            {
                Assert.IsFalse(pre.Amount >= amount);
            }
        }
    }
}

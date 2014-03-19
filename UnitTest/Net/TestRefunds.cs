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
            List<Refund> lstRefunds = _paymill.RefundService.ListAsync().Result;
            Assert.IsTrue(lstRefunds.Count > 0, "List Refunds failed");
        }
        [TestMethod]
        public void GetRefundsWithFilters()
        {
            Filter filter = new Filter();
            filter.Add("count", 5);
            List<Refund> lstRefunds = _paymill.RefundService.ListAsync(filter).Result;
            Assert.IsTrue(lstRefunds.Count > 0, "List Refunds failed");
        }
        [TestMethod]
        public void RefundTransactionIdWithAmount()
        {
            Transaction transaction = _paymill.TransactionService.CreateWithTokenAsync(testToken, 1000, "USD").Result;
            Refund newRefund = _paymill.RefundService.RefundTransactionAsync(transaction.Id, 500).Result;
            Assert.IsTrue(newRefund.Status == Refund.RefundStatus.Refunded, "Create Refund succeed");
            Assert.IsTrue(newRefund.Transaction.Status == Transaction.TransactionStatus.Partial_Refunded, "Create Refund succeed");
            Assert.IsTrue(newRefund.Transaction.Amount == 500);
            Assert.IsTrue(newRefund.Transaction.ResponseCode == 20000);
        }
        [TestMethod]
        public void RefundTransactionWithAmount()
        {
            Transaction transaction = _paymill.TransactionService.CreateWithTokenAsync(testToken, 1000, "USD").Result;
            Refund newRefund = _paymill.RefundService.RefundTransactionAsync(transaction, 500).Result;
            Assert.IsTrue(newRefund.Status == Refund.RefundStatus.Refunded, "Create Refund succeed");
            Assert.IsTrue(newRefund.Transaction.Status == Transaction.TransactionStatus.Partial_Refunded, "Create Refund succeed");
            Assert.IsTrue(newRefund.Transaction.Amount == 500);
            Assert.IsTrue(newRefund.Transaction.ResponseCode == 20000);
        }
        [TestMethod]
        public void RefundTransactionIdAndAmountAndDescrition()
        {
            Transaction transaction = _paymill.TransactionService.CreateWithTokenAsync(testToken, 1000, "USD").Result;
            Refund newRefund = _paymill.RefundService.RefundTransactionAsync(transaction.Id, 500, "Go to bar").Result;
            Assert.IsTrue(newRefund.Status == Refund.RefundStatus.Refunded, "Create Refund succeed");
            Assert.IsTrue(newRefund.Transaction.Status == Transaction.TransactionStatus.Partial_Refunded, "Create Refund succeed");
            Assert.IsTrue(newRefund.Transaction.Amount == 500);
            Assert.IsTrue(newRefund.Transaction.ResponseCode == 20000);
            Assert.IsTrue(newRefund.Description == "Go to bar");
        }
        [TestMethod]
        public void RefundTransactionAndAmountAndDescrition()
        {
            Transaction transaction = _paymill.TransactionService.CreateWithTokenAsync(testToken, 1000, "USD").Result;
            Refund newRefund = _paymill.RefundService.RefundTransactionAsync(transaction, 500, "Go to bar").Result;
            Assert.IsTrue(newRefund.Status == Refund.RefundStatus.Refunded, "Create Refund succeed");
            Assert.IsTrue(newRefund.Transaction.Status == Transaction.TransactionStatus.Partial_Refunded, "Create Refund succeed");
            Assert.IsTrue(newRefund.Transaction.Amount == 500);
            Assert.IsTrue(newRefund.Transaction.ResponseCode == 20000);
            Assert.IsTrue(newRefund.Description == "Go to bar");
        }
        [TestMethod]
        public void ListRefunds()
        {
            List<Refund> refund = _paymill.RefundService.ListAsync().Result;
            Assert.IsTrue(refund.Count > 0);
        }

        [TestMethod]
        public void GetRefund()
        {
            List<Refund> refunds = _paymill.RefundService.ListAsync().Result;
            Refund refund =_paymill.RefundService.GetAsync(refunds[0].Id).Result;
            Assert.IsFalse(String.IsNullOrEmpty(refund.Id));
        }
    }
}

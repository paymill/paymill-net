using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymillWrapper;
using PaymillWrapper.Service;
using System.Collections.Generic;
using PaymillWrapper.Models;
using PaymillWrapper.Exceptions;

namespace UnitTest.Net
{
    [TestClass]
    public class TestPayments
    {
        Paymill _paymill = null;
        [TestInitialize]
        public void Initialize()
        {
            _paymill = new Paymill("9a4129b37640ea5f62357922975842a1");
        }
        [TestMethod]
        public void GetPayments()
        {
            IReadOnlyCollection<Payment> lstPayments = _paymill.PaymentService.ListAsync().Result;
            Assert.IsFalse(lstPayments.Count == 0, "GetPayments Fail");
         }
        [TestMethod]
        public void CreatePayment()
        {
            Payment payment = _paymill.PaymentService.CreateWithTokenAsync("098f6bcd4621d373cade4e832627b4f6").Result;
            Assert.IsFalse(payment.Id == String.Empty, "CreatePayment Fail");
        }
        [TestMethod]
        public void CreatePaymentWithclient()
        {
            Client client = _paymill.ClientService.CreateWithEmailAndDescriptionAsync("lovely-client@example.com", "Lovely Client").Result;
            Assert.IsTrue(client.Id != String.Empty, "CreateClient Fail");

            Payment payment = _paymill.PaymentService.CreateWithTokenAndClientAsync("098f6bcd4621d373cade4e832627b4f6", client.Id).Result;
            Assert.IsFalse(payment.Id == String.Empty, "CreatePayment With Client Fail");
            Assert.IsFalse(payment.Client != client.Id, "Client does not match");
        }

        [TestMethod]
        public void DeletePayment()
        {
            Payment payment = _paymill.PaymentService.CreateWithTokenAsync("098f6bcd4621d373cade4e832627b4f6").Result;
            bool reply = _paymill.PaymentService.DeleteAsync(payment.Id).Result;
            Assert.IsTrue(reply, "RemovePayment Fail");
        }
        [TestMethod]
        [ExpectedException(typeof(PaymillRequestException))]
        public void DeletePaymentWithException()
        {
            bool reply = _paymill.PaymentService.DeleteAsync("pay_3af44644dd6d25c8hhhhh").Result;
            Assert.IsTrue(reply, "RemovePayment Fail");
        }
    }
}

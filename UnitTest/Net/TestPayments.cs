using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymillWrapper;
using PaymillWrapper.Service;
using System.Collections.Generic;
using PaymillWrapper.Models;
using PaymillWrapper.Exceptions;
using PaymillWrapper.Utils;

namespace UnitTest.Net
{
    [TestClass]
    public class TestPayments
    {
        Paymill _paymill = null;
        String testToken = "098f6bcd4621d373cade4e832627b4f6";
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

 /*       [TestMethod]
        public void GetPaymentsWithParameters()
        {
            Client client = _paymill.ClientService.CreateWithEmailAndDescriptionAsync("lovely-client@example.com", "Lovely Client").Result;
            Payment payment = _paymill.PaymentService.CreateWithTokenAndClientAsync(testToken, client).Result;
            Filter filter = new Filter();
            filter.Add("client", 5);
            filter.Add("offset", 41);
            List<Payment> lstPayments = _paymill.PaymentService.ListAsync(filter).Result;

            Assert.IsFalse(lstPayments.Count > 0, "GetPayments Fail");
        }*/
        [TestMethod]
        public void GetPayment()
        {
            Payment payment = _paymill.PaymentService.CreateWithTokenAsync(testToken).Result;
            Payment paymentFromService = _paymill.PaymentService.GetAsync(payment.Id).Result;

            Assert.IsFalse(String.IsNullOrEmpty(paymentFromService.Id), "Get Payment failed" );
            Assert.IsTrue(paymentFromService.CreatedAt.Date == DateTime.Now.Date, "Get Payment failed");
        }

        [TestMethod]
        public void CreatePaymentWithToken()
        {
            Payment payment = _paymill.PaymentService.CreateWithTokenAsync(testToken).Result;
            Assert.IsFalse(payment.Id == String.Empty, "CreatePayment Fail");
        }
        [TestMethod]
        public void CreatePaymentWithclient()
        {
            Client client = _paymill.ClientService.CreateWithEmailAndDescriptionAsync("lovely-client@example.com", "Lovely Client").Result;
            Assert.IsTrue(client.Id != String.Empty, "CreateClient Fail");

            Payment payment = _paymill.PaymentService.CreateWithTokenAndClientAsync(testToken, client.Id).Result;
            Assert.IsFalse(payment.Id == String.Empty, "CreatePayment With Client Fail");
            Assert.IsFalse(payment.Client != client.Id, "Client does not match");
        }

        [TestMethod]
        public void DeletePayment()
        {
            Payment payment = _paymill.PaymentService.CreateWithTokenAsync(testToken).Result;
            bool reply = _paymill.PaymentService.DeleteAsync(payment.Id).Result;
            Assert.IsTrue(reply, "RemovePayment Fail");
        }
        [TestMethod]
        public void DeletePaymentWithException()
        {
            Payment payment = _paymill.PaymentService.CreateWithTokenAsync(testToken).Result;
            bool reply = _paymill.PaymentService.DeleteAsync(payment.Id).Result;
            Assert.IsTrue(reply, "RemovePayment Fail");
        }
    }
}

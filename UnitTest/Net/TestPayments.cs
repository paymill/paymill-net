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
        PaymillContext _paymill = null;
        String testToken = "098f6bcd4621d373cade4e832627b4f6";
        [TestInitialize]
        public void Initialize()
        {
            _paymill = new PaymillContext("9a4129b37640ea5f62357922975842a1");
        }
        [TestMethod]
        public void GetPayments()
        {
            PaymillList<Payment> lstPayments = _paymill.PaymentService.ListAsync().Result;
            Assert.IsFalse(lstPayments.DataCount == 0, "GetPayments Fail");
        }


        [TestMethod]
        public void GetPayment()
        {
            Payment payment = _paymill.PaymentService.CreateWithTokenAsync(testToken).Result;
            Payment paymentFromService = _paymill.PaymentService.GetAsync(payment.Id).Result;

            Assert.IsFalse(String.IsNullOrEmpty(paymentFromService.Id), "Get Payment failed");
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
        [TestMethod]
        public void ListOrderByCreatedAt()
        {
            Payment.Order orderAsc = Payment.createOrder().ByCreatedAt().Asc();
            Payment.Order orderDesc = Payment.createOrder().ByCreatedAt().Desc();

            List<Payment> paymentsAsc = _paymill.PaymentService.ListAsync(null, orderAsc).Result.Data;
            List<Payment> paymentsDesc = _paymill.PaymentService.ListAsync(null, orderDesc).Result.Data;

            Assert.IsNotNull(paymentsAsc);
            Assert.IsNotNull(paymentsDesc);
            Assert.IsFalse(paymentsAsc.Count == 0);
            Assert.IsFalse(paymentsDesc.Count == 0);
            Assert.IsNotNull(paymentsAsc[0].Id);
            Assert.AreNotEqual(paymentsAsc[0].Id, paymentsDesc[0].Id);
        }

        [TestMethod]
        public void ListFilterByCardType()
        {
            Payment.Filter filter = Payment.createFilter().ByCardType(Payment.CardTypes.VISA);

            PaymillList<Payment> wrapper = _paymill.PaymentService.ListAsync(filter, null).Result;
            List<Payment> payments = wrapper.Data;

            Assert.IsNotNull(payments);
            Assert.IsFalse(payments.Count == 0);
            foreach (var paym in payments)
            {
                Assert.IsTrue(paym.CardType == Payment.CardTypes.VISA);
            }
        }

    }
}

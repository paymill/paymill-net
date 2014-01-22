using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymillWrapper;
using PaymillWrapper.Service;
using System.Collections.Generic;
using PaymillWrapper.Models;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using PaymillWrapper.Net;

namespace UnitTest.Net
{
    [TestClass]
    public class TestPayments
    {
        [TestInitialize]
        public void Initialize()
        {
            Paymill.ApiKey = "9a4129b37640ea5f62357922975842a1";
            Paymill.ApiUrl = "https://api.paymill.de/v2";
        }
        [TestMethod]
        public void GetPayments()
        {

            PaymentService paymentService = Paymill.GetService<PaymentService>();
            List<Payment> lstPayments = paymentService.GetPayments();
            Assert.IsFalse(lstPayments.Count == 0, "GetPayments Fail");
        }


        [TestMethod]
        public void CreatePayment()
        {
            PaymentService paymentService = Paymill.GetService<PaymentService>();
            Payment payment = paymentService.Create("098f6bcd4621d373cade4e832627b4f6");
            Assert.IsFalse(payment.Id == String.Empty, "CreatePayment Fail");
        }
        [TestMethod]
        public void CreatePaymentWithclient()
        {
            ClientService clientService = Paymill.GetService<ClientService>();
            Client client = clientService.Create("lovely-client@example.com", "Lovely Client");
            Assert.IsTrue(client.Id != String.Empty, "CreateClient Fail");

            PaymentService paymentService = Paymill.GetService<PaymentService>();
            Payment payment = paymentService.Create("098f6bcd4621d373cade4e832627b4f6", client.Id);
            Assert.IsFalse(payment.Id == String.Empty, "CreatePayment With Client Fail");
            Assert.IsFalse(payment.Client != client.Id, "Client does not match");
        }

        [TestMethod]
        public void RemovePayment()
        {
            PaymentService paymentService = Paymill.GetService<PaymentService>();
            Payment payment = paymentService.Create("098f6bcd4621d373cade4e832627b4f6");
            bool reply = paymentService.Remove(payment.Id);
            Assert.IsTrue(reply, "RemovePayment Fail");
        }
        [TestMethod]
        [ExpectedException(typeof(PaymillRequestException))]
        public void RemovePaymentWithException()
        {
            PaymentService paymentService = Paymill.GetService<PaymentService>();
            bool reply = paymentService.Remove("pay_3af44644dd6d25c8hhhhh");
            Assert.IsTrue(reply, "RemovePayment Fail");
        }
    }
}

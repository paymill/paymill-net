using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymillWrapper;
using PaymillWrapper.Service;
using System.Collections.Generic;
using PaymillWrapper.Models;

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
    }
}

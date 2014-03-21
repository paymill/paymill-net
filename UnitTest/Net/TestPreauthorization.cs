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
    public class TestPreauthorization
    {
        Paymill _paymill = null;
        String testToken = "098f6bcd4621d373cade4e832627b4f6";
        [TestInitialize]
        public void Initialize()
        {
            _paymill = new Paymill("9a4129b37640ea5f62357922975842a1");
        }
        [TestMethod]
        public void GetPreauthorizations()
        {
            PaymillList<Preauthorization> lstPreauthorizations = _paymill.PreauthorizationService.ListAsync().Result;
            Assert.IsFalse(lstPreauthorizations.DataCount == 0, "Get Preauthorization Failed");
        }
        [TestMethod]
        public void GetPreauthorizationsWithParameters()
        {

            Filter filter = new Filter();
            filter.Add("count", 1);
            filter.Add("offset", 2);
            PaymillList<Preauthorization> lstPreauthorizations = _paymill.PreauthorizationService.ListAsync(filter).Result;
            Assert.IsFalse(lstPreauthorizations.DataCount == 0, "Get Preauthorization Failed");
        }
        [TestMethod]
        public void CreatePreauthorizationWithToken()
        {
            Preauthorization newPreauthorization = _paymill.PreauthorizationService.CreateWithTokenAsync(testToken, 3500, "EUR").Result;
            Assert.IsFalse(String.IsNullOrEmpty(newPreauthorization.Id), "Create Preauthorization Failed");
            Assert.IsTrue(newPreauthorization.Amount == 3500, "Create Preauthorization Failed");
            Assert.IsTrue(newPreauthorization.Currency == "EUR", "Create Preauthorization Failed");
        }
        [TestMethod]
        public void CreatePreauthorizationWithPayment()
        {
            Payment payment = _paymill.PaymentService.CreateWithTokenAsync(testToken).Result;
            Preauthorization newPreauthorization = _paymill.PreauthorizationService.CreateWithPaymentAsync(payment, 3500, "EUR").Result;
            Assert.IsFalse(String.IsNullOrEmpty(newPreauthorization.Id), "Create Preauthorization Failed");
            Assert.IsTrue(newPreauthorization.Amount == 3500, "Create Preauthorization Failed");
            Assert.IsTrue(newPreauthorization.Currency == "EUR", "Create Preauthorization Failed");
        }
        [TestMethod]
        public void RemovePreauthorization()
        {
            Payment payment = _paymill.PaymentService.CreateWithTokenAsync(testToken).Result;
            Preauthorization newPreauthorization = _paymill.PreauthorizationService.CreateWithPaymentAsync(payment, 3500, "EUR").Result;
            Assert.IsFalse(String.IsNullOrEmpty(newPreauthorization.Id), "Create Preauthorization Failed");
            Assert.IsTrue(newPreauthorization.Amount == 3500, "Create Preauthorization Failed");
            Assert.IsTrue(newPreauthorization.Currency == "EUR", "Create Preauthorization Failed");
            Boolean result = _paymill.PreauthorizationService.DeleteAsync(newPreauthorization.Id).Result;
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void GetPreauthorization()
        {
            PaymillList<Preauthorization> lstPreauthorizations = _paymill.PreauthorizationService.ListAsync().Result;
            Preauthorization preauthorization = _paymill.PreauthorizationService.GetAsync(lstPreauthorizations.Data[0].Id).Result;
            Assert.IsFalse(String.IsNullOrEmpty(preauthorization.Id), "Create Preauthorization Failed");
  
        }
    }
}

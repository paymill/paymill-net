using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymillWrapper;
using PaymillWrapper.Service;
using System.Collections.Generic;
using PaymillWrapper.Models;

namespace UnitTest.Net
{
    [TestClass]
    public class TestPreauthorization
    {
         [TestInitialize]
        public void Initialize()
        {
            Paymill.ApiKey = "9a4129b37640ea5f62357922975842a1";
            Paymill.ApiUrl = "https://api.paymill.de/v2";
        }
        [TestMethod]
        public void GetPreauthorizations()
        {
            PreauthorizationService preauthorizationService = Paymill.GetService<PreauthorizationService>();
            List<Preauthorization> lstPreauthorizations = preauthorizationService.GetPreauthorizations();
            Assert.IsTrue(lstPreauthorizations.Count> 0 , "Get Transactions Fail");
        }
        [TestMethod]
        public void CreatePreauthorization()
        {
            PreauthorizationService preauthorizationService = Paymill.GetService<PreauthorizationService>();

            Preauthorization preauthorization = new Preauthorization();
            preauthorization.Amount = 3500;
            preauthorization.Currency = "EUR";
            PaymentService paymentService = Paymill.GetService<PaymentService>();
            Payment payment = paymentService.Create("098f6bcd4621d373cade4e832627b4f6");
            preauthorization.Payment = payment;

            Preauthorization newPreauthorization = preauthorizationService.Create(preauthorization);
            Assert.IsFalse(String.IsNullOrEmpty( newPreauthorization.Id ) , "Get Transactions Fail");
        }
    }
}

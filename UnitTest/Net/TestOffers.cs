using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymillWrapper;
using PaymillWrapper.Service;
using System.Collections.Generic;
using PaymillWrapper.Models;

namespace UnitTest.Net
{
    [TestClass]
    public class TestOffers
    {
        Paymill _paymill = null;
        [TestInitialize]
        public void Initialize()
        {
            _paymill = new Paymill("9a4129b37640ea5f62357922975842a1");
        }
        private Offer createOffer()
        {
            Offer newOffer = _paymill.OfferService.CreateAsync(1500, "EUR", "1 MONTH", "Test API", 3).Result;
            return newOffer;
        }
        [TestMethod]
        public void CreateOffer()
        {
            Offer newOffer = createOffer();
            Assert.IsTrue(newOffer.Id != String.Empty, "CreateOffer Fail");
        }
        [TestMethod]
        public void RemoveOffer()
        {
  
            Offer newOffer = createOffer();
            Assert.IsTrue(newOffer.Id != String.Empty, "CreateOffer Fail");

            Boolean result = _paymill.OfferService.DeleteAsync(newOffer.Id).Result;
            Assert.IsTrue(result, "Remove  Offer Failed");
        }
    }
}

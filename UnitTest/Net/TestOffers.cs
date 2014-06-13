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
    public class TestOffers
    {
        PaymillContext _paymill = null;
        const int offerAmount = 1500;
        [TestInitialize]
        public void Initialize()
        {

            _paymill = new PaymillContext("9a4129b37640ea5f62357922975842a1");
        }
        private Offer createOffer()
        {
            Offer newOffer = _paymill.OfferService.CreateAsync(offerAmount, "EUR", "1 MONTH", "Test API", 3).Result;
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
        [TestMethod]
        public void GetOffer()
        {
            Offer newOffer = createOffer();
            Offer offer = _paymill.OfferService.GetAsync(newOffer.Id).Result;
            Assert.IsTrue(String.Compare(offer.Id, newOffer.Id) == 0, "Get Offer failed");
        }
        [TestMethod]
        public void UpdateOffer()
        {
            Offer newOffer = createOffer();
            newOffer.Name = "Oferta 48";
            Offer updatedOffer = _paymill.OfferService.UpdateAsync(newOffer).Result;
            Assert.IsTrue(String.Compare(updatedOffer.Name, "Oferta 48") == 0, "Update offer failed");
        }
        [TestMethod]
        public void GetAllOffers()
        {
            Offer newOffer = createOffer();
            List<Offer> lstOffers = _paymill.OfferService.ListAsync().Result.Data;
            Assert.IsTrue(lstOffers.Count > 0, "List offers failed");
        }
        [TestMethod]
        public void ListOfferByAmountDesc()
        {
            Offer newOffer = createOffer();
            Offer.Order order = Offer.CreateOrder().ByCreatedAt().Desc();

            PaymillList<Offer> wrapper = _paymill.OfferService.ListAsync(null, order).Result;
            List<Offer> offers = wrapper.Data;

            Assert.IsNotNull(offers);
            Assert.IsFalse(offers.Count == 0);
            Assert.AreEqual(offers[0].Amount, offerAmount);
        }

        [TestMethod]
        public void ListFilterByAmount()
        {
            Offer newOffer = createOffer();
            Offer.Filter filter = Offer.CreateFilter().ByAmount(offerAmount);

            PaymillList<Offer> wrapper = _paymill.OfferService.ListAsync(filter, null).Result;
            List<Offer> offers = wrapper.Data;

            Assert.IsNotNull(offers);
            Assert.IsFalse(offers.Count == 0);
        }

       
    }
}

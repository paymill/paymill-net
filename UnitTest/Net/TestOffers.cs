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
        Paymill pm = null;
        [TestInitialize]
        public void Initialize()
        {
            pm = new Paymill("9a4129b37640ea5f62357922975842a1");
        }
        private Offer createOffer()
        {
            /* OfferService offerService = Paymill.GetService<OfferService>();

             Offer offer = new Offer();
             offer.Amount = 1500;
             offer.Currency = "EUR";
             offer.Interval = @"1 MONTH";
             offer.Name = "Test API";
             offer.Trial_Period_Days = 3;
             offer.Created_At = DateTime.Now;
             offer.Trial_Period_Days = 0;
             offer.Updated_At = DateTime.Now;
             offer.SubscriptionCount.Аctive = "3";
             offer.SubscriptionCount.Inactive = "0";*/
            Offer newOffer = pm.OfferService.Create(offer);
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
            OfferService offersService = Paymill.GetService<OfferService>();

            Offer newOffer = createOffer();
            Assert.IsTrue(newOffer.Id != String.Empty, "CreateOffer Fail");

            Boolean result = offersService.Remove(newOffer.Id);
            Assert.IsTrue(result, "Remove  Offer Failed");
        }
    }
}

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
        [TestInitialize]
        public void Initialize()
        {
            Paymill.ApiKey = "9a4129b37640ea5f62357922975842a1";
            Paymill.ApiUrl = "https://api.paymill.de/v2";
        }
        
        [TestMethod]
        public void CreateOffer()
        {
            OfferService offerService = Paymill.GetService<OfferService>();

            Offer offer = new Offer();
            offer.Amount = 1500;
            offer.Currency = "EUR";
            offer.Interval = @"1 MONTH";
            offer.Name = "Test API";
            offer.Created_At = DateTime.Now;
            offer.Trial_Period_Days = 12;
            offer.Updated_At = DateTime.Now;
            offer.SubscriptionCount.Аctive = "3";
            offer.SubscriptionCount.Inactive = "0";
            Offer newOffer = offerService.Create(offer);
            Assert.IsTrue(newOffer.Id != String.Empty, "CreateOffer Fail");
            var serviceOffer = offerService.Get(newOffer.Id);

            Assert.IsTrue(serviceOffer.Trial_Period_Days.HasValue
                    && serviceOffer.Trial_Period_Days.Value == offer.Trial_Period_Days.Value, "CreateOffer Fail");

        }
        [TestMethod]
        public void RemoveOffer()
        {
            OfferService offersService = Paymill.GetService<OfferService>();

            OfferService offerService = Paymill.GetService<OfferService>();

            Offer offer = new Offer();
            offer.Amount = 1500;
            offer.Currency = "EUR";
            offer.Interval = @"1 MONTH";
            offer.Name = "Test API";
            offer.Created_At = DateTime.Now;
            offer.Trial_Period_Days = 12;
            offer.Updated_At = DateTime.Now;
            offer.SubscriptionCount.Аctive = "3";
            offer.SubscriptionCount.Inactive = "0";
            Offer newOffer = offerService.Create(offer);
            Assert.IsTrue(newOffer.Id != String.Empty, "CreateOffer Fail");

            Boolean result = offersService.Remove(newOffer.Id);
            Assert.IsTrue(result, "Remove  Offer Failed");
        }
    }
}

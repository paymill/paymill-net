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
    public class TestSubscriptions
    {
        Paymill _paymill = null;
        String testToken = "098f6bcd4621d373cade4e832627b4f6";

        [TestInitialize]
        public void Initialize()
        {
            _paymill = new Paymill("9a4129b37640ea5f62357922975842a1");
        }
        [TestMethod]
        public void TestCreateWithPayment()
        {
            Offer offer = _paymill.OfferService.CreateAsync(1500, "EUR", "1 MONTH", "Test API", 3).Result;
            Payment payment = _paymill.PaymentService.CreateWithTokenAsync(testToken).Result;

            Subscription subscription = _paymill.SubscriptionService.CreateWithOfferAndPaymentAsync(offer, payment).Result;
            Assert.IsNotNull(subscription);
            Assert.IsNotNull(subscription.Client);
            Assert.IsFalse(subscription.CancelAtPeriodEnd);
        }

        [TestMethod]
        public void CreateWithPaymentAndClientWithOfferWithoutTrial()
        {
            Client client = _paymill.ClientService.CreateWithEmailAsync("zendest@example.com").Result;
            Payment payment = _paymill.PaymentService.CreateWithTokenAndClientAsync(testToken, client).Result;
            Offer offer = _paymill.OfferService.CreateAsync(2223, "EUR", "1 WEEK", "Offer No Trial").Result;

            Subscription subscriptionNoTrial = _paymill.SubscriptionService.CreateWithOfferPaymentAndClientAsync(offer, payment, client).Result;
            Assert.IsNull(subscriptionNoTrial.TrialStart);
            Assert.IsNull(subscriptionNoTrial.TrialEnd);
        }

        [TestMethod]
        public void CreateWithPaymentClientAndTrialWithOfferWithoutTrial()
        {
            Client client = _paymill.ClientService.CreateWithEmailAsync("zendest@example.com").Result;
            Payment payment = _paymill.PaymentService.CreateWithTokenAndClientAsync(testToken, client).Result;
            Offer offer = _paymill.OfferService.CreateAsync(2224, "EUR", "1 WEEK", "Offer No Trial").Result;

            long trialStart = DateTime.Now.AddDays(1).Ticks;
            Subscription subscriptionWithTrial = _paymill.SubscriptionService.CreateWithOfferPaymentAndClientAsync(offer, payment, client, new DateTime(trialStart)).Result;
            Assert.IsNotNull(subscriptionWithTrial.TrialStart);
            Assert.AreEqual(subscriptionWithTrial.TrialEnd.Value.Year, new DateTime(trialStart).Year);
            Assert.AreEqual(subscriptionWithTrial.TrialEnd.Value.Month, new DateTime(trialStart).Month);
            Assert.AreEqual(subscriptionWithTrial.TrialEnd.Value.Day, new DateTime(trialStart).Day);
            Assert.AreEqual(subscriptionWithTrial.TrialEnd.Value.Hour, new DateTime(trialStart).Hour);
            Assert.AreEqual(subscriptionWithTrial.TrialEnd.Value.Minute, new DateTime(trialStart).Minute);
        }

        [TestMethod]
        public void CreateWithPaymentAndClient_WithOfferWithTrial_shouldReturnSubscriptionWithTrialEqualsTrialInOffer()
        {
            Client client = _paymill.ClientService.CreateWithEmailAsync("zendest@example.com").Result;
            Payment payment = _paymill.PaymentService.CreateWithTokenAndClientAsync(testToken, client).Result;
            Offer offer = _paymill.OfferService.CreateAsync(2225, "EUR", "1 WEEK", "Offer With Trial", 2).Result;

            Subscription subscription = _paymill.SubscriptionService.CreateWithOfferPaymentAndClientAsync(offer, payment, client).Result;
            Assert.IsNotNull(subscription.TrialStart);
            Assert.AreEqual(subscription.TrialEnd.Value, subscription.TrialStart.Value.AddDays(2));
        }

        [TestMethod]
        public void CreateWithPaymentClientAndTrial_WithOfferWithTrial_shouldReturnSubscriptionWithTrialEqualsTrialInSubscription()
        {
            Client client = _paymill.ClientService.CreateWithEmailAsync("zendest@example.com").Result;
            Payment payment = _paymill.PaymentService.CreateWithTokenAndClientAsync(testToken, client).Result;
            Offer offer = _paymill.OfferService.CreateAsync(2224, "EUR", "1 WEEK", "Offer No Trial", 2).Result;

            long trialStart = DateTime.Now.AddDays(1).Ticks;
            Subscription subscriptionWithTrial = _paymill.SubscriptionService.CreateWithOfferPaymentAndClientAsync(offer, payment, client, new DateTime(trialStart)).Result;
            Assert.IsNotNull(subscriptionWithTrial.TrialStart);
            Assert.AreEqual(subscriptionWithTrial.TrialEnd.Value.Year, new DateTime(trialStart).Year);
            Assert.AreEqual(subscriptionWithTrial.TrialEnd.Value.Month, new DateTime(trialStart).Month);
            Assert.AreEqual(subscriptionWithTrial.TrialEnd.Value.Day, new DateTime(trialStart).Day);
            Assert.AreEqual(subscriptionWithTrial.TrialEnd.Value.Hour, new DateTime(trialStart).Hour);
            Assert.AreEqual(subscriptionWithTrial.TrialEnd.Value.Minute, new DateTime(trialStart).Minute);
        }
        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void CreateWithPaymentAndClient_shouldFail()
        {
            Client client = _paymill.ClientService.CreateWithEmailAsync("zendest@example.com").Result;
            Payment payment = _paymill.PaymentService.CreateWithTokenAndClientAsync(testToken, client.Id).Result;
            Offer offer = _paymill.OfferService.CreateAsync(900, "EUR", "1 WEEK", "Offer No Trial").Result;

            _paymill.SubscriptionService.CreateWithOfferPaymentAndClientAsync(offer, payment, null).Wait();
        }

        [TestMethod]
        public void TestUpdate()
        {

            Client client = _paymill.ClientService.CreateWithEmailAsync("zendest@example.com").Result;
            Payment payment = _paymill.PaymentService.CreateWithTokenAndClientAsync(testToken, client).Result;
            Offer offer = _paymill.OfferService.CreateAsync(2224, "EUR", "1 WEEK", "Offer No Trial", 2).Result;
            Offer offer2 = _paymill.OfferService.CreateAsync(1500, "EUR", "1 MONTH", "Test API", 3).Result;
            Subscription subscription = _paymill.SubscriptionService.CreateWithOfferPaymentAndClientAsync(offer, payment, client).Result;

            String offerId = subscription.Offer.Id;
            String subscriptionId = subscription.Id;

            subscription.CancelAtPeriodEnd = true;
            subscription.Offer = offer2;
            var updatedSubscrition = _paymill.SubscriptionService.UpdateAsync(subscription).Result;

            Assert.IsFalse(String.Equals(updatedSubscrition.Offer.Id, offerId));
            Assert.AreEqual(subscription.Offer.Id, offer2.Id);
            Assert.AreEqual(subscription.Id, subscriptionId);
            Assert.IsTrue(subscription.CancelAtPeriodEnd);
        }
    }
}

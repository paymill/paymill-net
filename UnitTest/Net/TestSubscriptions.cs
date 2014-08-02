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
        PaymillContext _paymill = null;
        String testToken = "098f6bcd4621d373cade4e832627b4f6";
        private int amount = 900;
        private Interval.Period interval;
        private String currency = "EUR";
        private Offer offer2;
        private String name = "Chuck Testa";

        [TestInitialize]
        public void Initialize()
        {
            _paymill = new PaymillContext("9a4129b37640ea5f62357922975842a1");
            interval = Interval.period(1, Interval.TypeUnit.MONTH);
            this.offer2 = _paymill.OfferService.CreateAsync(this.amount * 2, this.currency, this.interval, "Updated " + this.name).Result;
           
        }
        [TestMethod]
        public void TestCreateWithPaymentAndOffer()
        {
            Payment payment = _paymill.PaymentService.CreateWithTokenAsync(testToken).Result;
            Offer offer = _paymill.OfferService.CreateAsync(2223, "EUR", Interval.period(1, Interval.TypeUnit.WEEK), "Offer No Trial").Result;
            Subscription subscription = _paymill.SubscriptionService.CreateAsync(Subscription.Create(payment, offer)).Result;
            Assert.IsNotNull(subscription);
            Assert.IsNotNull(subscription.Client);
            Assert.AreEqual(subscription.Payment.Id, payment.Id);
            Assert.AreEqual(subscription.Offer.Id, offer.Id);
        }

        [TestMethod]
        public void TestCreateWithPaymentAndOfferComplex()
        {
            DateTime tomorrow = DateTime.Now.AddDays(1);
            Client client = _paymill.ClientService.CreateWithEmailAsync("zendest@example.com").Result;
            Payment payment = _paymill.PaymentService.CreateWithTokenAndClientAsync(testToken, client).Result;

            Offer offer = _paymill.OfferService.CreateAsync(2223, "EUR", Interval.period(1, Interval.TypeUnit.WEEK), "Offer No Trial").Result;

            Subscription subscription = _paymill.SubscriptionService.CreateAsync(Subscription.Create(payment, offer).WithClient(client)
                .WithAmount(this.amount * 5).WithCurrency("EUR").WithInterval("2 WEEK,monday").WithName("test sub").WithOffer(this.offer2)
                .WithPeriodOfValidity("1 YEAR").WithStartDate(tomorrow)).Result;

            Assert.IsNotNull(subscription);
            Assert.IsNotNull(subscription.Client);
            Assert.AreEqual(subscription.Payment.Id, payment.Id);
            Assert.AreEqual(subscription.Client.Id, client.Id);
            Assert.AreEqual(subscription.Amount, (int)(this.amount * 5));
            Assert.AreEqual(subscription.Currency, "EUR");
            Assert.AreEqual(subscription.Interval.Interval, (int)2);
            Assert.AreEqual(subscription.Interval.Unit, Interval.TypeUnit.WEEK);
            Assert.AreEqual(subscription.Interval.Weekday, Interval.Weekday.MONDAY);

            Assert.AreEqual(subscription.Name, "test sub");
            Assert.AreEqual(subscription.PeriodOfValidity.Interval, (int)1);
            Assert.AreEqual(subscription.PeriodOfValidity.Unit, Interval.TypeUnit.YEAR);
            Assert.IsTrue(subscription.NextCaptureAt.Value > DateTime.Now);
            Assert.AreEqual(subscription.Status, Subscription.SubscriptionStatus.ACTIVE);
            Assert.IsFalse(subscription.Canceled);
            Assert.IsFalse(subscription.Canceled);
            Assert.IsFalse(subscription.Livemode);

            Assert.IsNull(subscription.CanceledAt);
            Assert.IsTrue(DatesAroundSame(subscription.CreatedAt, DateTime.Now));
            Assert.IsTrue(DatesAroundSame(subscription.TrialStart.Value, DateTime.Now));
            Assert.IsTrue(DatesAroundSame(subscription.TrialEnd.Value, tomorrow));
            Assert.IsTrue(DatesAroundSame(subscription.NextCaptureAt.Value, tomorrow));
            Assert.IsNull(subscription.TempAmount);
        }
        /*
        [TestMethod]
        public void CreateWithPaymentAndClientWithOfferWithoutTrial()
        {
            Client client = _paymill.ClientService.CreateWithEmailAsync("zendest@example.com").Result;
            Payment payment = _paymill.PaymentService.CreateWithTokenAndClientAsync(testToken, client).Result;
            Offer offer = _paymill.OfferService.CreateAsync(2223, "EUR", Interval.period(1, Interval.TypeUnit.WEEK), "Offer No Trial").Result;

            Subscription subscriptionNoTrial = _paymill.SubscriptionService.CreateWithOfferPaymentAndClientAsync(offer, payment, client).Result;
            Assert.IsNull(subscriptionNoTrial.TrialStart);
            Assert.IsNull(subscriptionNoTrial.TrialEnd);
        }

        [TestMethod]
        public void CreateWithPaymentClientAndTrialWithOfferWithoutTrial()
        {
            Client client = _paymill.ClientService.CreateWithEmailAsync("zendest@example.com").Result;
            Payment payment = _paymill.PaymentService.CreateWithTokenAndClientAsync(testToken, client).Result;
            Offer offer = _paymill.OfferService.CreateAsync(2224, "EUR", Interval.period(1, Interval.TypeUnit.WEEK), "Offer No Trial").Result;

            long trialStart = DateTime.Now.AddDays(5).Ticks;
            Subscription subscriptionWithTrial = _paymill.SubscriptionService.CreateWithOfferPaymentAndClientAsync(offer, payment, client, new DateTime(trialStart)).Result;
            Assert.IsNotNull(subscriptionWithTrial.TrialStart);
            Assert.AreEqual(subscriptionWithTrial.TrialEnd.Value.Year, new DateTime(trialStart).Year);
            Assert.AreEqual(subscriptionWithTrial.TrialEnd.Value.Month, new DateTime(trialStart).Month);
            Assert.AreEqual(subscriptionWithTrial.TrialEnd.Value.Day, new DateTime(trialStart).Day);
            Assert.AreEqual(subscriptionWithTrial.TrialEnd.Value.Hour, new DateTime(trialStart).Hour);
        }

        [TestMethod]
        public void CreateWithPaymentAndClient_WithOfferWithTrial_shouldReturnSubscriptionWithTrialEqualsTrialInOffer()
        {
            Client client = _paymill.ClientService.CreateWithEmailAsync("zendest@example.com").Result;
            Payment payment = _paymill.PaymentService.CreateWithTokenAndClientAsync(testToken, client).Result;
            Offer offer = _paymill.OfferService.CreateAsync(2225, "EUR", Interval.period(1, Interval.TypeUnit.WEEK), "Offer With Trial", 2).Result;

            Subscription subscription = _paymill.SubscriptionService.CreateWithOfferPaymentAndClientAsync(offer, payment, client).Result;
            Assert.IsNotNull(subscription.TrialStart);
            Assert.AreEqual(subscription.TrialEnd.Value, subscription.TrialStart.Value.AddDays(2));
        }

        [TestMethod]
        public void CreateWithPaymentClientAndTrial_WithOfferWithTrial_shouldReturnSubscriptionWithTrialEqualsTrialInSubscription()
        {
            Client client = _paymill.ClientService.CreateWithEmailAsync("zendest@example.com").Result;
            Payment payment = _paymill.PaymentService.CreateWithTokenAndClientAsync(testToken, client).Result;
            Offer offer = _paymill.OfferService.CreateAsync(2224, "EUR", Interval.period(1, Interval.TypeUnit.WEEK), "Offer No Trial", 2).Result;

            long trialStart = DateTime.Now.AddDays(5).Ticks;
            Subscription subscriptionWithTrial = _paymill.SubscriptionService.CreateWithOfferPaymentAndClientAsync(offer, payment, client, new DateTime(trialStart)).Result;
            Assert.IsNotNull(subscriptionWithTrial.TrialStart);
            Assert.AreEqual(subscriptionWithTrial.TrialEnd.Value.Year, new DateTime(trialStart).Year);
            Assert.AreEqual(subscriptionWithTrial.TrialEnd.Value.Month, new DateTime(trialStart).Month);
            Assert.AreEqual(subscriptionWithTrial.TrialEnd.Value.Day, new DateTime(trialStart).Day);
            Assert.AreEqual(subscriptionWithTrial.TrialEnd.Value.Hour, new DateTime(trialStart).Hour);
        }
        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void CreateWithPaymentAndClient_shouldFail()
        {
            Client client = _paymill.ClientService.CreateWithEmailAsync("zendest@example.com").Result;
            Payment payment = _paymill.PaymentService.CreateWithTokenAndClientAsync(testToken, client.Id).Result;
            Offer offer = _paymill.OfferService.CreateAsync(900, "EUR", Interval.period(1, Interval.TypeUnit.WEEK), "Offer No Trial").Result;

            _paymill.SubscriptionService.CreateWithOfferPaymentAndClientAsync(offer, payment, null).Wait();
        }

        [TestMethod]
        public void TestUpdate()
        {

            Client client = _paymill.ClientService.CreateWithEmailAsync("zendest@example.com").Result;
            Payment payment = _paymill.PaymentService.CreateWithTokenAndClientAsync(testToken, client).Result;
            Offer offer = _paymill.OfferService.CreateAsync(2224, "EUR", Interval.period(1, Interval.TypeUnit.WEEK), "Offer No Trial", 2).Result;
            Offer offer2 = _paymill.OfferService.CreateAsync(1500, "EUR", Interval.period(1, Interval.TypeUnit.MONTH), "Test API", 3).Result;
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
        */
        [TestMethod]
        public void ListOrderByCreatedAt()
        {
            Subscription.Order orderDesc = Subscription.CreateOrder().ByCreatedAt().Desc();
            Subscription.Order orderAsc = Subscription.CreateOrder().ByCreatedAt().Asc();

            List<Subscription> subscriptionsDesc = _paymill.SubscriptionService.ListAsync(null, orderDesc).Result.Data;
            List<Subscription> subscriptionsAsc = _paymill.SubscriptionService.ListAsync(null, orderAsc).Result.Data;
            if (subscriptionsAsc.Count > 1
                && subscriptionsDesc.Count > 1)
            {
                Assert.AreNotEqual(subscriptionsDesc[0].Id, subscriptionsAsc[0].Id);
            }
        }

        public static Boolean DatesAroundSame(DateTime first, DateTime second, int minutes)
        {
            long timespan = minutes * 60 * 1000;
            return Math.Abs(first.Millisecond - second.Millisecond) < timespan;
        }

        public static Boolean DatesAroundSame(DateTime first, DateTime second)
        {
            return DatesAroundSame(first, second, 10);
        }

    }
}

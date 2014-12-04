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
        private Payment payment; 
        private Offer offer2;
        private Offer offer1;
        private String name = "Chuck Testa";
        private DateTime inAWeek = DateTime.Now.AddDays(7);
        private DateTime inTwoWeeks = DateTime.Now.AddDays(14);
        private DateTime inAMonth = DateTime.Now.AddMonths(1);

        [TestInitialize]
        public void Initialize()
        {
            _paymill = new PaymillContext("9a4129b37640ea5f62357922975842a1");
            interval = Interval.period(1, Interval.TypeUnit.MONTH);
            this.payment = _paymill.PaymentService.CreateWithTokenAsync(testToken).Result;
            this.offer1 = _paymill.OfferService.CreateAsync(this.amount, this.currency, this.interval, this.name).Result;
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
        [TestMethod]
        [ExpectedException(typeof(System.AggregateException))]

        public void TestCreateWithoutOfferAndAmount_ShouldFail()
        {
            Client client = _paymill.ClientService.CreateWithEmailAsync("zendest@example.com").Result;
            Payment payment = _paymill.PaymentService.CreateWithTokenAndClientAsync(testToken, client).Result;
            _paymill.SubscriptionService.CreateAsync(payment, client, null, null, null, null, null, null, null).Wait();
        }
        [TestMethod]
        public void TestCreateWithPaymentAndClient_WithOfferWithoutTrial_shouldReturnSubscriptionWithNullTrialStartAndNullTrialEnd()
        {
            Client client = _paymill.ClientService.CreateWithEmailAsync("zendest@example.com").Result;
            Payment payment = _paymill.PaymentService.CreateWithTokenAndClientAsync(testToken, client).Result;
            Offer offer = _paymill.OfferService.CreateAsync(2223, "EUR", Interval.period(1, Interval.TypeUnit.WEEK), "Offer No Trial").Result;

            Subscription subscriptionNoTrial = _paymill.SubscriptionService.CreateAsync(Subscription.Create(payment.Id, offer).WithClient(client.Id)).Result;
            Assert.IsNull(subscriptionNoTrial.TrialStart);
            Assert.IsNull(subscriptionNoTrial.TrialEnd);
        }

        [TestMethod]
        public void TestCreateWithPaymentAndClient_WithOfferWithTrial_shouldReturnSubscriptionWithTrialEqualsTrialInOffer()
        {
            Client client = _paymill.ClientService.CreateWithEmailAsync("zendest@example.com").Result;
            Payment payment = _paymill.PaymentService.CreateWithTokenAndClientAsync(testToken, client).Result;
            Offer offer = _paymill.OfferService.CreateAsync(2225, "EUR", Interval.period(1, Interval.TypeUnit.WEEK), "Offer With Trial", 2).Result;
            Subscription subscriptionWithOfferTrial = _paymill.SubscriptionService.CreateAsync(Subscription.Create(payment.Id, offer).WithClient(client.Id)).Result;

            Assert.IsNotNull(subscriptionWithOfferTrial.TrialStart);
            Assert.IsTrue(DatesAroundSame(subscriptionWithOfferTrial.TrialEnd.Value, DateTime.Now.AddDays(2)));
            Assert.IsTrue(DatesAroundSame(subscriptionWithOfferTrial.NextCaptureAt.Value, DateTime.Now.AddDays(2)));
        }

        [TestMethod]
        public void TestCreateWithPaymentClientAndStartat_WithOfferWithTrial_shouldReturnSubscriptionWithTrialEqualsTrialInSubscription()
        {
            Client client = _paymill.ClientService.CreateWithEmailAsync("zendest@example.com").Result;
            Payment payment = _paymill.PaymentService.CreateWithTokenAndClientAsync(testToken, client).Result;
            Offer offer = _paymill.OfferService.CreateAsync(2224, "EUR", Interval.period(1, Interval.TypeUnit.WEEK), "Offer No Trial", 2).Result;
            Subscription subscriptionWithOfferTrial = _paymill.SubscriptionService.CreateAsync(Subscription.Create(payment.Id, offer).WithClient(client.Id)
                .WithStartDate(DateTime.Now.AddDays(5))).Result;

            Assert.IsNotNull(subscriptionWithOfferTrial.TrialStart);
            Assert.IsTrue(DatesAroundSame(subscriptionWithOfferTrial.TrialEnd.Value, DateTime.Now.AddDays(5)));
            Assert.IsTrue(DatesAroundSame(subscriptionWithOfferTrial.NextCaptureAt.Value, DateTime.Now.AddDays(5)));

        }

        [TestMethod]
        public void TestPauseAndUnpauseSubscription()
        {
            Payment payment = _paymill.PaymentService.CreateWithTokenAsync(testToken).Result;
            Subscription subscription = _paymill.SubscriptionService.CreateAsync(Subscription.Create(payment, 1200, "EUR", "1 WEEK")).Result;
            subscription = _paymill.SubscriptionService.PauseAsync(subscription).Result;
            Assert.AreEqual(subscription.Status, Subscription.SubscriptionStatus.INACTIVE);
            subscription = _paymill.SubscriptionService.UnpauseAsync(subscription).Result;
            Assert.AreEqual(subscription.Status, Subscription.SubscriptionStatus.ACTIVE);
            Assert.IsTrue(DatesAroundSame(subscription.NextCaptureAt.Value, DateTime.Now.AddDays(1)));
        }

        [TestMethod]
        public void TestChangeSubscriptionAmountPermanently()
        {
            Payment payment = _paymill.PaymentService.CreateWithTokenAsync(testToken).Result;
            Subscription subscription = _paymill.SubscriptionService.CreateAsync(Subscription.Create(payment, 1200, "EUR", "1 WEEK")).Result;
            subscription = _paymill.SubscriptionService.ChangeAmountAsync(subscription, 2000).Result;
            Assert.AreEqual(subscription.Amount, (int)2000);
            Assert.IsNull(subscription.TempAmount);
        }

        [TestMethod]
        public void TestChangeSubscriptionAmountTemp()
        {
            Payment payment = _paymill.PaymentService.CreateWithTokenAsync(testToken).Result;
            Subscription subscription = _paymill.SubscriptionService.CreateAsync(Subscription.Create(payment, 1200, "EUR", "1 WEEK")).Result;
            subscription = _paymill.SubscriptionService.ChangeAmountTemporaryAsync(subscription, 2000).Result;
            Assert.AreEqual(subscription.Amount, (int)1200);
            Assert.AreEqual(subscription.TempAmount, (int)2000);
        }

        [TestMethod]
        public void TestChangeOfferKeepNextCaptureNoRefund()
        {
            Payment payment = _paymill.PaymentService.CreateWithTokenAsync(testToken).Result;

            Subscription subscription = _paymill.SubscriptionService.CreateAsync(Subscription.Create(payment, 1200, "EUR", "1 WEEK")).Result;
            Assert.IsTrue(DatesAroundSame(subscription.NextCaptureAt.Value, inAWeek));
            subscription = _paymill.SubscriptionService.ChangeOfferKeepCaptureDateNoRefundAsync(subscription, this.offer1).Result;
            Assert.IsTrue(DatesAroundSame(subscription.NextCaptureAt.Value, inAWeek));
        }

        [TestMethod]
        public void TestChangeOfferKeepNextCaptureAndRefund()
        {
            Payment payment = _paymill.PaymentService.CreateWithTokenAsync(testToken).Result;
            Subscription subscription = _paymill.SubscriptionService.CreateAsync(Subscription.Create(payment, 1200, "EUR", "1 WEEK")).Result;
            Assert.IsTrue(DatesAroundSame(subscription.NextCaptureAt.Value, inAWeek));
            subscription = _paymill.SubscriptionService.ChangeOfferKeepCaptureDateAndRefundAsync(subscription, this.offer1).Result;
            //TODO cannot be tested correctly as there

        }
        
          [TestMethod]
          public void TestChangeOfferChangeNextCaptureAndRefund() {
            Subscription subscription = _paymill.SubscriptionService.CreateAsync( Subscription.Create( this.payment, 1200, "EUR", "1 WEEK" ).WithStartDate( inTwoWeeks ) ).Result;
            Assert.IsTrue( DatesAroundSame( subscription.NextCaptureAt.Value, inTwoWeeks ) );
            subscription =_paymill.SubscriptionService.ChangeOfferChangeCaptureDateAndRefundAsync(subscription, this.offer1).Result;
            // when we call the above we trigger a transaction, so the nextCapture moves to offer1 interval - 1 month
            Assert.IsTrue( DatesAroundSame( subscription.NextCaptureAt.Value, inAMonth ) );
          }

          [TestMethod]
          public void TestEndTrial() {
            Subscription subscription = _paymill.SubscriptionService.CreateAsync( Subscription.Create( this.payment, 1200, "EUR", "1 WEEK" ).WithStartDate( inTwoWeeks ) ).Result;
            Assert.IsTrue( DatesAroundSame( subscription.NextCaptureAt.Value, inTwoWeeks ) );
            subscription = _paymill.SubscriptionService.EndTrialAsync( subscription ).Result;
            Assert.IsTrue( DatesAroundSame( subscription.NextCaptureAt.Value, DateTime.Now ) );
            Assert.IsNull( subscription.TrialEnd );
          }

          [TestMethod]
          public void TestChangePeriodValidity() {
            Subscription subscription = _paymill.SubscriptionService.CreateAsync( Subscription.Create( this.payment, 1200, "EUR", "2 MONTH" ).WithPeriodOfValidity( "1 YEAR" ) ).Result;
            Assert.AreEqual( subscription.PeriodOfValidity.Interval, (int) 1 );
            Assert.AreEqual( subscription.PeriodOfValidity.Unit, Interval.TypeUnit.YEAR );
            subscription = _paymill.SubscriptionService.LimitValidityAsync(subscription, "2 MONTH").Result;
            Assert.AreEqual( subscription.PeriodOfValidity.Interval, (int) 2 );
            Assert.AreEqual( subscription.PeriodOfValidity.Unit, Interval.TypeUnit.MONTH );
            subscription = _paymill.SubscriptionService.UnlimitValidityAsync(subscription).Result;
            Assert.IsNull( subscription.PeriodOfValidity );
          }

          [TestMethod]
          public void TestCancelSubscription()/* throws InterruptedException */{
            Subscription subscription = _paymill.SubscriptionService.CreateAsync( Subscription.Create( this.payment, 1200, "EUR", "1 WEEK" ).WithInterval( "1 WEEK" ) ).Result;
            subscription = _paymill.SubscriptionService.CancelAsync(subscription).Result;
            //TODO this seems to be an API bug, as the subscription is not updated immediately. we "refresh"
            Assert.AreEqual( subscription.Status.Value.ToString() , Subscription.SubscriptionStatus.INACTIVE.ToString() );
            Assert.AreEqual( subscription.Canceled, true);
            Assert.AreEqual( subscription.Deleted, false );
          }

          [TestMethod]
          public void TestDeleteSubscription(){
            Subscription subscription = _paymill.SubscriptionService.CreateAsync( Subscription.Create( this.payment, 1200, "EUR", "1 WEEK" ).WithInterval( "1 WEEK" ) ).Result;
            subscription = _paymill.SubscriptionService.DeleteAsync(subscription).Result;
            //TODO this seems to be an API bug, as the subscription is not updated immediately. we "refresh"
            Assert.AreEqual( subscription.Status.ToString(), Subscription.SubscriptionStatus.INACTIVE.ToString() );
            Assert.AreEqual( subscription.Canceled, true );
            Assert.AreEqual( subscription.Deleted, false );
          }

          [TestMethod]
          public void TestUpdateSubscription() {
            Subscription subscription = _paymill.SubscriptionService.CreateAsync( Subscription.Create( this.payment, 2000, "EUR", "1 WEEK" ).WithName( "test1" ) ).Result;
            subscription.Currency =  "USD" ;
            subscription.Interval = Interval.periodWithChargeDay( 2, Interval.TypeUnit.MONTH ) ;
            subscription.Name =  "test2" ;
            subscription.Offer = null; // Do not update Offer
            subscription = _paymill.SubscriptionService.UpdateAsync(subscription).Result;
            Assert.AreEqual( subscription.Currency, "USD" );
            Assert.AreEqual( subscription.Interval.Interval, (int) 2 );
            Assert.AreEqual( subscription.Interval.Unit, Interval.TypeUnit.MONTH );
            Assert.AreEqual( subscription.Name, "test2" );
            Assert.IsNotNull(subscription.Offer.Id);
          }
          [TestMethod]
          public void TestUpdateSubscriptionOffer()
          {
              Subscription subscription = _paymill.SubscriptionService.CreateAsync(Subscription.Create(this.payment, 2000, "EUR", "1 WEEK").WithName("test1")).Result;
              subscription.Currency = null;// Do not update Currency
              subscription.Interval = null;// Do not update Interval
              subscription.Name = null;// Do not update Name
              subscription.Offer = this.offer1; 
              subscription = _paymill.SubscriptionService.UpdateAsync(subscription).Result;
              Assert.AreEqual(subscription.Offer.Id, this.offer1.Id);
          }
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

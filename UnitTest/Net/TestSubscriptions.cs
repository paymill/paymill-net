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
            Payment payment = _paymill.PaymentService.CreateWithTokenAndClientAsync("098f6bcd4621d373cade4e832627b4f6", client).Result;
            Offer offer = _paymill.OfferService.CreateAsync(2223, "EUR", Interval.period(1, Interval.TypeUnit.WEEK), "Offer No Trial").Result;

            Subscription subscriptionNoTrial = _paymill.SubscriptionService.CreateAsync(Subscription.Create(payment.Id, offer).WithClient(client.Id)).Result;
            Assert.IsNull(subscriptionNoTrial.TrialStart);
            Assert.IsNull(subscriptionNoTrial.TrialEnd);
        }
        /*
          @Test
          public void testCreateWithPaymentAndClient_WithOfferWithTrial_shouldReturnSubscriptionWithTrialEqualsTrialInOffer() {
            Client client = clientService.createWithEmail( "zendest@example.com" );
            Payment payment = paymentService.createWithTokenAndClient( "098f6bcd4621d373cade4e832627b4f6", client );
            Offer offer = offerService.create( 2225, "EUR", Interval.period( 1, Interval.Unit.WEEK ), "Offer With Trial", 2 );
            Subscription subscriptionWithOfferTrial = subscriptionService.create( Subscription.create( payment.getId(), offer ).withClient( client.getId() ) );

            Assert.assertNotNull( subscriptionWithOfferTrial.getTrialStart() );
            Assert.assertTrue( datesAroundSame( subscriptionWithOfferTrial.getTrialEnd(), DateUtils.addDays( new Date(), 2 ) ) );
            Assert.assertTrue( datesAroundSame( subscriptionWithOfferTrial.getNextCaptureAt(), DateUtils.addDays( new Date(), 2 ) ) );

            this.subscriptions.add( subscriptionWithOfferTrial );
          }

          @Test
          public void testCreateWithPaymentClientAndStartat_WithOfferWithTrial_shouldReturnSubscriptionWithTrialEqualsTrialInSubscription() {
            Client client = clientService.createWithEmail( "zendest@example.com" );
            Payment payment = paymentService.createWithTokenAndClient( "098f6bcd4621d373cade4e832627b4f6", client );
            Offer offer = offerService.create( 2224, "EUR", Interval.period( 1, Interval.Unit.WEEK ), "Offer No Trial", 2 );
            Subscription subscriptionWithOfferTrial = subscriptionService.create( Subscription.create( payment.getId(), offer ).withClient( client.getId() )
                .withStartDate( DateUtils.addDays( new Date(), 5 ) ) );

            Assert.assertNotNull( subscriptionWithOfferTrial.getTrialStart() );
            Assert.assertTrue( datesAroundSame( subscriptionWithOfferTrial.getTrialEnd(), DateUtils.addDays( new Date(), 5 ) ) );
            Assert.assertTrue( datesAroundSame( subscriptionWithOfferTrial.getNextCaptureAt(), DateUtils.addDays( new Date(), 5 ) ) );

            this.subscriptions.add( subscriptionWithOfferTrial );
          }

          @Test
          public void testPauseAndUnpauseSubscription() {
            Subscription subscription = subscriptionService.create( Subscription.create( this.payment, 1200, "EUR", "1 WEEK" ) );
            subscriptionService.pause( subscription );
            Assert.assertEquals( subscription.getStatus(), Subscription.Status.INACTIVE );
            subscriptionService.unpause( subscription );
            Assert.assertEquals( subscription.getStatus(), Subscription.Status.ACTIVE );
            Assert.assertTrue( datesAroundSame( subscription.getNextCaptureAt(), DateUtils.addWeeks( new Date(), 1 ) ) );
            this.subscriptions.add( subscription );
          }

          @Test
          public void testChangeSubscriptionAmountPermanently() {
            Subscription subscription = subscriptionService.create( Subscription.create( this.payment, 1200, "EUR", "1 WEEK" ) );
            subscriptionService.changeAmount( subscription, 2000 );
            Assert.assertEquals( subscription.getAmount(), (Integer) 2000 );
            Assert.assertNull( subscription.getTempAmount() );
            this.subscriptions.add( subscription );
          }

          @Test
          public void testChangeSubscriptionAmountTemp() {
            Subscription subscription = subscriptionService.create( Subscription.create( this.payment, 1200, "EUR", "1 WEEK" ) );
            subscriptionService.changeAmountTemporary( subscription, 2000 );
            Assert.assertEquals( subscription.getAmount(), (Integer) 1200 );
            Assert.assertEquals( subscription.getTempAmount(), (Integer) 2000 );
            this.subscriptions.add( subscription );
          }

          @Test
          public void testChangeOfferKeepNextCaptureNoRefund() {
            Subscription subscription = subscriptionService.create( Subscription.create( this.payment, 1200, "EUR", "1 WEEK" ) );
            Assert.assertTrue( datesAroundSame( subscription.getNextCaptureAt(), inAWeek ) );
            subscriptionService.changeOfferKeepCaptureDateNoRefund( subscription, this.offer1 );
            Assert.assertTrue( datesAroundSame( subscription.getNextCaptureAt(), inAWeek ) );
            this.subscriptions.add( subscription );
          }

          @Test
          public void testChangeOfferKeepNextCaptureAndRefund() {
            Subscription subscription = subscriptionService.create( Subscription.create( this.payment, 1200, "EUR", "1 WEEK" ) );
            Assert.assertTrue( datesAroundSame( subscription.getNextCaptureAt(), inAWeek ) );
            subscriptionService.changeOfferKeepCaptureDateAndRefund( subscription, this.offer1 );
            //TODO cannot be tested correctly as there
            //Assert.assertTrue( datesAroundSame( subscription.getNextCaptureAt(), inAWeek ) );
            this.subscriptions.add( subscription );
          }

          @Test
          public void testChangeOfferChangeNextCaptureAndRefund() {
            Subscription subscription = subscriptionService.create( Subscription.create( this.payment, 1200, "EUR", "1 WEEK" ).withStartDate( inTwoWeeks ) );
            Assert.assertTrue( datesAroundSame( subscription.getNextCaptureAt(), inTwoWeeks ) );
            subscriptionService.changeOfferChangeCaptureDateAndRefund( subscription, this.offer1 );
            // when we call the above we trigger a transaction, so the nextCapture moves to offer1 interval - 1 month
            Assert.assertTrue( datesAroundSame( subscription.getNextCaptureAt(), inAMonth ) );
            this.subscriptions.add( subscription );
          }

          @Test
          public void testEndTrial() {
            Subscription subscription = subscriptionService.create( Subscription.create( this.payment, 1200, "EUR", "1 WEEK" ).withStartDate( inTwoWeeks ) );
            Assert.assertTrue( datesAroundSame( subscription.getNextCaptureAt(), inTwoWeeks ) );
            subscriptionService.endTrial( subscription );
            Assert.assertTrue( datesAroundSame( subscription.getNextCaptureAt(), new Date() ) );
            Assert.assertNull( subscription.getTrialEnd() );
            this.subscriptions.add( subscription );
          }

          @Test
          public void testChangePeriodValidity() {
            Subscription subscription = subscriptionService.create( Subscription.create( this.payment, 1200, "EUR", "2 MONTH" ).withPeriodOfValidity( "1 YEAR" ) );
            Assert.assertEquals( subscription.getPeriodOfValidity().getInterval(), (Integer) 1 );
            Assert.assertEquals( subscription.getPeriodOfValidity().getUnit(), Interval.Unit.YEAR );
            subscriptionService.limitValidity( subscription, "2 MONTH" );
            Assert.assertEquals( subscription.getPeriodOfValidity().getInterval(), (Integer) 2 );
            Assert.assertEquals( subscription.getPeriodOfValidity().getUnit(), Interval.Unit.MONTH );
            subscriptionService.unlimitValidity( subscription );
            Assert.assertNull( subscription.getPeriodOfValidity() );
            this.subscriptions.add( subscription );
          }

          @Test
          public void testCancelSubscription() throws InterruptedException {
            Subscription subscription = subscriptionService.create( Subscription.create( this.payment, 1200, "EUR", "1 WEEK" ).withInterval( "1 WEEK" ) );
            subscriptionService.cancel( subscription );
            //TODO this seems to be an API bug, as the subscription is not updated immediately. we "refresh"
            Assert.assertEquals( subscription.getStatus(), Subscription.Status.INACTIVE );
            Assert.assertEquals( subscription.getCanceled(), Boolean.TRUE );
            Assert.assertEquals( subscription.getDeleted(), Boolean.FALSE );
            this.subscriptions.add( subscription );
          }

          @Test
          public void testDeleteSubscription() throws InterruptedException {
            Subscription subscription = subscriptionService.create( Subscription.create( this.payment, 1200, "EUR", "1 WEEK" ).withInterval( "1 WEEK" ) );
            subscriptionService.delete( subscription );
            //TODO this seems to be an API bug, as the subscription is not updated immediately. we "refresh"
            Assert.assertEquals( subscription.getStatus(), Subscription.Status.INACTIVE );
            Assert.assertEquals( subscription.getCanceled(), Boolean.TRUE );
            Assert.assertEquals( subscription.getDeleted(), Boolean.TRUE );
          }

          @Test
          public void testUpdateSubscription() {
            Subscription subscription = subscriptionService.create( Subscription.create( this.payment, 2000, "EUR", "1 WEEK" ).withName( "test1" ) );
            subscription.setCurrency( "USD" );
            subscription.setInterval( Interval.periodWithChargeDay( 2, Interval.Unit.MONTH ) );
            subscription.setName( "test2" );
            subscriptionService.update( subscription );
            Assert.assertEquals( subscription.getCurrency(), "USD" );
            Assert.assertEquals( subscription.getInterval().getInterval(), (Integer) 2 );
            Assert.assertEquals( subscription.getInterval().getUnit(), Interval.Unit.MONTH );
            Assert.assertEquals( subscription.getName(), "test2" );
          }

          // TODO[VNi]: There is an API error: No sorting by offer.
          //@Test( )
          public void testListOrderByOffer() {

            subscriptionService.create( Subscription.create( payment, offer4 ) );
            subscriptionService.create( Subscription.create( payment, offer5 ) );

            Subscription.Order orderDesc = Subscription.createOrder().byOffer().desc();
            Subscription.Order orderAsc = Subscription.createOrder().byOffer().asc();

            List<Subscription> subscriptionsDesc = this.subscriptionService.list( null, orderDesc ).getData();

            List<Subscription> subscriptionsAsc = this.subscriptionService.list( null, orderAsc ).getData();

            Assert.assertNotEquals( subscriptionsDesc.get( 0 ).getOffer().getId(), subscriptionsAsc.get( 0 ).getOffer().getId() );
          }

          // @Test( dependsOnMethods = "testCreateWithPaymentAndOfferComplex" )
          public void testListOrderByCreatedAt() {
            Subscription.Order orderDesc = Subscription.createOrder().byCreatedAt().desc();
            Subscription.Order orderAsc = Subscription.createOrder().byCreatedAt().asc();

            List<Subscription> subscriptionsDesc = this.subscriptionService.list( null, orderDesc, 100000, 0 ).getData();
            for( Subscription subscription : subscriptionsDesc ) {
              if( subscription.getOffer() == null )
                this.subscriptionService.get( subscription );
            }

            List<Subscription> subscriptionsAsc = this.subscriptionService.list( null, orderAsc, 100000, 0 ).getData();

            Assert.assertEquals( subscriptionsDesc.get( 0 ).getId(), subscriptionsAsc.get( subscriptionsAsc.size() - 1 ).getId() );
            Assert.assertEquals( subscriptionsDesc.get( subscriptionsDesc.size() - 1 ).getId(), subscriptionsAsc.get( 0 ).getId() );
          }*/
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

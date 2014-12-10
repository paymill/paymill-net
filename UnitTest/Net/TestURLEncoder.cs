using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymillWrapper.Utils;
using PaymillWrapper.Models;
using PaymillWrapper;

namespace UnitTest.Net
{
    /// <summary>
    /// Descripción resumida de TestURLEncoder
    /// </summary>
    [TestClass]
    public class TestURLEncoder
    {
        public TestURLEncoder()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }

        private TestContext testContextInstance;

        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }


        [TestMethod]
        public void EncodeTransactionUpdate()
        {
            UrlEncoder urlEncoder = new UrlEncoder();
            Transaction transaction = new Transaction();
            transaction.Token = "098f6bcd4621d373cade4e832627b4f6";
            transaction.Amount = 3500;
            transaction.Currency = "EUR";
            transaction.Description = "Test";
            string expected = "description=Test";
            string reply = urlEncoder.EncodeUpdate(transaction);
            Assert.AreEqual(expected, reply);
        }
        [TestMethod]
        public void EncodeSubscriptionUpdate()
        {
            UrlEncoder urlEncoder = new UrlEncoder();
            Subscription subscription = new Subscription();
            subscription.Id = "sub_569df922b4506cd73030";
            subscription.Offer = new Offer() { Id = "sub_569df922b4506cd73030" };
            string expected = "offer=sub_569df922b4506cd73030";
            string reply = urlEncoder.EncodeUpdate(subscription);
            Assert.AreEqual(expected, reply);
        }
        [TestMethod]
        public void EncodeNullableObject()
        {
            UrlEncoder urlEncoder = new UrlEncoder();
            DateTime? trialEnd = null;
            String encodedObject = urlEncoder.EncodeObject(new
                {
                    Offer = "OfferId",
                    Payment = "PaymentId",
                    Client = "clientId",
                    Start_At = "1395266400",
                    End_At = trialEnd
                });

            Assert.IsNotNull(encodedObject);
            Assert.AreEqual(encodedObject, "offer=OfferId&payment=PaymentId&client=clientId&start_at=1395266400");
        }
        [TestMethod]
        public void EncodeNullableIntObject()
        {
            UrlEncoder urlEncoder = new UrlEncoder();
            int? trialPeriodDays = 100;
            int? trialPeriodMinutes = null;
            String encodedObject = urlEncoder.EncodeObject(new
            {
                Amount = "amount",
                Currency = "currency",
                Interval = "interval",
                Name = "name",
                Trial_Period_Days = trialPeriodDays,
                Trial_Period_Minutes = trialPeriodMinutes
            });

            Assert.IsNotNull(encodedObject);
            Assert.AreEqual(encodedObject, "amount=amount&currency=currency&interval=interval&name=name&trial_period_days=100");
        }
        [TestMethod]
        public void EncodeClientFilter()
        {
            UrlEncoder urlEncoder = new UrlEncoder();
            Client.Filter filter = Client.CreateFilter();
            filter.ByEmail("john.rambo@qaiware.com");
            filter.ByPayment("_pay12345678");
            filter.BySubscriptionId("_subs12345678");
            filter.ByOfferId("_offery12345678");
            filter.ByCreatedAt(unixEpoch.AddSeconds(1340199740), unixEpoch.AddSeconds(1340199741));
            filter.ByUpdatedAt(unixEpoch.AddSeconds(1385145851), unixEpoch.AddSeconds(1385145851));

            String encodedObject = urlEncoder.EncodeFilterParameters(filter, null, 20, 9);

            Assert.IsNotNull(encodedObject);
            Assert.AreEqual(encodedObject, "payment=_pay12345678&subscription=_subs12345678&offer=_offery12345678&email=john.rambo%40qaiware.com&created_at=1340199740-1340199741&updated_at=1385145851-1385145851&count=20&offset=9");
        }
        [TestMethod]
        public void EncodeClientOrder()
        {
            UrlEncoder urlEncoder = new UrlEncoder();
            Client.Order order = Client.CreateOrder();
            order.ByCreatedAt().Asc();
            String encodedObject = urlEncoder.EncodeFilterParameters(null, order, 20, 9);
            Assert.IsNotNull(encodedObject);
            Assert.AreEqual(encodedObject, "order=created_at_asc&count=20&offset=9");
        }
        private static readonly DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).ToLocalTime();
 
    }
}

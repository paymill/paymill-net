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
            subscription.CancelAtPeriodEnd = true;
            subscription.Id = "sub_569df922b4506cd73030";
            string expected = "cancel_at_period_end=true";
            string reply = urlEncoder.EncodeUpdate(subscription);

            Assert.AreEqual(expected, reply);
        }
        [TestMethod]
        public void EncodeNullableObject()
        {
            UrlEncoder urlEncoder = new UrlEncoder();
            DateTime? trialStart = new DateTime(2014, 3, 20);
            DateTime? trialEnd = null;
            String encodedObject = urlEncoder.EncodeObject( new
                {
                    Offer = "OfferId",
                    Payment = "PaymentId",
                    Client = "clientId",
                    Start_At = trialStart,
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
    }
}

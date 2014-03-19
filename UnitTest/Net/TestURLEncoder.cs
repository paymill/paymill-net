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
        public void EncodeTransaction()
        {
            UrlEncoder urlEncoder = new UrlEncoder();
            Transaction transaction = new Transaction();
            transaction.Token = "098f6bcd4621d373cade4e832627b4f6";
            transaction.Amount = 3500;
            transaction.Currency = "EUR";
            transaction.Description = "Test";

            string expected = "amount=3500&currency=EUR&source=paymill-net-0.1.1.2&token=098f6bcd4621d373cade4e832627b4f6&description=Test";
            string reply = urlEncoder.EncodeObject(transaction);

            Assert.AreEqual(expected, reply);
        }

        [TestMethod]
        public void EncodePreauthorization()
        {
            UrlEncoder urlEncoder = new UrlEncoder();
            Preauthorization preauthorization = new Preauthorization();
            preauthorization.Amount = 3500;
            preauthorization.Currency = "EUR";
            preauthorization.Payment = new Payment() { Id = "pay_4c159fe95d3be503778a" };

            string expected = "amount=3500&currency=EUR&source=paymill-net-0.1.1.2&payment=pay_4c159fe95d3be503778a";
            string reply = urlEncoder.EncodeObject(preauthorization);

            Assert.AreEqual(expected, reply);
        }

        [TestMethod]
        public void EncodeRefund()
        {
            UrlEncoder urlEncoder = new UrlEncoder();

            Refund refund = new Refund();
            refund.Amount = 500;
            refund.Description = "Test";
            refund.Transaction = new Transaction() { Id = "tran_a7c93a1e5b431b52c0f0" };

            string expected = "amount=500&description=Test";
            string reply = urlEncoder.EncodeObject(refund);

            Assert.AreEqual(expected, reply);
        }
        [TestMethod]
        public void EncodeSubscriptionAdd()
        {
            UrlEncoder urlEncoder = new UrlEncoder();

            Subscription subscription = new Subscription();
            subscription.Client = new Client("client_bbe895116de80b6141fd");
            subscription.Offer = new Offer("offer_32008ddd39954e71ed48");
            subscription.Payment = new Payment("pay_81ec02206e9b9c587513");

            string expected = "client=client_bbe895116de80b6141fd&offer=offer_32008ddd39954e71ed48&payment=pay_81ec02206e9b9c587513";
            string reply = urlEncoder.EncodeObject(subscription);

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
            string reply = urlEncoder.EncodeObject(subscription);

            Assert.AreEqual(expected, reply);
        }

    }
}

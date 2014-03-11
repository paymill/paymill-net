using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymillWrapper.Net;
using PaymillWrapper.Models;

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

        /// <summary>
        ///Obtiene o establece el contexto de las Tests que proporciona
        ///información y funcionalidad para la ejecución de Tests actual.
        ///</summary>
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
        public void EncodeUpdateClient()
        {
            UrlEncoder urlEncoder = new UrlEncoder();

            Client client = new Client();
            client.Email = "max@musterman.com";
            client.Description = "description1";
            string reply = urlEncoder.EncodeUpdate(client);
            Assert.AreEqual("email=max%40musterman.com&description=description1", reply);
        }
        [TestMethod]
        public void EncodeUpdateOffer()
        {
            UrlEncoder urlEncoder = new UrlEncoder();

            Offer offer = new Offer ();
            offer.Name = "TestOffer";
            offer.Amount = 100;
            offer.Interval = new Interval("10 Day");
            offer.Currency = "USD";
            string reply = urlEncoder.EncodeUpdate(offer);
            Assert.AreEqual("name=TestOffer", reply);
        }
        [TestMethod]
        public void EncodeUpdateSubscription()
        {
            UrlEncoder urlEncoder = new UrlEncoder();
            Subscription subs = new Subscription();
            subs.CreatedAt = DateTime.Now;
            subs.Payment = new Payment() { Id = "pay_1234", Code = "Test Code" };
            subs.Offer = new Offer() { Id = "offer_111", Name = "Test Offer" };
            subs.TrialEnd = DateTime.Now.AddDays(-1);
            subs.Client = new Client("client_id");
            subs.CancelAtPeriodEnd = true;
            string reply = urlEncoder.EncodeUpdate(subs);
            Assert.AreEqual("offer=offer_111&cancel_at_period_end=True&payment=pay_1234", reply);
        }
        [TestMethod]
        public void EncodeTransaction()
        {
            UrlEncoder urlEncoder = new UrlEncoder();
            Transaction trans = new Transaction();
            trans.Status =Transaction.TypeStatus.Closed;
            trans.Payment = new Payment() { Id = "pay_1234", Code = "Test Code" };
            trans.Token = "token";
            trans.Description = "description";
            string reply = urlEncoder.EncodeUpdate(trans);
            Assert.AreEqual("description=description", reply);
        }
        [TestMethod]
        public void EncodeWebhook()
        {
            UrlEncoder urlEncoder = new UrlEncoder();
            Webhook hook = new Webhook();
            hook.Id = "id_12345";
            hook.livemode = true;
            hook.Url = new Uri(@"http://www.url.com");
            hook.Email = "email";
            string reply = urlEncoder.EncodeUpdate(hook);
            Assert.AreEqual("url=http%3a%2f%2fwww.url.com%2f&email=email", reply);
        }
    }
}

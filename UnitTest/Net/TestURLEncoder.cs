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
        ///Obtiene o establece el contexto de las pruebas que proporciona
        ///información y funcionalidad para la ejecución de pruebas actual.
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

        #region Atributos de prueba adicionales
        //
        // Puede usar los siguientes atributos adicionales conforme escribe las pruebas:
        //
        // Use ClassInitialize para ejecutar el código antes de ejecutar la primera prueba en la clase
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup para ejecutar el código una vez ejecutadas todas las pruebas en una clase
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Usar TestInitialize para ejecutar el código antes de ejecutar cada prueba 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup para ejecutar el código una vez ejecutadas todas las pruebas
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void EncodeTransaction()
        {
            URLEncoder urlEncoder = new URLEncoder();

            Transaction transaction = new Transaction();
            transaction.Token = "098f6bcd4621d373cade4e832627b4f6";
            transaction.Amount = 3500;
            transaction.Currency = "EUR";
            transaction.Description = "Prueba";

            string expected = "amount=3500&currency=EUR&token=098f6bcd4621d373cade4e832627b4f6&description=Prueba";
            string reply = urlEncoder.EncodeTransaction(transaction);

            Assert.AreEqual(expected, reply);
        }

        [TestMethod]
        public void EncodePreauthorization()
        {
            URLEncoder urlEncoder = new URLEncoder();

            Preauthorization preauthorization = new Preauthorization();
            preauthorization.Amount = 3500;
            preauthorization.Currency = "EUR";
            preauthorization.Payment = new Payment() { Id = "pay_4c159fe95d3be503778a" };

            string expected = "amount=3500&currency=EUR&payment=pay_4c159fe95d3be503778a";
            string reply = urlEncoder.EncodePreauthorization(preauthorization);

            Assert.AreEqual(expected, reply);
        }

        [TestMethod]
        public void EncodeRefund()
        {
            URLEncoder urlEncoder = new URLEncoder();

            Refund refund = new Refund();
            refund.Amount = 500;
            refund.Description = "Prueba";
            refund.Transaction = new Transaction() { Id = "tran_a7c93a1e5b431b52c0f0" };

            string expected = "amount=500&description=Prueba";
            string reply = urlEncoder.EncodeRefund(refund);

            Assert.AreEqual(expected, reply);
        }

        [TestMethod]
        public void EncodeOfferAdd()
        {
            URLEncoder urlEncoder = new URLEncoder();

            Offer offer = new Offer();
            offer.Amount = 1500;
            offer.Currency = "eur";
            offer.Interval = "MONTH";
            offer.Name = "Prueba API";
            offer.Trial_Period_Days = 3;

            string expected = "amount=1500&currency=eur&interval=month&name=Prueba+API";
            string reply = urlEncoder.EncodeOfferAdd(offer);

            Assert.AreEqual(expected, reply);
        }

        [TestMethod]
        public void EncodeOfferUpdate()
        {
            URLEncoder urlEncoder = new URLEncoder();

            Offer offer = new Offer();
            offer.Name = "Oferta 48";
            offer.Id = "offer_6eea405f83d4d3098604";

            string expected = "amount=0&interval=week&name=Oferta+48";
            string reply = urlEncoder.EncodeOfferAdd(offer);

            Assert.AreEqual(expected, reply);
        }

        [TestMethod]
        public void EncodeSubscriptionAdd()
        {
            URLEncoder urlEncoder = new URLEncoder();

            Subscription subscription = new Subscription();
            subscription.Client = new Client() { Id = "client_bbe895116de80b6141fd" };
            subscription.Offer = new Offer() { Id = "offer_32008ddd39954e71ed48" };
            subscription.Payment = new Payment() { Id = "pay_81ec02206e9b9c587513" };

            string expected = "client=client_bbe895116de80b6141fd&offer=offer_32008ddd39954e71ed48&payment=pay_81ec02206e9b9c587513";
            string reply = urlEncoder.EncodeSubscriptionAdd(subscription);

            Assert.AreEqual(expected, reply);
        }

        [TestMethod]
        public void EncodeSubscriptionUpdate()
        {
            URLEncoder urlEncoder = new URLEncoder();

            Subscription subscription = new Subscription();
            subscription.Cancel_At_Period_End = true;
            subscription.Id = "sub_569df922b4506cd73030";

            string expected = "cancel_at_period_end=True";
            string reply = urlEncoder.EncodeSubscriptionUpdate(subscription);

            Assert.AreEqual(expected, reply);
        }

        [TestMethod]
        public void EncodeClientAdd()
        {
            URLEncoder urlEncoder = new URLEncoder();

            Client c = new Client();
            c.Description = "Prueba";
            c.Email = "javicantos22@hotmail.es";

            string expected = "email=javicantos22%40hotmail.es&description=Prueba";
            string reply = urlEncoder.Encode<Client>(c);

            Assert.AreEqual(expected, reply);
        }

        [TestMethod]
        public void EncodeClientUpdate()
        {
            URLEncoder urlEncoder = new URLEncoder();

            Client c = new Client();
            c.Description = "Javier";
            c.Email = "javicantos33@hotmail.es";
            c.Id = "client_bbe895116de80b6141fd";

            string expected = "email=javicantos33%40hotmail.es&description=Javier&id=client_bbe895116de80b6141fd";
            string reply = urlEncoder.Encode<Client>(c);

            Assert.AreEqual(expected, reply);
        }
    }
}

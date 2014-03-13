using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymillWrapper.Net;
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

        #region Atributos de Test adicionales
        //
        // Puede usar los siguientes atributos adicionales conforme escribe las Tests:
        //
        // Use ClassInitialize para ejecutar el código antes de ejecutar la primera Test en la clase
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup para ejecutar el código una vez ejecutadas todas las Tests en una clase
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Usar TestInitialize para ejecutar el código antes de ejecutar cada Test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup para ejecutar el código una vez ejecutadas todas las Tests
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
            transaction.Description = "Test";

            string expected = "amount=3500&currency=EUR&source=paymill-net-0.1.1.2&token=098f6bcd4621d373cade4e832627b4f6&description=Test";
            string reply = urlEncoder.EncodeTransaction(transaction, null);

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

            string expected = "amount=3500&currency=EUR&source=paymill-net-0.1.1.2&payment=pay_4c159fe95d3be503778a";
            string reply = urlEncoder.EncodePreauthorization(preauthorization);

            Assert.AreEqual(expected, reply);
        }

        [TestMethod]
        public void EncodeRefund()
        {
            URLEncoder urlEncoder = new URLEncoder();

            Refund refund = new Refund();
            refund.Amount = 500;
            refund.Description = "Test";
            refund.Transaction = new Transaction() { Id = "tran_a7c93a1e5b431b52c0f0" };

            string expected = "amount=500&description=Test";
            string reply = urlEncoder.EncodeRefund(refund);

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
            string expected = "cancel_at_period_end=true";
            string reply = urlEncoder.EncodeSubscriptionUpdate(subscription);

            Assert.AreEqual(expected, reply);
        }

    }
}

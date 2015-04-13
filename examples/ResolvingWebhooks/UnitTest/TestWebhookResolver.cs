using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ResolvingWebhooks;
using System.Reflection;
using System.IO;
using PaymillWrapper;
using PaymillWrapper.Models;
namespace UnitTest
{
    [TestClass]
    public class TestWebhookResolver
    {
        private static string GetInputFile(string filename)
        {
            var thisAssembly = Assembly.GetExecutingAssembly();
            const string path = "UnitTest.Models";
            var stream = thisAssembly.GetManifestResourceStream(path + "." + filename);
            var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }
        [TestMethod]
        public void TestResolveSubscriptionSucceeded_shouldSecceed()
        {
            String content = GetInputFile("subscription.succeeded.json");
            WebhookResolver resolver = WebhookResolver.FromString(content);
            Assert.AreEqual(resolver.EventType, Webhook.WebhookEventType.SUBSCRIPTION_SUCCEEDED);
            Assert.AreEqual(resolver.Transaction.Id, "tran_59c55c0a7e7d6597f08d55e5981a");
            Assert.AreEqual(resolver.Subscription.Id, "sub_1dfa3dc813004a11c9d6");
            Assert.IsNull(resolver.Refund);
            Assert.IsNull(resolver.Client);
        }
        [TestMethod]
        public void TestResolveTransactionCreated_shouldSecceed()
        {
            String content = GetInputFile("transaction.created.json");
            WebhookResolver resolver = WebhookResolver.FromString(content);
            Assert.AreEqual(resolver.EventType, Webhook.WebhookEventType.TRANSACTION_CREATED);
            Assert.AreEqual(resolver.Transaction.Id, "tran_656a13cb793ac165f35556f16ce5");
            Assert.IsNull(resolver.Subscription);
            Assert.IsNull(resolver.Refund);
            Assert.IsNull(resolver.Client);
            Assert.AreEqual(resolver.AppId, "app_1234");
        }

        [TestMethod]
        public void TestResolveInvoiceAvailable_shouldSecceed()
        {
            String content = GetInputFile("invoice.available.json");
            WebhookResolver resolver = WebhookResolver.FromString(content);
            Assert.AreEqual(resolver.EventType, Webhook.WebhookEventType.INVOICE_AVAILABLE);
            Assert.AreEqual(resolver.Invoice.InvoiceNumber, "1293724");
            Assert.AreEqual(resolver.Invoice.Netto, (int)12399);
            Assert.AreEqual(resolver.Invoice.Brutto, (int)14755);
            Assert.AreEqual(resolver.Invoice.Status, "sent");
            Assert.AreEqual(resolver.Invoice.VatRate, (int)19);
            Assert.IsNull(resolver.Subscription);
            Assert.IsNull(resolver.Refund);
            Assert.IsNull(resolver.Client);
        }

        [TestMethod]
        public void TestResolveAppMerchantActivated_shouldSecceed()
        {
            String content = GetInputFile("app.merchant.activated.json");
            WebhookResolver resolver = WebhookResolver.FromString(content);
            Assert.AreEqual(resolver.EventType, Webhook.WebhookEventType.APP_MERCHANT_ACTIVATED);
            Assert.AreEqual(resolver.Merchant.Identifier, "mer_123456789");
            Assert.AreEqual(resolver.Merchant.Email, "mail@example.com");
            Assert.AreEqual(resolver.Merchant.Locale, "de_DE");
            Assert.AreEqual(resolver.Merchant.Country, "DEU");
            Assert.AreEqual(resolver.Merchant.Methods[0], "visa");
            Assert.AreEqual(resolver.Merchant.Methods[1], "mastercard");
            Assert.IsNull(resolver.Subscription);
            Assert.IsNull(resolver.Refund);
            Assert.IsNull(resolver.Client);
        }
    }
}

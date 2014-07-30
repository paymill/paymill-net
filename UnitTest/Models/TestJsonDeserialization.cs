using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymillWrapper.Models;
using PaymillWrapper.Utils;
using Newtonsoft.Json;

namespace UnitTest.Models
{
    [TestClass]
    public class TestJsonDeserialization
    {
        private static string GetInputFile(string filename)
        {
            var thisAssembly = Assembly.GetExecutingAssembly();
            const string path = "UnitTest.Models.Input";
            var stream = thisAssembly.GetManifestResourceStream(path + "." + filename);
            var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }

        private static T ReadResult<T>(string filename)
        {
            String data = GetInputFile(filename);
            return PaymillWrapper.Service.AbstractService<T>.ReadResult<T>(data);
        }

        private static PaymillList<T> ReadResults<T>(string filename)
        {
            String data = GetInputFile(filename);
            return PaymillWrapper.Service.AbstractService<T>.ReadResults<T>(data);
 
        }
        [TestMethod]
        public void TestClients()
        {
            var clients = ReadResults<Client>("clients.json").Data;
            int hotmailAcounts = clients.Count(x=> x.Email == "javicantos22@hotmail.es");
            Assert.IsTrue(hotmailAcounts == clients.Count(), "Invalid clients count");
        }
        [TestMethod]
        public void TestWebhookUrl()
        {
            var webhook = ReadResult<Webhook>("webhook_url.json");
            Assert.AreEqual("hook_40237e20a7d5a231d99b", webhook.Id);
            Assert.AreEqual("http://lovely-client.com/", webhook.Url.AbsoluteUri);
            Assert.AreEqual(false, webhook.livemode);
            Assert.AreEqual(1358982000, webhook.CreatedAt.ToUnixTimestamp());
            Assert.AreEqual(1358982000, webhook.UpdatedAt.ToUnixTimestamp());
            Assert.AreEqual(2, webhook.EventTypes.Count());
            Assert.AreEqual(Webhook.WebhookEventType.TRANSACTION_SUCCEEDED, webhook.EventTypes[0]);
            Assert.AreEqual(Webhook.WebhookEventType.TRANSACTION_FAILED, webhook.EventTypes[1]);
        }
        [TestMethod]
        public void TestWebhookEmail()
        {
            var webhook = ReadResult<Webhook>("webhook_email.json");
            Assert.AreEqual("hook_40237e20a7d5a231d99b", webhook.Id);
            Assert.AreEqual("lovely-client@example.com", webhook.Email);
            Assert.AreEqual(false, webhook.livemode);
            Assert.AreEqual(1358982000, webhook.CreatedAt.ToUnixTimestamp());
            Assert.AreEqual(1358982000, webhook.UpdatedAt.ToUnixTimestamp());
            Assert.AreEqual(2, webhook.EventTypes.Count());
            Assert.AreEqual(Webhook.WebhookEventType.TRANSACTION_SUCCEEDED, webhook.EventTypes[0]);
            Assert.AreEqual(Webhook.WebhookEventType.TRANSACTION_FAILED, webhook.EventTypes[1]);
        }
        [TestMethod]
        public void TestClient()
        {
            var client = ReadResult<Client>("client.json");
            Assert.AreEqual("client_11cc57776f7954925cf9", client.Id);
            Assert.AreEqual("lovely-client@example.com", client.Email);
            Assert.AreEqual("Lovely Client", client.Description);
            Assert.AreEqual(1340199740, client.CreatedAt.ToUnixTimestamp());
            Assert.AreEqual(1340199760, client.UpdatedAt.ToUnixTimestamp());
            Assert.AreEqual(0, client.Payments.Count);
            Assert.IsNotNull(client.Subscriptions);
        }

        [TestMethod]
        public void TestOffer()
        {
            var offer = ReadResult<Offer>("offer.json");
            Assert.AreEqual("offer_caef7c90466b540cf8d1", offer.Id);
            Assert.AreEqual("Test Offer", offer.Name);
            Assert.AreEqual(42.00, offer.AmountFormatted);
            Assert.AreEqual("EUR", offer.Currency);
            Assert.AreEqual("1 MONTH", offer.Interval.ToString());
            Assert.AreEqual(new DateTime(2013, 10, 11, 13, 2, 38, DateTimeKind.Local), offer.CreatedAt);
            Assert.AreEqual(new DateTime(2013, 10, 11, 13, 2, 38, DateTimeKind.Local), offer.UpdatedAt);
            Assert.AreEqual(20, offer.SubscriptionCount.Inactive);
            Assert.AreEqual(0, offer.TrialPeriodDays);
        }

        [TestMethod]
        public void TestPayment()
        {
            var payment = ReadResult<Payment>("payment.json");
            Assert.AreEqual("pay_9eb3371ae4ca3a51ab255a2e", payment.Id);
            Assert.AreEqual(Payment.PaymentType.CREDIT_CARD, payment.Type);
            Assert.AreEqual("client_11cc57776f7954925cf9", payment.Client.Id);
            Assert.AreEqual(Payment.CardTypes.MASTERCARD, payment.CardType);
            Assert.IsNull(payment.Country);
            Assert.AreEqual(12, payment.ExpireMonth);
            Assert.AreEqual(2014, payment.ExpireYear);
            Assert.AreEqual("Joddle Botten", payment.CardHolder);
            Assert.AreEqual("1111", payment.Last4);
            Assert.AreEqual(1381489629, payment.CreatedAt.ToUnixTimestamp());
            Assert.AreEqual(1381489629, payment.UpdatedAt.ToUnixTimestamp());
        }

        [TestMethod]
        public void TestPayments()
        {
            var payments = ReadResults<Payment>("payments.json").Data;
            Assert.AreEqual(payments.Count(), 20);
            foreach (var pa in payments)
            {
                Assert.IsNotNull(pa.Id);
            }
        }
        
        [TestMethod]
        public void TestPreauthorization()
        {
            var paym = new Payment();
            var p = ReadResults<Preauthorization>("preauthorizations.json").Data.FirstOrDefault();
            Assert.AreEqual("preauth_31eb90495837447f76b7", p.Id);
            Assert.AreEqual("client_11cc57776f7954925cf9", p.Client.Id);
            Assert.AreEqual("pay_9eb3371ae4ca3a51ab255a2e", p.Payment.Id);
            Assert.IsFalse(p.Livemode);
            Assert.AreEqual(Preauthorization.PreauthorizationStatus.CLOSED, p.Status);
            Assert.AreEqual(1349948920, p.CreatedAt.ToUnixTimestamp());
            Assert.AreEqual(1349948920, p.UpdatedAt.ToUnixTimestamp());
        }
        
        [TestMethod]
        public void TestRefund()
        {
            var p = ReadResult<Refund>("refund.json");
            Assert.AreEqual("refund_66b4ad3bc514a2ac0eb8", p.Id);
            Assert.AreEqual(5.00, p.AmountFormatted);
            Assert.AreEqual("tran_88fb15ddf21039335ff910653c65", p.Transaction.Id);
            Assert.IsFalse(p.Livemode);
            Assert.AreEqual(Refund.RefundStatus.REFUNDED, p.Status);
            Assert.AreEqual(20000, p.ResponseCode);
            Assert.IsNull(p.Description);
            Assert.AreEqual(1395238945, p.CreatedAt.ToUnixTimestamp());
            Assert.AreEqual(1395238945, p.UpdatedAt.ToUnixTimestamp());
        }

        [TestMethod]
        public void TestSubscription()
        {
            var s = ReadResult<Subscription>("subscription.json");
            Assert.AreEqual("sub_2c3af703dff3cc9990b3", s.Id);
            Assert.AreEqual("offer_9a8d20d7a52d1cbecd6c", s.Offer.Id);
            Assert.AreEqual("client_a64eacffe5e67b784eac", s.Client.Id);
            Assert.AreEqual("pay_86be5e6c3547b752cb4ce93c", s.Payment.Id);
            Assert.IsFalse(s.Livemode);
            Assert.IsFalse(s.CancelAtPeriodEnd);
            Assert.IsNull(s.TrialEnd);
            Assert.IsNull(s.TrialStart);
            Assert.AreEqual(1402488673, s.CreatedAt.ToUnixTimestamp(), "Created at");
            Assert.AreEqual(1402488731, s.UpdatedAt.ToUnixTimestamp(), "Updated at");
            Assert.IsNull(s.NextCaptureAt);
        }
        [TestMethod]
        public void TestSubscriptions()
        {
            var subs = ReadResults<Subscription>("subscriptions.json");
            Assert.AreEqual(subs.DataCount, 95);
            foreach (var sub in subs.Data)
            {
                Assert.IsNotNull(sub.Id);
            }
        }

        [TestMethod]
        public void TestTransaction()
        {
            var t = ReadResult<Transaction>("transaction.json");
            Assert.AreEqual("tran_6983a8d95b03a594a34acd529fb5", t.Id);
            Assert.AreEqual("client_db50b6496357e2700f00", t.Client.Id);
            Assert.AreEqual("pay_6bc452ea00f15be0225a1a6e", t.Payment.Id);
            Assert.IsFalse(t.Livemode);
            Assert.AreEqual(Transaction.TransactionStatus.CLOSED, t.Status);
            Assert.AreEqual("Bar", t.Description);
            Assert.AreEqual(59.00, t.AmountFormatted);
            Assert.IsNull(t.Refunds);
            Assert.IsNull(t.Preauthorization);
            Assert.AreEqual(20000, t.ResponseCode);
            Assert.IsFalse(t.IsFraud);
            Assert.AreEqual("7357.7357.7357", t.ShortId);
            Assert.AreEqual(1395239167, t.CreatedAt.ToUnixTimestamp(), "Created at");
            Assert.AreEqual(1395239167, t.UpdatedAt.ToUnixTimestamp(), "Updated at");
        }
    }
}

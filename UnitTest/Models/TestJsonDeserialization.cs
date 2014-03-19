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
            return JsonConvert.DeserializeObject<SingleResult<T>>(GetInputFile(filename), new UnixTimestampConverter()).Data;
        }

        private static IEnumerable<T> ReadResults<T>(string filename)
        {
            return JsonConvert.DeserializeObject<MultipleResults<T>>(GetInputFile(filename),
                new UnixTimestampConverter()).Data;
        }
        [TestMethod]
        public void TestClients()
        {
            var clients = ReadResults<Client>("clients.json");
            int hotmailAcounts = clients.Count(x=> x.Email == "javicantos22@hotmail.es");
            Assert.IsTrue(hotmailAcounts == clients.Count(), "Invalid clients count");
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
            Assert.IsNull(client.Subscriptions);
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
            Assert.AreEqual(Payment.TypePayment.CreditCard, payment.Type);
            Assert.AreEqual("client_11cc57776f7954925cf9", payment.Client);
            Assert.AreEqual(Payment.TypeCard.Mastercard, payment.CardType);
            Assert.IsNull(payment.Country);
            Assert.AreEqual(12, payment.ExpireMonth);
            Assert.AreEqual(2014, payment.ExpireYear);
            Assert.AreEqual("Joddle Botten", payment.CardHolder);
            Assert.AreEqual("1111", payment.Last4);
            Assert.AreEqual(1381489629, payment.CreatedAt.ToUnixTimestamp());
            Assert.AreEqual(1381489629, payment.UpdatedAt.ToUnixTimestamp());
        }
        
        [TestMethod]
        public void TestPreauthorization()
        {
            var p = ReadResults<Preauthorization>("preauthorizations.json").FirstOrDefault();
            Assert.AreEqual("preauth_31eb90495837447f76b7", p.Id);
            Assert.AreEqual("client_11cc57776f7954925cf9", p.Client.Id);
            Assert.AreEqual("pay_9eb3371ae4ca3a51ab255a2e", p.Payment.Id);
            Assert.IsFalse(p.Livemode);
            Assert.AreEqual(Preauthorization.PreauthorizationStatus.Closed, p.Status);
            Assert.AreEqual(1349948920, p.CreatedAt.ToUnixTimestamp());
            Assert.AreEqual(1349948920, p.UpdatedAt.ToUnixTimestamp());
        }
        
        [TestMethod]
        public void TestRefund()
        {
            var p = ReadResult<Refund>("refund.json");
            Assert.AreEqual("refund_87bc404a95d5ce616049", p.Id);
            Assert.AreEqual(0.42, p.AmountFormatted);
            Assert.AreEqual("tran_54645bcb98ba7acfe204", p.Transaction.Id);
            Assert.IsFalse(p.Livemode);
            Assert.AreEqual(Refund.RefundStatus.Refunded, p.Status);
            Assert.AreEqual(200, p.ResponseCode);
            Assert.AreEqual("foo", p.Description);
            Assert.AreEqual(1349947042, p.CreatedAt.ToUnixTimestamp());
            Assert.AreEqual(1349947042, p.UpdatedAt.ToUnixTimestamp());
        }

        [TestMethod]
        public void TestSubscription()
        {
            var s = ReadResult<Subscription>("subscription.json");
            Assert.AreEqual("sub_666b6315d641420f7897", s.Id);
            Assert.AreEqual("offer_caef7c90466b540cf8d1", s.Offer.Id);
            Assert.AreEqual("client_11cc57776f7954925cf9", s.Client.Id);
            Assert.AreEqual("pay_9eb3371ae4ca3a51ab255a2e", s.Payment.Id);
            Assert.IsFalse(s.Livemode);
            Assert.IsFalse(s.CancelAtPeriodEnd);
            Assert.IsNull(s.TrialEnd);
            Assert.IsNull(s.TrialStart);
            Assert.AreEqual(1381489644, s.CreatedAt.ToUnixTimestamp(), "Created at");
            Assert.AreEqual(1381489644, s.UpdatedAt.ToUnixTimestamp(), "Updated at");
            Assert.AreEqual(1384171644, s.NextCaptureAt.ToUnixTimestamp(), "Next capture at");
        }

        [TestMethod]
        public void TestTransaction()
        {
            var t = ReadResult<Transaction>("transaction.json");
            Assert.AreEqual("tran_6983a8d95b03a594a34acd529fb5", t.Id);
            Assert.AreEqual("client_db50b6496357e2700f00", t.Client.Id);
            Assert.AreEqual("pay_6bc452ea00f15be0225a1a6e", t.Payment.Id);
            Assert.IsFalse(t.Livemode);
            Assert.AreEqual(Transaction.TransactionStatus.Closed, t.Status);
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

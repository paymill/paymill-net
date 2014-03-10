using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymillWrapper.Models;
using PaymillWrapper.Net;
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
            return JsonConvert.DeserializeObject<MultipleResults<T>>(GetInputFile(filename), new UnixTimestampConverter()).Data;
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

    /*    [TestMethod]
        public void TestOffer()
        {
            var offer = ReadResult<Offer>("offer.json");
            Assert.AreEqual("offer_caef7c90466b540cf8d1", offer.Id);
            Assert.AreEqual("Test Offer", offer.Name);
            Assert.AreEqual(42, offer.AmountFormatted);
            Assert.AreEqual("EUR", offer.Currency);
            Assert.AreEqual("1 MONTH", offer.Interval);
            Assert.AreEqual(new DateTime(2013, 10, 11, 12, 2, 38, DateTimeKind.Utc), offer.CreatedAt);
            Assert.AreEqual(new DateTime(2013, 10, 11, 12, 2, 38, DateTimeKind.Utc), offer.UpdatedAt);
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
            Assert.AreEqual(CardType.Mastercard, payment.CardType);
            Assert.IsNull(payment.Country);
            Assert.AreEqual(12, payment.ExpireMonth);
            Assert.AreEqual(2014, payment.ExpireYear);
            Assert.AreEqual("Joddle Botten", payment.CardHolder);
            Assert.AreEqual("1111", payment.Last4);
            Assert.AreEqual(new DateTime(2013, 10, 11, 12, 7, 9, DateTimeKind.Utc), payment.CreatedAt);
            Assert.AreEqual(new DateTime(2013, 10, 11, 12, 7, 9, DateTimeKind.Utc), payment.UpdatedAt);
        }

        [TestMethod]
        public void TestPreauthorization()
        {
            var p = ReadResults<Preauthorization>("preauthorizations.json").FirstOrDefault();
            Assert.AreEqual("preauth_31eb90495837447f76b7", p.Id);
            Assert.AreEqual("client_11cc57776f7954925cf9", p.Client.Id);
            Assert.AreEqual("pay_9eb3371ae4ca3a51ab255a2e", p.Payment.Id);
            Assert.IsFalse(p.Livemode);
            Assert.AreEqual(PreauthorizationStatus.Closed, p.Status);
            Assert.AreEqual(new DateTime(2012, 10, 11, 10, 48, 40, DateTimeKind.Utc), p.CreatedAt);
            Assert.AreEqual(new DateTime(2012, 10, 11, 10, 48, 40, DateTimeKind.Utc), p.UpdatedAt);
        }

        [TestMethod]
        public void TestRefund()
        {
            var p = ReadResult<Refund>("refund.json");
            Assert.AreEqual("refund_87bc404a95d5ce616049", p.Id);
            Assert.AreEqual(0.42, p.AmountFormatted);
            Assert.AreEqual("tran_54645bcb98ba7acfe204", p.Transaction.Id);
            Assert.IsFalse(p.Livemode);
            Assert.AreEqual(RefundStatus.Refunded, p.Status);
            Assert.AreEqual(ResponseCode.Success, p.ResponseCode);
            Assert.AreEqual("foo", p.Description);
            Assert.AreEqual(new DateTime(2012, 10, 11, 10, 17, 22, DateTimeKind.Utc), p.CreatedAt);
            Assert.AreEqual(new DateTime(2012, 10, 11, 10, 17, 22, DateTimeKind.Utc), p.UpdatedAt);
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
            Assert.AreEqual(new DateTime(2013, 10, 11, 12, 7, 24, DateTimeKind.Utc), s.CreatedAt, "Created at");
            Assert.AreEqual(new DateTime(2013, 10, 11, 12, 7, 24, DateTimeKind.Utc), s.UpdatedAt, "Updated at");
            Assert.AreEqual(new DateTime(2013, 11, 11, 13, 7, 24, DateTimeKind.Utc), s.NextCaptureAt, "Next capture at");
        }

        [TestMethod]
        public void TestTransaction()
        {
            var t = ReadResult<Transaction>("transaction.json");
            Assert.AreEqual("tran_6983a8d95b03a594a34acd529fb5", t.Id);
            Assert.AreEqual("client_db50b6496357e2700f00", t.Client.Id);
            Assert.AreEqual("pay_6bc452ea00f15be0225a1a6e", t.Payment.Id);
            Assert.IsFalse(t.Livemode);
            Assert.AreEqual(TransactionStatus.Closed, t.Status);
            Assert.AreEqual("Bar", t.Description);
            Assert.AreEqual(59, t.AmountFormatted);
            Assert.IsNull(t.Refunds);
            Assert.IsNull(t.Preauthorization);
            Assert.AreEqual(ResponseCode.Success, t.ResponseCode);
            Assert.IsFalse(t.IsFraud);
            Assert.AreEqual("7357.7357.7357", t.ShortId);
            Assert.AreEqual(new DateTime(2013, 10, 1, 7, 28, 45, DateTimeKind.Utc), t.CreatedAt, "Created at");
            Assert.AreEqual(new DateTime(2013, 10, 1, 7, 28, 45, DateTimeKind.Utc), t.UpdatedAt, "Updated at");
        */
    }
}

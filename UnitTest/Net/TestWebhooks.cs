using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymillWrapper;
using PaymillWrapper.Service;
using System.Collections.Generic;
using PaymillWrapper.Models;
using PaymillWrapper.Exceptions;

namespace UnitTest.Net
{
    [TestClass]
    public class TestWebhooks
    {
        PaymillContext _paymill = null;
        [TestInitialize]
        public void Initialize()
        {
            _paymill = new PaymillContext("9a4129b37640ea5f62357922975842a1");
        }
        [TestMethod]
        public void CreateURLWebhook()
        {
            Webhook webhook = _paymill.WebhookService.CreateUrlWebhookAsync(new Uri("http://google.com"),
                        Webhook.WebhookEventType.SUBSCRIPTION_SUCCEEDED,
                        Webhook.WebhookEventType.SUBSCRIPTION_FAILED).Result;
            Assert.IsTrue(webhook.Id != String.Empty, "CreateURLWebhook Fail");
        }
        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void CreateEmailWebhookFiled()
        {
            Webhook.WebhookEventType[] eventTypes = { Webhook.WebhookEventType.SUBSCRIPTION_SUCCEEDED, Webhook.WebhookEventType.SUBSCRIPTION_FAILED };
            Webhook webhook = _paymill.WebhookService.CreateEmailWebhookAsync("<your-webhook-email>", eventTypes).Result;
        }
        [TestMethod]
        public void CreateEmailWebhook()
        {
            Webhook.WebhookEventType[] eventTypes = { Webhook.WebhookEventType.SUBSCRIPTION_SUCCEEDED, Webhook.WebhookEventType.SUBSCRIPTION_FAILED };
            Webhook webhook = _paymill.WebhookService.CreateEmailWebhookAsync("test@email.com", eventTypes).Result;
            Assert.IsTrue(webhook.Id != String.Empty, "CreateEmailWebhook Fail");
        }
        [TestMethod]
        public void GetWebhook()
        {
            Webhook.WebhookEventType[] eventTypes = { Webhook.WebhookEventType.SUBSCRIPTION_SUCCEEDED, Webhook.WebhookEventType.SUBSCRIPTION_FAILED };
            Webhook webhook = _paymill.WebhookService.CreateEmailWebhookAsync("test@email.com", eventTypes).Result;
            Assert.IsTrue(webhook.Id != String.Empty, "CreateEmailWebhook Fail");
            Webhook newWebhook = _paymill.WebhookService.GetAsync(webhook.Id).Result;
            Assert.IsTrue(webhook.Id != String.Empty, "GetWebhook Fail");
            Assert.IsTrue(webhook.EventTypes.Length == 2, "GetWebhook Fail");
        }
        [TestMethod]
        public void UpdateWebhook()
        {
            Webhook.WebhookEventType[] eventTypes = { Webhook.WebhookEventType.SUBSCRIPTION_SUCCEEDED, Webhook.WebhookEventType.SUBSCRIPTION_FAILED };
            Webhook webhook = _paymill.WebhookService.CreateEmailWebhookAsync("test@email.com", eventTypes).Result;
            Assert.IsTrue(webhook.Id != String.Empty, "CreateEmailWebhook Fail");
            // get
            Webhook newWebhook = _paymill.WebhookService.GetAsync(webhook.Id).Result;
            Assert.IsTrue(webhook.Id != String.Empty, "GetWebhook Fail");
            Assert.IsTrue(webhook.EventTypes.Length == 2, "GetWebhook Fail");
            // update 
            webhook.Email = "test1@mail.com";
            Webhook updatedWebhook = _paymill.WebhookService.UpdateAsync(webhook).Result;
            Assert.IsTrue(updatedWebhook.Email == "test1@mail.com", "Update Webhook Fail");
        }
        [TestMethod]
        public void RemoveWebhook()
        {
            Webhook.WebhookEventType[] eventTypes = { Webhook.WebhookEventType.SUBSCRIPTION_SUCCEEDED, Webhook.WebhookEventType.SUBSCRIPTION_FAILED };
            Webhook webhook = _paymill.WebhookService.CreateEmailWebhookAsync("test@email.com", eventTypes).Result;
            Assert.IsTrue(webhook.Id != String.Empty, "CreateEmailWebhook Fail");
            // get
            Boolean result = _paymill.WebhookService.DeleteAsync(webhook.Id).Result;
            Assert.IsTrue(result == true, "Remove Webhook");

        }
        [TestMethod]
        public void GetWebhooks()
        {
            Webhook.WebhookEventType[] eventTypes = { Webhook.WebhookEventType.SUBSCRIPTION_SUCCEEDED, Webhook.WebhookEventType.SUBSCRIPTION_FAILED };
            Webhook webhook = _paymill.WebhookService.CreateEmailWebhookAsync("test@email.com", eventTypes).Result;
            Assert.IsTrue(webhook.Id != String.Empty, "CreateEmailWebhook Fail");
            PaymillList<Webhook> resultList = _paymill.WebhookService.ListAsync().Result;
            Assert.IsTrue(resultList.DataCount > 0, "Get Webhooks failed");
        }
        [TestMethod]
        public void ListWebhooks()
        {
            var list = _paymill.WebhookService.ListAsync().Result;
            Assert.IsTrue(list.DataCount > 0, "List Webhooks Failed");
        }
        [TestMethod]
        public void ListOrderByCreatedAt()
        {
            Webhook.Order orderDesc = Webhook.CreateOrder().ByCreatedAt().Desc();
            Webhook.Order orderAsc = Webhook.CreateOrder().ByCreatedAt().Asc();

            List<Webhook> webhooksDesc = _paymill.WebhookService.ListAsync(null, orderDesc).Result.Data;
            List<Webhook> webhooksAsc = _paymill.WebhookService.ListAsync(null, orderAsc).Result.Data;
            if (webhooksDesc.Count > 1
                && webhooksAsc.Count > 1)
            {
                Assert.AreNotEqual(webhooksDesc[0].Id, webhooksAsc[0].Id);
            }
        }
    }
}

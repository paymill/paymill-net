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
        Paymill _paymill = null;
        [TestInitialize]
        public void Initialize()
        {
            _paymill = new Paymill("9a4129b37640ea5f62357922975842a1");
        }
        [TestMethod]
        public void CreateURLWebhook()
        {
            Webhook webhook = _paymill.WebhookService.CreateUrlWebhookAsync(new Uri("http://google.com"),
                        Webhook.EventType.SUBSCRIPTION_SUCCEEDED,
                        Webhook.EventType.SUBSCRIPTION_FAILED).Result;
            Assert.IsTrue(webhook.Id != String.Empty, "CreateURLWebhook Fail");
        }
        [TestMethod]
        [ExpectedException(typeof(PaymillRequestException))]
        public void CreateEmailWebhookFiled()
        {
            Webhook.EventType[] eventTypes = { Webhook.EventType.SUBSCRIPTION_SUCCEEDED, Webhook.EventType.SUBSCRIPTION_FAILED };
            Webhook webhook = _paymill.WebhookService.CreateEmailWebhookAsync("<your-webhook-email>", eventTypes).Result;
        }
        [TestMethod]
        public void CreateEmailWebhook()
        {
            Webhook.EventType[] eventTypes = { Webhook.EventType.SUBSCRIPTION_SUCCEEDED, Webhook.EventType.SUBSCRIPTION_FAILED };
            Webhook webhook = _paymill.WebhookService.CreateEmailWebhookAsync("test@email.com", eventTypes).Result;
            Assert.IsTrue(webhook.Id != String.Empty, "CreateEmailWebhook Fail");
        }
        [TestMethod]
        public void GetWebhook()
        {
            Webhook.EventType[] eventTypes = { Webhook.EventType.SUBSCRIPTION_SUCCEEDED, Webhook.EventType.SUBSCRIPTION_FAILED };
            Webhook webhook = _paymill.WebhookService.CreateEmailWebhookAsync("test@email.com", eventTypes).Result;
            Assert.IsTrue(webhook.Id != String.Empty, "CreateEmailWebhook Fail");
            Webhook newWebhook = _paymill.WebhookService.GetAsync(webhook.Id).Result;
            Assert.IsTrue(webhook.Id != String.Empty, "GetWebhook Fail");
            Assert.IsTrue(webhook.EventTypes.Length == 2, "GetWebhook Fail");
        }
        [TestMethod]
        public void UpdateWebhook()
        {
            Webhook.EventType[] eventTypes = { Webhook.EventType.SUBSCRIPTION_SUCCEEDED, Webhook.EventType.SUBSCRIPTION_FAILED };
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
            Webhook.EventType[] eventTypes = { Webhook.EventType.SUBSCRIPTION_SUCCEEDED, Webhook.EventType.SUBSCRIPTION_FAILED };
            Webhook webhook = _paymill.WebhookService.CreateEmailWebhookAsync("test@email.com", eventTypes).Result;
            Assert.IsTrue(webhook.Id != String.Empty, "CreateEmailWebhook Fail");
            // get
            Boolean result = _paymill.WebhookService.DeleteAsync(webhook.Id).Result;
            Assert.IsTrue(result == true, "Remove Webhook");

        }
        [TestMethod]
        public void GetWebhooks()
        {
            Webhook.EventType[] eventTypes = { Webhook.EventType.SUBSCRIPTION_SUCCEEDED, Webhook.EventType.SUBSCRIPTION_FAILED };
            Webhook webhook = _paymill.WebhookService.CreateEmailWebhookAsync("test@email.com", eventTypes).Result;
            Assert.IsTrue(webhook.Id != String.Empty, "CreateEmailWebhook Fail");
            List<Webhook> resultList = _paymill.WebhookService.ListAsync().Result;
            Assert.IsTrue(resultList.Count > 0, "Get Webhooks failed");

        }
    }
}

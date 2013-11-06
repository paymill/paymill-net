using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymillWrapper;
using PaymillWrapper.Service;
using System.Collections.Generic;
using PaymillWrapper.Models;

namespace UnitTest.Net
{
    [TestClass]
    public class TestWebhooks
    {
        [TestInitialize]
        public void Initialize()
        {
            Paymill.ApiKey = "9a4129b37640ea5f62357922975842a1";
            Paymill.ApiUrl = "https://api.paymill.de/v2";
        }
        [TestMethod]
        public void CreateURLWebhook()
        {
            WebhookService webhookService = Paymill.GetService<WebhookService>();
            Webhook webhook = webhookService.CreateUrl(new Uri("http://google.com"),
                        EventType.SUBSCRIPTION_SUCCEEDED,
                        EventType.SUBSCRIPTION_FAILED);
            Assert.IsTrue(webhook.Id != String.Empty, "CreateURLWebhook Fail");
        }
        [TestMethod]
        [ExpectedException(typeof(PaymillRequestException))]
        public void CreateEmailWebhookFiled()
        {
            EventType[] eventTypes = { EventType.SUBSCRIPTION_SUCCEEDED, EventType.SUBSCRIPTION_FAILED };
            WebhookService srv = Paymill.GetService<WebhookService>();
            Webhook webhook = new Webhook();
            webhook.Email ="<your-webhook-email>";
            webhook.EventTypes = eventTypes;
            webhook = srv.Create(webhook);
        }
        [TestMethod]
        public void CreateEmailWebhook()
        {
            EventType[] eventTypes = { EventType.SUBSCRIPTION_SUCCEEDED, EventType.SUBSCRIPTION_FAILED };
            WebhookService srv = Paymill.GetService<WebhookService>();
            Webhook webhook = new Webhook();
            webhook.Email = "test@email.com";
            webhook.EventTypes = eventTypes;
            webhook = srv.Create(webhook);
            Assert.IsTrue(webhook.Id != String.Empty, "CreateEmailWebhook Fail");
        }
        [TestMethod]
        public void GetWebhook()
        {
            EventType[] eventTypes = { EventType.SUBSCRIPTION_SUCCEEDED, EventType.SUBSCRIPTION_FAILED };
            WebhookService srv = Paymill.GetService<WebhookService>();
            Webhook webhook = new Webhook();
            webhook.Email = "test@email.com";
            webhook.EventTypes = eventTypes;
            webhook = srv.Create(webhook);
            Assert.IsTrue(webhook.Id != String.Empty, "CreateEmailWebhook Fail");
            Webhook newWebhook = srv.Get(webhook.Id);
            Assert.IsTrue(webhook.Id != String.Empty, "GetWebhook Fail");
            Assert.IsTrue(webhook.EventTypes.Length == 2, "GetWebhook Fail");
        }
        [TestMethod]
        public void UpdateWebhook()
        {
            EventType[] eventTypes = { EventType.SUBSCRIPTION_SUCCEEDED, EventType.SUBSCRIPTION_FAILED };
            WebhookService srv = Paymill.GetService<WebhookService>();
            Webhook webhook = new Webhook();
            webhook.Email = "test@email.com";
            webhook.EventTypes = eventTypes;
            webhook = srv.Create(webhook);
            Assert.IsTrue(webhook.Id != String.Empty, "CreateEmailWebhook Fail");
            // get
            Webhook newWebhook = srv.Get(webhook.Id);
            Assert.IsTrue(webhook.Id != String.Empty, "GetWebhook Fail");
            Assert.IsTrue(webhook.EventTypes.Length == 2, "GetWebhook Fail");
            // update 
            Webhook updatedWebhook = srv.Update(webhook);
        }
        [TestMethod]
        public void RemoveWebhook()
        {
            EventType[] eventTypes = { EventType.SUBSCRIPTION_SUCCEEDED, EventType.SUBSCRIPTION_FAILED };
            WebhookService srv = Paymill.GetService<WebhookService>();
            Webhook webhook = new Webhook();
            webhook.Email = "test@email.com";
            webhook.EventTypes = eventTypes;
            webhook = srv.Create(webhook);
            Assert.IsTrue(webhook.Id != String.Empty, "CreateEmailWebhook Fail");
            // get
            Boolean result = srv.Remove(webhook.Id);
            Assert.IsTrue(result == true, "Remove Webhook");
            
        }
        [TestMethod]
        public void GetWebhooks()
        {
            EventType[] eventTypes = { EventType.SUBSCRIPTION_SUCCEEDED, EventType.SUBSCRIPTION_FAILED };
            WebhookService srv = Paymill.GetService<WebhookService>();
            Webhook webhook = new Webhook();
            webhook.Email = "test@email.com";
            webhook.EventTypes = eventTypes;
            webhook = srv.Create(webhook);
            Assert.IsTrue(webhook.Id != String.Empty, "CreateEmailWebhook Fail");
            List<Webhook> resultList = srv.GetWebhooks();
            Assert.IsTrue(resultList.Count > 0, "Get Webhooks failed");

        }
    }
}

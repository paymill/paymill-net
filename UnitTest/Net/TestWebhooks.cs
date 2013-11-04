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
                        PaymillWrapper.Models.Webhook.EventType.SUBSCRIPTION_SUCCEEDED,
                        PaymillWrapper.Models.Webhook.EventType.SUBSCRIPTION_FAILED);
            Assert.IsTrue(webhook.Id != String.Empty, "CreateURLWebhook Fail");
        }
       
    }
}

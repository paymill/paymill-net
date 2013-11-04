using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymillWrapper;
using PaymillWrapper.Service;
using System.Collections.Generic;
using PaymillWrapper.Models;

namespace UnitTest.Net
{
    [TestClass]
    public class TestClients
    {
        [TestInitialize]
        public void Initialize()
        {
            Paymill.ApiKey = "9a4129b37640ea5f62357922975842a1";
            Paymill.ApiUrl = "https://api.paymill.de/v2";
        }
        [TestMethod]
        public void CreateClient()
        {
            ClientService clientService = Paymill.GetService<ClientService>();
            Client client = clientService.Create("lovely-client@example.com", "Lovely Client");
            Assert.IsTrue(client.Id != String.Empty, "CreateClient Fail");
        }
        [TestMethod]
        public void RemoveClient()
        {
            ClientService clientService = Paymill.GetService<ClientService>();
            Client client = clientService.Create("lovely-client@example.com", "Lovely Client");
            Assert.IsTrue(client.Id != String.Empty, "CreateClient Fail");

            Boolean result = clientService.RemoveClient(client.Id);
            Assert.IsTrue(result, "Remove  Offer");

  
        }
    }
}

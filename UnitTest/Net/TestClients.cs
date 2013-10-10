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
        Paymill _paymill = null;
        [TestInitialize]
        public void Initialize()
        {
            _paymill = new Paymill("9a4129b37640ea5f62357922975842a1");
        }
        [TestMethod]
        public void CreateClient()
        {
            Client client = _paymill.Clients.Create("lovely-client@example.com", "Lovely Client");
            Assert.IsTrue(client.Id != String.Empty, "CreateClient Fail");
        }
        [TestMethod]
        public void RemoveClient()
        {
            Client client = _paymill.Clients.Create("lovely-client@example.com", "Lovely Client");
            Assert.IsTrue(client.Id != String.Empty, "CreateClient Fail");

            Boolean result = _paymill.Clients.Remove(client.Id);
            Assert.IsTrue(result, "Remove  Offer");


        }
        [TestMethod]
        public void UpdateClient()
        {
            Client client = _paymill.Clients.Create("lovely-client@example.com", "Lovely Client");
            Assert.IsTrue(client.Id != String.Empty, "CreateClient Fail");

            client.Email = "test@mail.com";
            var updatetedClient = _paymill.Clients.Update(client);
            Assert.IsTrue(updatetedClient.Email == "test@mail.com", "Update Client Failed");

        }
    }
}

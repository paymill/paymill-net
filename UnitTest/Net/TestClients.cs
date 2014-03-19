using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymillWrapper;
using PaymillWrapper.Service;
using System.Collections.Generic;
using PaymillWrapper.Models;
using PaymillWrapper.Utils;

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
        public void CreateClientWithDescription()
        {
            Client client = _paymill.ClientService.CreateWithDescriptionAsync("Lovely Client").Result;
            Assert.IsFalse(String.IsNullOrWhiteSpace(client.Id), "CreateClient Fail");
        }
        [TestMethod]
        public void CreateClientWithEmail()
        {
            Client client = _paymill.ClientService.CreateWithEmailAsync("lovely-client@example.com").Result;
            Assert.IsFalse(String.IsNullOrWhiteSpace(client.Id), "CreateClient Fail");
        }
        [TestMethod]
        public void CreateClientWithEmailAndDescription()
        {
            Client client = _paymill.ClientService.CreateWithEmailAndDescriptionAsync("lovely-client@example.com", "Lovely Client").Result;
            Assert.IsFalse(String.IsNullOrWhiteSpace(client.Id), "CreateClient Fail");
        }
        [TestMethod]
        public void GetClientsList()
        {
            _paymill.ClientService.CreateWithEmailAndDescriptionAsync("lovely-client@example.com", "Lovely Client").Wait();
            List<Client> clientsList = _paymill.ClientService.ListAsync().Result;
            Assert.IsTrue(clientsList.Count > 0, "Remove  Offer");
        }
        [TestMethod]
        public void GetClientsWithParameters()
        {
            Filter filter = new Filter();
            filter.Add("email", "javicantos22@hotmail.es"); //OK
            List<Client> lstClients = _paymill.ClientService.ListAsync(filter).Result;
            Assert.IsTrue(lstClients.FindAll(x => x.Email == "javicantos22@hotmail.es").Count == lstClients.Count);
        }
        [TestMethod]
        public void DeleteClient()
        {
            Client client = _paymill.ClientService.CreateWithEmailAndDescriptionAsync("lovely-client@example.com", "Lovely Client").Result;
            Assert.IsFalse(String.IsNullOrWhiteSpace( client.Id), "CreateClient Fail");
            Boolean result = _paymill.ClientService.DeleteAsync(client.Id).Result;
            Assert.IsTrue(result, "Remove Client");
        }
        [TestMethod]
        public void UpdateClient()
        {
            Client client = _paymill.ClientService.CreateWithEmailAndDescriptionAsync("lovely-client@example.com", "Lovely Client").Result;
            Assert.IsFalse(String.IsNullOrWhiteSpace(client.Id ), "CreateClient Fail");

            client.Email = "test@mail.com";
            var updatetedClient = _paymill.ClientService.UpdateAsync(client).Result;
            Assert.IsTrue(updatetedClient.Email == "test@mail.com", "Update Client Failed");

        }
    }
}

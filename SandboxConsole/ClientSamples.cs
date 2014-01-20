using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PaymillWrapper;
using PaymillWrapper.Models;
using PaymillWrapper.Net;
using PaymillWrapper.Service;
using System.ComponentModel;

namespace SandboxConsole
{
    public static class ClientSamples
    {
        public static void GetClients()
        {
            Paymill.ApiKey = Properties.Settings.Default.ApiKey;
            Paymill.ApiUrl = Properties.Settings.Default.ApiUrl;
            ClientService clientService = Paymill.GetService<ClientService>();

            Console.WriteLine("Waiting request list clients...");
            List<Client> lstClients = clientService.GetClients();

            foreach (Client c in lstClients)
            {
                Console.WriteLine(String.Format("ClientID:{0}", c.Id));
            }

            Console.Read();
        }
        public static void GetClientsWithParameters()
        {
            Paymill.ApiKey = Properties.Settings.Default.ApiKey;
            Paymill.ApiUrl = Properties.Settings.Default.ApiUrl;
            ClientService clientService = Paymill.GetService<ClientService>();

            Console.WriteLine("Waiting request list clients with parameters...");

            Filter filter = new Filter();
            filter.Add("email", "javicantos22@hotmail.es"); //OK
  
            List<Client> lstClients = clientService.GetClientsByFilter(filter);

            foreach (Client c in lstClients)
            {
                Console.WriteLine(String.Format("ClientID:{0}", c.Id));
            }

            Console.Read();
        }
        public static void CreateClient()
        {
            Paymill.ApiKey = Properties.Settings.Default.ApiKey;
            Paymill.ApiUrl = Properties.Settings.Default.ApiUrl;
            ClientService clientService = Paymill.GetService<ClientService>();
            Client newClient = clientService.Create("javicantos22@hotmail.es", "Test API");
            Utilities.printObject(newClient);

        }
        public static void GetClient()
        {
            Paymill.ApiKey = Properties.Settings.Default.ApiKey;
            Paymill.ApiUrl = Properties.Settings.Default.ApiUrl;
            ClientService clientService = Paymill.GetService<ClientService>();

            Console.WriteLine("Request client...");
            string clientID = "client_ad591663d69051d306a8";
            Client c = clientService.Get(clientID);

            Console.WriteLine("ClientID:" + c.Id);
            Console.WriteLine("Created at:" + c.Created_At.ToShortDateString());
            Console.Read();
        }
        public static void UpdateClient()
        {
            Paymill.ApiKey = Properties.Settings.Default.ApiKey;
            Paymill.ApiUrl = Properties.Settings.Default.ApiUrl;
            ClientService clientService = Paymill.GetService<ClientService>();

            Client c = new Client();
            c.Description = "Javier";
            c.Email = "javicantos33@hotmail.es";
            c.Id = "client_bbe895116de80b6141fd";

            Client updatedClient = clientService.Update(c);

            Console.WriteLine("ClientID:" + updatedClient.Id);
            Console.Read();
        }
        public static void RemoveClient(String clientId)
        {
            // lo borra pero no devuelve blanco
            // devuelve el objeto cliente con el identificador pasado por parametro

            Paymill.ApiKey = Properties.Settings.Default.ApiKey;
            Paymill.ApiUrl = Properties.Settings.Default.ApiUrl;
            ClientService clientService = Paymill.GetService<ClientService>();

            Console.WriteLine("Removing client...");

            string clientID = "client_180ad3d1042a1da4a0a0";
            bool reply = clientService.Remove(clientID);

            Console.WriteLine("Result remove:" + reply);
            Console.Read();
        }
    }
}

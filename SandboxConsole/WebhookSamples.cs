using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaymillWrapper;
using PaymillWrapper.Models;
using PaymillWrapper.Net;
using PaymillWrapper.Service;
using System.ComponentModel;

namespace SandboxConsole
{
    public static class WebhookSamples
    {
        public static void GetWebhooks()
        {
            Paymill.ApiKey = Properties.Settings.Default.ApiKey;
            Paymill.ApiUrl = Properties.Settings.Default.ApiUrl;
            WebhookService whService = Paymill.GetService<WebhookService>();

            Console.WriteLine("Waiting request list webhooks...");
            List<Webhook> lstWebhooks = whService.GetWebhooks();

            foreach (Webhook w in lstWebhooks)
            {
                Utilities.printObject(w);
            }

            Console.Read();
        }
       /* public static void CreateClient()
        {
            Paymill.ApiKey = Properties.Settings.Default.ApiKey;
            Paymill.ApiUrl = Properties.Settings.Default.ApiUrl;
            ClientService clientService = Paymill.GetService<ClientService>();
            Client newClient = clientService.Create("javicantos22@hotmail.es", "Prueba API");
            Utilities.printObject(newClient);

        }
        public static void GetClient()
        {
            Paymill.ApiKey = Properties.Settings.Default.ApiKey;
            Paymill.ApiUrl = Properties.Settings.Default.ApiUrl;
            ClientService clientService = Paymill.GetService<ClientService>();

            Console.WriteLine("Request client...");
            string clientID = "client_ad591663d69051d306a8";
            Client c = clientService.GetClient(clientID);

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

            Client updatedClient = clientService.UpdateClient(c);

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
            bool reply = clientService.RemoveClient(clientID);

            Console.WriteLine("Result remove:" + reply);
            Console.Read();
        }*/
    }
}

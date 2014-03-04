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
    public class SubscriptionSamples
    {
        Paymill pm = new Paymill(Properties.Settings.Default.ApiKey);
        public void GetSubscriptions()
        {
            Console.WriteLine("Waiting request list subscriptions...");
            List<Subscription> lstSubscriptions = pm.SubscriptionService.List();

            foreach (Subscription s in lstSubscriptions)
            {
                Utilities.printObject(s);
            }

            Console.Read();
        }
        public void GetSubscriptionsWithParameters()
        {
            Console.WriteLine("Waiting request list subscriptions with parameters...");

            Filter filter = new Filter();
            filter.Add("count", 1); //OK
            filter.Add("offset", 2); //OK
            filter.Add("offer", "offer_32008ddd39954e71ed48"); //KO
            List<Subscription> lstSubscriptions = pm.SubscriptionService.GetSubscriptionsByFilter(filter);

            foreach (Subscription s in lstSubscriptions)
            {
                Utilities.printObject(s);
            }

            Console.Read();
        }
       
        private Subscription createSubsription()
        {
            Paymill.ApiKey = Properties.Settings.Default.ApiKey;
            Paymill.ApiUrl = Properties.Settings.Default.ApiUrl;
            SubscriptionService susbscriptionService = Paymill.GetService<SubscriptionService>();
            ClientService clientService = Paymill.GetService<ClientService>();
            OfferService offerService = Paymill.GetService<OfferService>();

            var client = clientService.Create("test@mail.com", "test");
            var offer = ОfferSamples.CreateOfferObject();

            Subscription subscription = new Subscription();
            subscription.Client = client;
            subscription.Offer = offer;
            PaymentService paymentService = Paymill.GetService<PaymentService>();
            string token = "098f6bcd4621d373cade4e832627b4f6";
            Payment payment = paymentService.Create(token, client.Id);
            subscription.Payment = payment;
            return offerService.Subscribe(offer, client, payment);
        
        }
        public void AddSubscription()
        {
            Paymill.ApiKey = Properties.Settings.Default.ApiKey;
            Paymill.ApiUrl = Properties.Settings.Default.ApiUrl;
            OfferService offerService = Paymill.GetService<OfferService>();
            Client client = new Client() { Id = "client_bbe895116de80b6141fd" };
            Offer offer = new Offer() { Id = "offer_32008ddd39954e71ed48" };
            Payment payment = new Payment() { Id = "pay_81ec02206e9b9c587513" };
            Subscription newSubscription = offerService.Subscribe(offer, client, payment);

            Utilities.printObject(newSubscription);
            Console.Read();
        }
        public void GetSubscription()
        {
            Paymill.ApiKey = Properties.Settings.Default.ApiKey;
            Paymill.ApiUrl = Properties.Settings.Default.ApiUrl;
            SubscriptionService susbscriptionService = Paymill.GetService<SubscriptionService>();

            Console.WriteLine("Request subscription...");
            string subscriptionID = "sub_25523ba98729754be371";
            Subscription subscription = susbscriptionService.Get(subscriptionID);
            Utilities.printObject(subscription);

            Subscription subscription1 = susbscriptionService.Get("sub_ca7ed15bc2c8e97e29f2");
            Utilities.printObject(subscription1);
            Console.Read();
        }
        public void UpdateSubscription()
        {
            Paymill.ApiKey = Properties.Settings.Default.ApiKey;
            Paymill.ApiUrl = Properties.Settings.Default.ApiUrl;

            OfferService offerService = Paymill.GetService<OfferService>();
            PaymentService paymentService = Paymill.GetService<PaymentService>();
            SubscriptionService susbscriptionService = Paymill.GetService<SubscriptionService>();
            ClientService clientService = Paymill.GetService<ClientService>();
            Client newClient = clientService.Create("javicantos22@hotmail.es", "Test API");
            string token = "098f6bcd4621d373cade4e832627b4f6";
            Payment payment = paymentService.Create(token, newClient.Id);
            Offer offer = ОfferSamples.CreateOfferObject();
            Subscription newSubscription = offerService.Subscribe(offer, newClient, payment);
            Subscription subs = susbscriptionService.Get(newSubscription.Id);
            subs.Offer = offer;
            subs.Payment = payment;
            subs.CancelAtPeriodEnd = true;
            var updatedSubscription = susbscriptionService.Update(subs);

            Console.WriteLine("SubscriptionID:" + updatedSubscription.Id);
            Console.Read();
        }
        public void RemoveSubscription()
        {
            // se elimina correctamente pero el json de respuesta no devuelve vacio 

            Paymill.ApiKey = Properties.Settings.Default.ApiKey;
            Paymill.ApiUrl = Properties.Settings.Default.ApiUrl;
            SubscriptionService susbscriptionService = Paymill.GetService<SubscriptionService>();

            Console.WriteLine("Removing subscription...");

            string subscriptionID = "sub_569df922b4506cd73030";
            bool reply = susbscriptionService.Remove(subscriptionID);

            Console.WriteLine("Result remove:" + reply);
            Console.Read();
        }
    }
}

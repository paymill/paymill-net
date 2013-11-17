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
    public static class SubscriptionSamples
    {
        public static void GetSubscriptions()
        {
            Paymill.ApiKey = Properties.Settings.Default.ApiKey;
            Paymill.ApiUrl = Properties.Settings.Default.ApiUrl;
            SubscriptionService susbscriptionService = Paymill.GetService<SubscriptionService>();

            Console.WriteLine("Waiting request list subscriptions...");
            List<Subscription> lstSubscriptions = susbscriptionService.GetSubscriptions();

            foreach (Subscription s in lstSubscriptions)
            {
                Utilities.printObject(s);
            }

            Console.Read();
        }
        public static void GetSubscriptionsWithParameters()
        {
            Paymill.ApiKey = Properties.Settings.Default.ApiKey;
            Paymill.ApiUrl = Properties.Settings.Default.ApiUrl;
            SubscriptionService susbscriptionService = Paymill.GetService<SubscriptionService>();

            Console.WriteLine("Waiting request list subscriptions with parameters...");

            Filter filter = new Filter();
            filter.Add("count", 1); //OK
            filter.Add("offset", 2); //OK
            filter.Add("offer", "offer_32008ddd39954e71ed48"); //KO
 
            List<Subscription> lstSubscriptions = susbscriptionService.GetSubscriptions(filter);

            foreach (Subscription s in lstSubscriptions)
            {
                Utilities.printObject(s);
            }

            Console.Read();
        }
       
        private static Subscription createSubsription()
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
            Payment payment = paymentService.Create(token);
            subscription.Payment = payment;
            return susbscriptionService.CreateSubscription(subscription);
        
        }
        public static void AddSubscription()
        {
            Paymill.ApiKey = Properties.Settings.Default.ApiKey;
            Paymill.ApiUrl = Properties.Settings.Default.ApiUrl;
            SubscriptionService susbscriptionService = Paymill.GetService<SubscriptionService>();

            Subscription subscription = new Subscription();
            subscription.Client = new Client() { Id = "client_bbe895116de80b6141fd" };
            subscription.Offer = new Offer() { Id = "offer_32008ddd39954e71ed48" };
            subscription.Payment = new Payment() { Id = "pay_81ec02206e9b9c587513" };
             Subscription newSubscription = susbscriptionService.CreateSubscription(subscription);

            Utilities.printObject(newSubscription);
            Console.Read();
        }
        public static void GetSubscription()
        {
            Paymill.ApiKey = Properties.Settings.Default.ApiKey;
            Paymill.ApiUrl = Properties.Settings.Default.ApiUrl;
            SubscriptionService susbscriptionService = Paymill.GetService<SubscriptionService>();

            Console.WriteLine("Request subscription...");
            string subscriptionID = "sub_e77d3332e456674101ad";
            Subscription subscription = susbscriptionService.GetSubscription(subscriptionID);
            Utilities.printObject(subscription);
            Console.Read();
        }
        public static void UpdateSubscription()
        {
            Paymill.ApiKey = Properties.Settings.Default.ApiKey;
            Paymill.ApiUrl = Properties.Settings.Default.ApiUrl;
            SubscriptionService susbscriptionService = Paymill.GetService<SubscriptionService>();

            Subscription subscription = createSubsription();
            subscription.Cancel_At_Period_End = true;

            subscription.Id = "sub_569df922b4506cd73030";

            Subscription updatedSubscription = susbscriptionService.UpdateSubscription(subscription);

            Console.WriteLine("SubscriptionID:" + updatedSubscription.Id);
            Console.Read();
        }
        public static void RemoveSubscription()
        {
            // se elimina correctamente pero el json de respuesta no devuelve vacio 

            Paymill.ApiKey = Properties.Settings.Default.ApiKey;
            Paymill.ApiUrl = Properties.Settings.Default.ApiUrl;
            SubscriptionService susbscriptionService = Paymill.GetService<SubscriptionService>();

            Console.WriteLine("Removing subscription...");

            string subscriptionID = "sub_569df922b4506cd73030";
            bool reply = susbscriptionService.RemoveSubscription(subscriptionID);

            Console.WriteLine("Result remove:" + reply);
            Console.Read();
        }
    }
}

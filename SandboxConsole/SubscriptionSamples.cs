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
        static void getSubscriptionsWithParameters()
        {
            Paymill.ApiKey = Properties.Settings.Default.ApiKey;
            Paymill.ApiUrl = Properties.Settings.Default.ApiUrl;
            SubscriptionService susbscriptionService = Paymill.GetService<SubscriptionService>();

            Console.WriteLine("Waiting request list subscriptions with parameters...");

            Filter filter = new Filter();
            filter.Add("count", 1); //OK
            filter.Add("offset", 2); //OK
            filter.Add("offer", "offer_32008ddd39954e71ed48"); //KO
            //filter.Add("canceled_at", 495); //KO
            //filter.Add("created_at", 1353194860); //KO

            List<Subscription> lstSubscriptions = susbscriptionService.GetSubscriptionsByFilter(filter);

            foreach (Subscription s in lstSubscriptions)
            {
                Utilities.printObject(s);
            }

            Console.Read();
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
            // TODO: get result body
            Subscription newSubscription = susbscriptionService.Create(subscription);

            Utilities.printObject(newSubscription);
            Console.Read();
        }
        static void getSubscription()
        {
            Paymill.ApiKey = Properties.Settings.Default.ApiKey;
            Paymill.ApiUrl = Properties.Settings.Default.ApiUrl;
            SubscriptionService susbscriptionService = Paymill.GetService<SubscriptionService>();

            Console.WriteLine("Request subscription...");
            string subscriptionID = "sub_e77d3332e456674101ad";
            Subscription subscription = susbscriptionService.Get(subscriptionID);
            Utilities.printObject(subscription);
            Console.Read();
        }
        static void updateSubscription()
        {
            Paymill.ApiKey = Properties.Settings.Default.ApiKey;
            Paymill.ApiUrl = Properties.Settings.Default.ApiUrl;
            SubscriptionService susbscriptionService = Paymill.GetService<SubscriptionService>();

            Subscription subscription = new Subscription();
            subscription.Cancel_At_Period_End = true;
            subscription.Id = "sub_569df922b4506cd73030";

            Subscription updatedSubscription = susbscriptionService.Update(subscription);

            Console.WriteLine("SubscriptionID:" + updatedSubscription.Id);
            Console.Read();
        }
        static void removeSubscription()
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

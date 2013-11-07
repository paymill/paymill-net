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
    public static class ОfferSamples
    {
        public static void GetAllOffers()
        {
            Paymill.ApiKey = Properties.Settings.Default.ApiKey;
            Paymill.ApiUrl = Properties.Settings.Default.ApiUrl;
            OfferService offerService = Paymill.GetService<OfferService>();

            Console.WriteLine("Waiting request list offers...");
            List<Offer> lstOffers = offerService.GetOffers();

            foreach (Offer o in lstOffers)
            {
                Utilities.printObject(o);
            }

            Console.Read();
        }
        public static void GetOffersWithParameters()
        {
            Paymill.ApiKey = Properties.Settings.Default.ApiKey;
            Paymill.ApiUrl = Properties.Settings.Default.ApiUrl;
            OfferService offerService = Paymill.GetService<OfferService>();

            Console.WriteLine("Waiting request list offers with parameters...");

            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime();
            TimeSpan span = (new DateTime(2012, 11, 28, 18, 38, 33) - epoch);

            Filter filter = new Filter();
            filter.Add("count", 1); //OK
            filter.Add("offset", 2); //OK
            filter.Add("interval","MONTH"); //OK
            filter.Add("amount", 495); //OK
            //filter.Add("created_at", span.TotalSeconds.ToString()); //KO
            //filter.Add("trial_period_days", 5); //OK

            List<Offer> lstOffers = offerService.GetOffersByFilter(filter);

            foreach (Offer o in lstOffers)
            {
                Utilities.printObject(o);
            }

            Console.Read();
        }
        public static void CreateOffer()
        {
            Offer newOffer = createOffer();
            Utilities.printObject(newOffer);
            Console.Read();
        }

        private static Offer createOffer()
        {
            Paymill.ApiKey = Properties.Settings.Default.ApiKey;
            Paymill.ApiUrl = Properties.Settings.Default.ApiUrl;
            OfferService offerService = Paymill.GetService<OfferService>();

            Offer offer = new Offer();
            offer.Amount = 1500;
            offer.Currency = "EUR";
            offer.Interval = @"1 MONTH";
            offer.Name = "Test API";
            offer.Trial_Period_Days = 3;
            offer.Created_At = DateTime.Now;
            offer.Trial_Period_Days = 0;
            offer.Updated_At = DateTime.Now;
            offer.SubscriptionCount.Аctive = "3";
            offer.SubscriptionCount.Inactive = "0";
            Offer newOffer = offerService.Create(offer);
            return newOffer;
        }
        public static void GetOffer()
        {
            Paymill.ApiKey = Properties.Settings.Default.ApiKey;
            Paymill.ApiUrl = Properties.Settings.Default.ApiUrl;
            OfferService offerService = Paymill.GetService<OfferService>();

            Console.WriteLine("Request offer...");
            string offerID = "offer_6eea405f83d4d3098604";
            Offer offer = offerService.Get(offerID);

            Console.WriteLine("OfferID:" + offer.Id);
            Console.WriteLine("Created at:" + offer.Created_At.ToShortDateString());
            Console.Read();
        }
        public static void UpdateOffer()
        {
            Paymill.ApiKey = Properties.Settings.Default.ApiKey;
            Paymill.ApiUrl = Properties.Settings.Default.ApiUrl;
            OfferService offerService = Paymill.GetService<OfferService>();

            Offer offer = new Offer();
            offer.Name = "Oferta 48";
            offer.Id = "offer_6eea405f83d4d3098604";

            Offer updatedOffer = offerService.Update(offer);

            Console.WriteLine("OfferID:" + updatedOffer.Id);
            Console.Read();
        }
        public static void RemoveOffer()
        {
            Paymill.ApiKey = Properties.Settings.Default.ApiKey;
            Paymill.ApiUrl = Properties.Settings.Default.ApiUrl;
            OfferService offerService = Paymill.GetService<OfferService>();

            Console.WriteLine("Removing offer...");

            string offerID = "offer_6eea405f83d4d3098604";
            bool reply = offerService.Remove(offerID);

            Console.WriteLine("Result remove:" + reply);
            Console.Read();
        }
    }
}

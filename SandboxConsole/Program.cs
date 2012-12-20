using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PaymillWrapper;
using PaymillWrapper.Models;
using PaymillWrapper.Net;
using PaymillWrapper.Service;

namespace SandboxConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            getClientsWithParameters();

            Console.ReadLine();
        }

        // payments
        static void getPayments()
        {
            Paymill.ApiKey = Properties.Settings.Default.ApiKey;
            Paymill.ApiUrl = Properties.Settings.Default.ApiUrl;
            PaymentService paymentService = Paymill.GetService<PaymentService>();

            // list payments
            Console.WriteLine("Waiting request list payments...");

            List<Payment> lstPayments = paymentService.GetPayments();

            foreach (Payment payment in lstPayments)
            {
                Console.WriteLine(String.Format("PaymentID:{0}", payment.Id)); 
            }

            Console.Read();
        }
        static void getPaymentsWithParameters()
        {
            Paymill.ApiKey = Properties.Settings.Default.ApiKey;
            Paymill.ApiUrl = Properties.Settings.Default.ApiUrl;
            PaymentService paymentService = Paymill.GetService<PaymentService>();

            // list payments
            Console.WriteLine("Waiting request list payments with parameters...");

            Filter filter = new Filter();
            filter.Add("count", 5);
            filter.Add("offset", 41);

            List<Payment> lstPayments = paymentService.GetPayments(filter);

            foreach (Payment payment in lstPayments)
            {
                Console.WriteLine(String.Format("PaymentID:{0}", payment.Id));
            }

            Console.Read();
        }
        static void addCreditCardPayment()
        {
            Paymill.ApiKey = Properties.Settings.Default.ApiKey;
            Paymill.ApiUrl = Properties.Settings.Default.ApiUrl;
            PaymentService paymentService = Paymill.GetService<PaymentService>();

            Payment payment = new Payment();
            payment.Token = "098f6bcd4621d373cade4e832627b4f6";
            
            Payment newPayment = paymentService.AddPayment(payment);

            Console.WriteLine("PaymentID:" + newPayment.Id);
            Console.Read();
        }
        static void addCreditCardPaymentWithClient()
        {
            Paymill.ApiKey = Properties.Settings.Default.ApiKey;
            Paymill.ApiUrl = Properties.Settings.Default.ApiUrl;
            PaymentService paymentService = Paymill.GetService<PaymentService>();

            Payment payment = new Payment();
            payment.Token = "098f6bcd4621d373cade4e832627b4f6";
            payment.Client = "client_ad591663d69051d306a8";
            
            Payment newPayment = paymentService.AddPayment(payment);

            Console.WriteLine("PaymentID:" + newPayment.Id);
            Console.WriteLine("Created at:" + newPayment.Created_At);
            Console.Read();
        }
        static void addDebitPayment()
        {
            Paymill.ApiKey = Properties.Settings.Default.ApiKey;
            Paymill.ApiUrl = Properties.Settings.Default.ApiUrl;
            PaymentService paymentService = Paymill.GetService<PaymentService>();

            Payment payment = new Payment();
            payment.Type = Payment.TypePayment.DEBIT;
            payment.Code = "86055500";
            payment.Account = "1234512345";
            payment.Holder = "Max Mustermann";

            Payment newPayment = paymentService.AddPayment(payment);

            Console.WriteLine("PaymentID:" + newPayment.Id);
            Console.WriteLine("Created at:" + newPayment.Created_At);
            Console.Read();
        }
        static void addDebitPaymentWithClient()
        {
            Paymill.ApiKey = Properties.Settings.Default.ApiKey;
            Paymill.ApiUrl = Properties.Settings.Default.ApiUrl;
            PaymentService paymentService = Paymill.GetService<PaymentService>();

            Payment payment = new Payment();
            payment.Type = Payment.TypePayment.DEBIT;
            payment.Code = "86055500";
            payment.Account = "1234512345";
            payment.Holder = "Max Mustermann";
            payment.Client = "client_bbe895116de80b6141fd";

            Payment newPayment = paymentService.AddPayment(payment);

            Console.WriteLine("PaymentID:" + newPayment.Id);
            Console.WriteLine("Created at:" + newPayment.Created_At);
            Console.Read();
        }
        static void getPayment()
        {
            Paymill.ApiKey = Properties.Settings.Default.ApiKey;
            Paymill.ApiUrl = Properties.Settings.Default.ApiUrl;
            PaymentService paymentService = Paymill.GetService<PaymentService>();

            string paymentID = "pay_4c159fe95d3be503778a";
            Payment payment = paymentService.GetPayment(paymentID);

            Console.WriteLine("PaymentID:" + payment.Id);
            Console.WriteLine("PaymentID:" + payment.Created_At.ToShortDateString());
            Console.Read();
        }
        static void removePayment()
        {
            Paymill.ApiKey = Properties.Settings.Default.ApiKey;
            Paymill.ApiUrl = Properties.Settings.Default.ApiUrl;
            PaymentService paymentService = Paymill.GetService<PaymentService>();

            string paymentID = "pay_640be2127169cea1d375";
            bool reply = paymentService.RemovePayment(paymentID);

            Console.WriteLine("Result remove:" + reply);
            Console.Read();
        }

        // transactions
        static void getTransactions()
        {
            Paymill.ApiKey = Properties.Settings.Default.ApiKey;
            Paymill.ApiUrl = Properties.Settings.Default.ApiUrl;
            TransactionService transactionService = Paymill.GetService<TransactionService>();

            Console.WriteLine("Waiting request list transactions...");
            List<Transaction> lstTransactions = transactionService.GetTransactions();

            foreach (Transaction transaction in lstTransactions)
            {
                Console.WriteLine(String.Format("TransactionID:{0}", transaction.Id));
            }

            Console.Read();
        }
        static void getTransactionsWithParameters()
        {
            Paymill.ApiKey = Properties.Settings.Default.ApiKey;
            Paymill.ApiUrl = Properties.Settings.Default.ApiUrl;
            TransactionService transactionService = Paymill.GetService<TransactionService>();

            Console.WriteLine("Waiting request list transactions with parameters...");

            Filter filter = new Filter();
            filter.Add("count", 1);
            filter.Add("offset", 2);

            List<Transaction> lstTransactions = transactionService.GetTransactions(filter);

            foreach (Transaction transaction in lstTransactions)
            {
                Console.WriteLine(String.Format("TransactionID:{0}", transaction.Id));
            }

            Console.Read();
        }
        static void addTransaction()
        {
            Paymill.ApiKey = Properties.Settings.Default.ApiKey;
            Paymill.ApiUrl = Properties.Settings.Default.ApiUrl;
            TransactionService transactionService = Paymill.GetService<TransactionService>();

            Transaction transaction = new Transaction();
            transaction.Token = "098f6bcd4621d373cade4e832627b4f6";
            transaction.Amount = 3500;
            transaction.Currency = "EUR";
            transaction.Description = "Prueba desde API c#";

            Transaction newTransaction = transactionService.AddTransaction(transaction);

            Console.WriteLine("TransactionID:" + newTransaction.Id);
            Console.Read();
        }
        static void addTransactionWithPayment()
        {
            Paymill.ApiKey = Properties.Settings.Default.ApiKey;
            Paymill.ApiUrl = Properties.Settings.Default.ApiUrl;
            TransactionService transactionService = Paymill.GetService<TransactionService>();

            Transaction transaction = new Transaction();
            transaction.Amount = 3500;
            transaction.Currency = "EUR";
            transaction.Description = "Prueba desde API c#";
            transaction.Payment = new Payment() { Id = "pay_81ec02206e9b9c587513" };

            Transaction newTransaction = transactionService.AddTransaction(transaction);

            Console.WriteLine("TransactionID:" + newTransaction.Id);
            Console.Read();
        }
        static void addTransactionWithClient()
        {
            // Hay que depurar esta función, no funciona bien cuando se pasa el identificador del cliente, 
            // está creando un nuevo cliente aunque le pasemos el identificador de uno ya existente

            Paymill.ApiKey = Properties.Settings.Default.ApiKey;
            Paymill.ApiUrl = Properties.Settings.Default.ApiUrl;
            TransactionService transactionService = Paymill.GetService<TransactionService>();

            Transaction transaction = new Transaction();

            transaction.Amount = 8000;
            transaction.Currency = "EUR";
            transaction.Description = "Transacción con cliente";
            transaction.Payment = new Payment() { Id = "pay_c08f1f94754b93f46ac3" };
            transaction.Client = new Client() { Id = "client_ad591663d69051d306a8" };

            Transaction newTransaction = transactionService.AddTransaction(transaction);

            Console.WriteLine("TransactionID:" + newTransaction.Id);
            Console.Read();
        }
        static void getTransaction()
        {
            Paymill.ApiKey = Properties.Settings.Default.ApiKey;
            Paymill.ApiUrl = Properties.Settings.Default.ApiUrl;
            TransactionService transactionService = Paymill.GetService<TransactionService>();

            Console.WriteLine("Solicitando transaction...");
            string transactionID = "tran_9255ee9ad5a7f2999625";
            Transaction transaction = transactionService.GetTransaction(transactionID);

            Console.WriteLine("TransactionID:" + transaction.Id);
            Console.WriteLine("Created at:" + transaction.Created_At.ToShortDateString());
            Console.Read();
        }

        // preauthorizations
        static void getPreauthorizations()
        {
            Paymill.ApiKey = Properties.Settings.Default.ApiKey;
            Paymill.ApiUrl = Properties.Settings.Default.ApiUrl;
            PreauthorizationService preauthorizationService = Paymill.GetService<PreauthorizationService>();

            Console.WriteLine("Waiting request list preauthorizations...");
            List<Preauthorization> lstPreauthorizations = preauthorizationService.GetPreauthorizations();

            foreach (Preauthorization preauthorization in lstPreauthorizations)
            {
                Console.WriteLine(String.Format("PreauthorizationID:{0}", preauthorization.Id));
            }

            Console.Read();
        }
        static void getPreauthorizationsWithParameters()
        {
            Paymill.ApiKey = Properties.Settings.Default.ApiKey;
            Paymill.ApiUrl = Properties.Settings.Default.ApiUrl;
            PreauthorizationService preauthorizationService = Paymill.GetService<PreauthorizationService>();

            Console.WriteLine("Waiting request list preauthorizations...");

            Filter filter = new Filter();
            filter.Add("count", 1);
            filter.Add("offset", 2);

            List<Preauthorization> lstPreauthorizations = preauthorizationService.GetPreauthorizations(filter);

            foreach (Preauthorization preauthorization in lstPreauthorizations)
            {
                Console.WriteLine(String.Format("PreauthorizationID:{0}", preauthorization.Id));
            }

            Console.Read();
        }
        static void addPreauthorization()
        {
            Paymill.ApiKey = Properties.Settings.Default.ApiKey;
            Paymill.ApiUrl = Properties.Settings.Default.ApiUrl;
            PreauthorizationService preauthorizationService = Paymill.GetService<PreauthorizationService>();

            Preauthorization preauthorization = new Preauthorization();
            preauthorization.Amount = 3500;
            preauthorization.Currency = "EUR";
            //preauthorization.Token = "098f6bcd4621d373cade4e832627b4f6";
            preauthorization.Payment = new Payment() { Id = "pay_4c159fe95d3be503778a" };

            Preauthorization newPreauthorization = preauthorizationService.AddPreauthorization(preauthorization);

            Console.WriteLine("PreauthorizationID:" + newPreauthorization.Id);
            Console.Read();
        }
        static void getPreauthorization()
        {
            Paymill.ApiKey = Properties.Settings.Default.ApiKey;
            Paymill.ApiUrl = Properties.Settings.Default.ApiUrl;
            PreauthorizationService preauthorizationService = Paymill.GetService<PreauthorizationService>();

            Console.WriteLine("Solicitando preauthorization...");
            string preauthorizationID = "preauth_96fe414f466f91ddb266";
            Preauthorization preauthorization = preauthorizationService.GetPreauthorization(preauthorizationID);

            Console.WriteLine("PreauthorizationID:" + preauthorization.Id);
            Console.WriteLine("Created at:" + preauthorization.Created_At.ToShortDateString());
            Console.Read();
        }

        // refunds
        static void getRefunds()
        {
            Paymill.ApiKey = Properties.Settings.Default.ApiKey;
            Paymill.ApiUrl = Properties.Settings.Default.ApiUrl;
            RefundService refundService = Paymill.GetService<RefundService>();

            Console.WriteLine("Waiting request list refunds...");
            List<Refund> lstRefunds = refundService.GetRefunds();

            foreach (Refund refund in lstRefunds)
            {
                Console.WriteLine(String.Format("RefundID:{0}", refund.Id));
            }

            Console.Read();
        }
        static void getRefundsWithParameters()
        {
            // probar los parametros, no funciona bien
            // transaction es ok
            // client no funciona
            // amount es ok
            // created_at no funciona
            // count es ok

            Paymill.ApiKey = Properties.Settings.Default.ApiKey;
            Paymill.ApiUrl = Properties.Settings.Default.ApiUrl;
            RefundService refundService = Paymill.GetService<RefundService>();

            Console.WriteLine("Waiting request list refunds with parameters...");

            Filter filter = new Filter();
            filter.Add("count", 5);

            List<Refund> lstRefunds = refundService.GetRefunds(filter);

            foreach (Refund refund in lstRefunds)
            {
                Console.WriteLine(String.Format("RefundID:{0}", refund.Id));
            }

            Console.Read();
        }
        static void addRefund()
        {
            // la documentación de la API está mal, devuelve un objeto Refund en vez de Transaction

            Paymill.ApiKey = Properties.Settings.Default.ApiKey;
            Paymill.ApiUrl = Properties.Settings.Default.ApiUrl;
            RefundService refundService = Paymill.GetService<RefundService>();

            Refund refund = new Refund();
            refund.Amount = 500;
            refund.Description = "Prueba desde API c#";
            refund.Transaction = new Transaction() { Id = "tran_a7c93a1e5b431b52c0f0" };

            Refund newRefund = refundService.AddRefund(refund);

            Console.WriteLine("RefundID:" + newRefund.Id);
            Console.Read();
        }
        static void getRefund()
        {
            Paymill.ApiKey = Properties.Settings.Default.ApiKey;
            Paymill.ApiUrl = Properties.Settings.Default.ApiUrl;
            RefundService refundService = Paymill.GetService<RefundService>();

            Console.WriteLine("Request refund...");
            string refundID = "refund_53860aa0e514d4913aad";
            Refund refund = refundService.GetRefund(refundID);

            Console.WriteLine("RefundID:" + refund.Id);
            Console.WriteLine("Created at:" + refund.Created_At.ToShortDateString());
            Console.Read();
        }

        // clients
        static void getClients()
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
        static void getClientsWithParameters()
        {
            Paymill.ApiKey = Properties.Settings.Default.ApiKey;
            Paymill.ApiUrl = Properties.Settings.Default.ApiUrl;
            ClientService clientService = Paymill.GetService<ClientService>();

            Console.WriteLine("Waiting request list clients with parameters...");

            Filter filter = new Filter();
            //filter.Add("email", "javicantos22@hotmail.es"); //OK
            //filter.Add("creditcard", "pay_f95c7d70c6ad8da339e5"); //KO
            filter.Add("created_at", 1352930695); //KO

            List<Client> lstClients = clientService.GetClients(filter);

            foreach (Client c in lstClients)
            {
                Console.WriteLine(String.Format("ClientID:{0}", c.Id));
            }

            Console.Read();
        }
        static void addClient()
        {
            Paymill.ApiKey = Properties.Settings.Default.ApiKey;
            Paymill.ApiUrl = Properties.Settings.Default.ApiUrl;
            ClientService clientService = Paymill.GetService<ClientService>();

            Client c = new Client();
            c.Description = "Prueba API";
            c.Email = "javicantos22@hotmail.es";

            Client newClient = clientService.AddClient(c);

            Console.WriteLine("ClientID:" + newClient.Id);
            Console.Read();
        }
        static void getClient()
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
        static void updateClient()
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
        static void removeClient()
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
        }

        // offers
        static void getOffers()
        {
            Paymill.ApiKey = Properties.Settings.Default.ApiKey;
            Paymill.ApiUrl = Properties.Settings.Default.ApiUrl;
            OfferService offerService = Paymill.GetService<OfferService>();

            Console.WriteLine("Waiting request list offers...");
            List<Offer> lstOffers = offerService.GetOffers();

            foreach (Offer o in lstOffers)
            {
                Console.WriteLine(String.Format("OfferID:{0}", o.Id));
            }

            Console.Read();
        }
        static void getOffersWithParameters()
        {
            Paymill.ApiKey = Properties.Settings.Default.ApiKey;
            Paymill.ApiUrl = Properties.Settings.Default.ApiUrl;
            OfferService offerService = Paymill.GetService<OfferService>();

            Console.WriteLine("Waiting request list offers with parameters...");

            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime();
            TimeSpan span = (new DateTime(2012,11,28,18,38,33) - epoch);

            Filter filter = new Filter();
            filter.Add("count", 1); //OK
            filter.Add("offset", 2); //OK
            //filter.Add("interval","month"); //OK
            //filter.Add("amount", 495); //OK
            //filter.Add("created_at", span.TotalSeconds.ToString()); //KO
            //filter.Add("trial_period_days", 5); //OK

            List<Offer> lstOffers = offerService.GetOffers(filter);

            foreach (Offer o in lstOffers)
            {
                Console.WriteLine(String.Format("OfferID:{0}", o.Id));
            }

            Console.Read();
        }
        static void addOffer()
        {
            Paymill.ApiKey = Properties.Settings.Default.ApiKey;
            Paymill.ApiUrl = Properties.Settings.Default.ApiUrl;
            OfferService offerService = Paymill.GetService<OfferService>();

            Offer offer = new Offer();
            offer.Amount = 1500;
            offer.Currency = "eur";
            offer.Interval = Offer.TypeInterval.MONTH;
            offer.Name = "Prueba API";
            offer.Trial_Period_Days = 3;

            Offer newOffer = offerService.AddOffer(offer);

            Console.WriteLine("OfferID:" + newOffer.Id);
            Console.Read();
        }
        static void getOffer()
        {
            Paymill.ApiKey = Properties.Settings.Default.ApiKey;
            Paymill.ApiUrl = Properties.Settings.Default.ApiUrl;
            OfferService offerService = Paymill.GetService<OfferService>();

            Console.WriteLine("Request offer...");
            string offerID = "offer_6eea405f83d4d3098604";
            Offer offer = offerService.GetOffer(offerID);

            Console.WriteLine("OfferID:" + offer.Id);
            Console.WriteLine("Created at:" + offer.Created_At.ToShortDateString());
            Console.Read();
        }
        static void updateOffer()
        {
            Paymill.ApiKey = Properties.Settings.Default.ApiKey;
            Paymill.ApiUrl = Properties.Settings.Default.ApiUrl;
            OfferService offerService = Paymill.GetService<OfferService>();

            Offer offer = new Offer();
            offer.Name = "Oferta 48";
            offer.Id = "offer_6eea405f83d4d3098604";

            Offer updatedOffer = offerService.UpdateOffer(offer);

            Console.WriteLine("OfferID:" + updatedOffer.Id);
            Console.Read();
        }
        static void removeOffer()
        {
            Paymill.ApiKey = Properties.Settings.Default.ApiKey;
            Paymill.ApiUrl = Properties.Settings.Default.ApiUrl;
            OfferService offerService = Paymill.GetService<OfferService>();

            Console.WriteLine("Removing offer...");

            string offerID = "offer_6eea405f83d4d3098604";
            bool reply = offerService.RemoveOffer(offerID);

            Console.WriteLine("Result remove:" + reply);
            Console.Read();
        }

        // subscriptions
        static void getSubscriptions()
        {
            Paymill.ApiKey = Properties.Settings.Default.ApiKey;
            Paymill.ApiUrl = Properties.Settings.Default.ApiUrl;
            SubscriptionService susbscriptionService = Paymill.GetService<SubscriptionService>();

            Console.WriteLine("Waiting request list subscriptions...");
            List<Subscription> lstSubscriptions = susbscriptionService.GetSubscriptions();

            foreach (Subscription s in lstSubscriptions)
            {
                Console.WriteLine(String.Format("SubscriptionID:{0}", s.Id));
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
            //filter.Add("offer", "offer_32008ddd39954e71ed48"); //KO
            //filter.Add("canceled_at", 495); //KO
            //filter.Add("created_at", 1353194860); //KO
            
            List<Subscription> lstSubscriptions = susbscriptionService.GetSubscriptions(filter);

            foreach (Subscription s in lstSubscriptions)
            {
                Console.WriteLine(String.Format("SubscriptionID:{0}", s.Id));
            }

            Console.Read();
        }
        static void addSubscription()
        {
            Paymill.ApiKey = Properties.Settings.Default.ApiKey;
            Paymill.ApiUrl = Properties.Settings.Default.ApiUrl;
            SubscriptionService susbscriptionService = Paymill.GetService<SubscriptionService>();

            Subscription subscription = new Subscription();
            subscription.Client = new Client() { Id = "client_bbe895116de80b6141fd" };
            subscription.Offer = new Offer() { Id = "offer_32008ddd39954e71ed48" };
            subscription.Payment = new Payment() { Id = "pay_81ec02206e9b9c587513" };

            Subscription newSubscription = susbscriptionService.AddSubscription(subscription);

            Console.WriteLine("SubscriptionID:" + newSubscription.Id);
            Console.Read();
        }
        static void getSubscription()
        {
            Paymill.ApiKey = Properties.Settings.Default.ApiKey;
            Paymill.ApiUrl = Properties.Settings.Default.ApiUrl;
            SubscriptionService susbscriptionService = Paymill.GetService<SubscriptionService>();

            Console.WriteLine("Request subscription...");
            string subscriptionID = "sub_e77d3332e456674101ad";
            Subscription subscription = susbscriptionService.GetSubscription(subscriptionID);

            Console.WriteLine("SubscriptionID:" + subscription.Id);
            Console.WriteLine("Created at:" + subscription.Created_At.ToShortDateString());
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

            Subscription updatedSubscription = susbscriptionService.UpdateSubscription(subscription);

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
            bool reply = susbscriptionService.RemoveSubscription(subscriptionID);

            Console.WriteLine("Result remove:" + reply);
            Console.Read();
        }
    }

}

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
    public static class PaymentSamples
    {
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

            Utilities.printObject(newPayment);
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
        public static void AddDebitPayment()
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
    }
}

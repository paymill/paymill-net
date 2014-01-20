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
    public static class PaymentSamples
    {
        public static void GetPayments()
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
        public static void GetPaymentsWithParameters()
        {
            Paymill.ApiKey = Properties.Settings.Default.ApiKey;
            Paymill.ApiUrl = Properties.Settings.Default.ApiUrl;
            PaymentService paymentService = Paymill.GetService<PaymentService>();

            // list payments
            Console.WriteLine("Waiting request list payments with parameters...");

            Filter filter = new Filter();
            filter.Add("count", 5);
            filter.Add("offset", 41);

            List<Payment> lstPayments = paymentService.GetPaymentsByFilter(filter);

            foreach (Payment payment in lstPayments)
            {
                Console.WriteLine(String.Format("PaymentID:{0}", payment.Id));
            }

            Console.Read();
        }

        public static void CreatePayment()
        {
            Paymill.ApiKey = Properties.Settings.Default.ApiKey;
            Paymill.ApiUrl = Properties.Settings.Default.ApiUrl;
            PaymentService paymentService = Paymill.GetService<PaymentService>();

            string token = "098f6bcd4621d373cade4e832627b4f6";
            Payment payment = paymentService.Create(token);

            Utilities.printObject(payment);
            Console.Read();
        }
        public static void GetPayment()
        {
            Paymill.ApiKey = Properties.Settings.Default.ApiKey;
            Paymill.ApiUrl = Properties.Settings.Default.ApiUrl;
            PaymentService paymentService = Paymill.GetService<PaymentService>();

            string paymentID = "pay_4c159fe95d3be503778a";
            Payment payment = paymentService.Get(paymentID);

            Console.WriteLine("PaymentID:" + payment.Id);
            Console.WriteLine("PaymentID:" + payment.Created_At.ToShortDateString());
            Console.Read();
        }
        public static void RemovePayment()
        {
            Paymill.ApiKey = Properties.Settings.Default.ApiKey;
            Paymill.ApiUrl = Properties.Settings.Default.ApiUrl;
            PaymentService paymentService = Paymill.GetService<PaymentService>();

            string paymentID = "pay_640be2127169cea1d375";
            bool reply = paymentService.Remove(paymentID);

            Console.WriteLine("Result remove:" + reply);
            Console.Read();
        }
    }
}

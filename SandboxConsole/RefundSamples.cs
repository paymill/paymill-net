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
    public static class RefundSamples
    {
        public static void GetRefunds()
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
        public static void GetRefundsWithParameters()
        {
 
            Paymill.ApiKey = Properties.Settings.Default.ApiKey;
            Paymill.ApiUrl = Properties.Settings.Default.ApiUrl;
            RefundService refundService = Paymill.GetService<RefundService>();

            Console.WriteLine("Waiting request list refunds with parameters...");

            Filter filter = new Filter();
            filter.Add("count", 5);

            List<Refund> lstRefunds = refundService.GetRefundsByFilter(filter);

            foreach (Refund refund in lstRefunds)
            {
                Console.WriteLine(String.Format("RefundID:{0}", refund.Id));
            }

            Console.Read();
        }
        public static void AddRefund()
        {
            // la documentación de la API está mal, devuelve un objeto Refund en vez de Transaction

            Paymill.ApiKey = Properties.Settings.Default.ApiKey;
            Paymill.ApiUrl = Properties.Settings.Default.ApiUrl;
            RefundService refundService = Paymill.GetService<RefundService>();

            Refund refund = new Refund();
            refund.Amount = 500;
            refund.Description = "Test desde API c#";
            refund.Transaction = new Transaction() { Id = "tran_a7c93a1e5b431b52c0f0" };

            Refund newRefund = refundService.Create(refund);

            Console.WriteLine("RefundID:" + newRefund.Id);
            Console.Read();
        }
        public static void GetRefund()
        {
            Paymill.ApiKey = Properties.Settings.Default.ApiKey;
            Paymill.ApiUrl = Properties.Settings.Default.ApiUrl;
            RefundService refundService = Paymill.GetService<RefundService>();

            Console.WriteLine("Request refund...");
            string refundID = "refund_53860aa0e514d4913aad";
            Refund refund = refundService.Get(refundID);

            Console.WriteLine("RefundID:" + refund.Id);
            Console.WriteLine("Created at:" + refund.Created_At.ToShortDateString());
            Console.Read();
        }
    }
}

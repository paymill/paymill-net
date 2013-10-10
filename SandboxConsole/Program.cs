using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;


namespace SandboxConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var payment = PaymentSamples.GetPayments();
            (new ТransactionStamples()).AddTransactionWithClient(payment.First().Id, payment.First().Client);
            Console.ReadLine();
        }
     
    }

}
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
            PaymentSamples.AddDebitPayment();
            //addDebitPaymentWithClient();
            // removeClient();
           // getOffer();
            // updateOffer()
           // removeOffer();
           // addSubscription();
           // updateSubscription();
          //  removeSubscription();
            Console.ReadLine();
        }
     
    }

}
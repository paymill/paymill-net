using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;



namespace SandboxConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            ТransactionStamples.AddTransactionWithPayment();
            Console.ReadLine();
        }
     
    }

}
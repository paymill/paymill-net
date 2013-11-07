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
    public static class ТransactionStamples
    {
        public static void GetTransactions()
        {
            Paymill.ApiKey = Properties.Settings.Default.ApiKey;
            Paymill.ApiUrl = Properties.Settings.Default.ApiUrl;
            TransactionService transactionService = Paymill.GetService<TransactionService>();

            Console.WriteLine("Waiting request list transactions...");
            List<Transaction> lstTransactions = transactionService.GetTransactions();

            foreach (Transaction transaction in lstTransactions)
            {
                Utilities.printObject(transaction);
            }

            Console.Read();
        }
        public static void GetTransactionsWithParameters()
        {
            Paymill.ApiKey = Properties.Settings.Default.ApiKey;
            Paymill.ApiUrl = Properties.Settings.Default.ApiUrl;
            TransactionService transactionService = Paymill.GetService<TransactionService>();

            Console.WriteLine("Waiting request list transactions with parameters...");

            Filter filter = new Filter();
            filter.Add("count", 1);
            filter.Add("offset", 2);

            List<Transaction> lstTransactions = transactionService.GetTransactionsByFilter(filter);

            foreach (Transaction transaction in lstTransactions)
            {
                Console.WriteLine(String.Format("TransactionID:{0}", transaction.Id));
            }

            Console.Read();
        }
        public static void AddTransaction()
        {
            Paymill.ApiKey = Properties.Settings.Default.ApiKey;
            Paymill.ApiUrl = Properties.Settings.Default.ApiUrl;
            TransactionService transactionService = Paymill.GetService<TransactionService>();

            Transaction transaction = new Transaction();
            transaction.Token = "098f6bcd4621d373cade4e832627b4f6";
            transaction.Amount = 3500;
            transaction.Currency = "EUR";
            transaction.Description = "Test desde API c#";

            Transaction newTransaction = transactionService.Create(transaction);

            Console.WriteLine("TransactionID:" + newTransaction.Id);
            Console.Read();
        }
        public static void AddTransactionWithPayment()
        {
            Paymill.ApiKey = Properties.Settings.Default.ApiKey;
            Paymill.ApiUrl = Properties.Settings.Default.ApiUrl;
            TransactionService transactionService = Paymill.GetService<TransactionService>();

            Transaction transaction = new Transaction();
            transaction.Amount = 3500;
            transaction.Currency = "EUR";
            transaction.Description = "Test desde API c#";
            transaction.Payment = new Payment() { Id = "pay_81ec02206e9b9c587513" };

            Transaction newTransaction = transactionService.Create(transaction);

            Console.WriteLine("TransactionID:" + newTransaction.Id);
            Console.Read();
        }
        public static void AddTransactionWithClient()
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

            Transaction newTransaction = transactionService.Create(transaction);

            Console.WriteLine("TransactionID:" + newTransaction.Id);
            Console.Read();
        }
        public static void GetTransaction()
        {
            Paymill.ApiKey = Properties.Settings.Default.ApiKey;
            Paymill.ApiUrl = Properties.Settings.Default.ApiUrl;
            TransactionService transactionService = Paymill.GetService<TransactionService>();

            Console.WriteLine("Solicitando transaction...");
            string transactionID = "tran_9255ee9ad5a7f2999625";
            Transaction transaction = transactionService.Get(transactionID);

            Console.WriteLine("TransactionID:" + transaction.Id);
            Console.WriteLine("Created at:" + transaction.Created_At.ToShortDateString());
            Console.Read();
        }

    }
}

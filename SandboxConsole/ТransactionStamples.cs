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
    public class ТransactionStamples
    {
        Paymill paymill = new Paymill(Properties.Settings.Default.ApiKey);
        public void GetTransactions()
        {
            IReadOnlyCollection<Transaction> lstTransactions = paymill.TransactionService.GetTransactions();
            Console.WriteLine("Waiting request list transactions...");
  
            foreach (Transaction transaction in lstTransactions)
            {
                Utilities.printObject(transaction);
            }

            Console.Read();
        }
        public void GetTransactionsWithParameters()
        {

            Filter filter = new Filter();
            filter.Add("count", 1);
            filter.Add("offset", 2);
            IReadOnlyCollection<Transaction> lstTransactions = paymill.TransactionService.GetTransactionsByFilter(filter);

            foreach (Transaction transaction in lstTransactions)
            {
                Console.WriteLine(String.Format("TransactionID:{0}", transaction.Id));
            }

            Console.Read();
        }
        public  void AddTransaction()
        {
            Transaction transaction = new Transaction();
            transaction.Token = "098f6bcd4621d373cade4e832627b4f6";
            transaction.Amount = 3500;
            transaction.Currency = "EUR";
            transaction.Description = "Test desde API c#";

            Transaction newTransaction = paymill.TransactionService.Create(transaction, null);
            Console.WriteLine("TransactionID:" + newTransaction.Id);
            Console.Read();
        }
        public  void AddTransactionWithPayment()
        {
            Transaction transaction = new Transaction();
            transaction.Amount = 3500;
            transaction.Currency = "EUR";
            transaction.Description = "Test desde API c#";
            transaction.Token = "098f6bcd4621d373cade4e832627b4f6";
            Fee fee = new Fee();
            fee.Amount = 320;
            fee.Payment = "pay_3af44644dd6d25c820a8";
            Transaction newTransaction = paymill.TransactionService.Create(transaction, fee);

            Console.WriteLine("TransactionID:" + newTransaction.Id);
            Console.Read();
        }
        public  void AddTransactionWithClient(String paymentId, String clientId)
        {
            Transaction transaction = new Transaction();
            transaction.Token = "098f6bcd4621d373cade4e832627b4f6";
            transaction.Amount = 8000;
            transaction.Currency = "EUR";
            transaction.Description = "Transacción con cliente";
            transaction.Payment = new Payment() { Id = paymentId };
            transaction.Client = new Client() { Id = clientId };

            Transaction newTransaction = paymill.TransactionService.Create(transaction, null);

            Console.WriteLine("TransactionID:" + newTransaction.Id);
            Console.Read();
        }
       
        public void GetTransaction()
        {
 
            Console.WriteLine("Solicitando transaction...");
            string transactionID = "tran_9255ee9ad5a7f2999625";
            Transaction transaction = paymill.TransactionService.Get(transactionID);

            Console.WriteLine("TransactionID:" + transaction.Id);
            Console.WriteLine("Created at:" + transaction.Created_At.ToShortDateString());
            Console.Read();
        }

    }
}

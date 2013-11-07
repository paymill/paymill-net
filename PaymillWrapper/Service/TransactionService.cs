using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PaymillWrapper.Models;
using PaymillWrapper.Net;

namespace PaymillWrapper.Service
{
    public class TransactionService : AbstractService<Transaction>
    {
        public TransactionService(HttpClientRest client):base(client)
        {
        }

        /// <summary>
        /// This function allows request a transaction list
        /// </summary>
        /// <returns>Returns a list transactions-object</returns>
        public List<Transaction> GetTransactions()
        {
            return getList<Transaction>(Resource.Transactions);
        }

        /// <summary>
        /// This function allows request a transaction list
        /// </summary>
        /// <param name="filter">Result filtered in the required way</param>
        /// <returns>Returns a list transaction-object</returns>
        public List<Transaction> GetTransactionsByFilter(Filter filter)
        {
            return getList<Transaction>(Resource.Transactions, filter);
        }

        /// <summary>
        /// This function creates a transaction object
        /// </summary>
        /// <param name="client">Object-transaction</param>
        /// <returns>New object-transaction just add</returns>
        public Transaction Create(Transaction transaction)
        {
            return create<Transaction>(
                Resource.Transactions,
                null,
                new URLEncoder().EncodeTransaction(transaction));
        }
        /// <summary>
        /// This function update a transaction object
        /// </summary>
        /// <param name="client">Object-transaction</param>
        /// <returns>New object-transaction just add</returns>
        public Transaction Update(Transaction transaction)
        {
            return update<Transaction>(
                Resource.Transactions,
                transaction,
                transaction.Id,
                new URLEncoder().EncodeTransaction(transaction));
        }
        /// <summary>
        /// To get the details of an existing transaction you’ll need to supply the transaction ID
        /// </summary>
        /// <param name="clientID">Client identifier</param>
        /// <returns>Client-object</returns>
        public Transaction Get(string transactionID)
        {
            return get<Transaction>(Resource.Transactions, transactionID);
        }
    }
}
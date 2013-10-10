
﻿using System.Net.Http;
using PaymillWrapper.Models;
using PaymillWrapper.Net;
using System.Collections.Generic;
namespace PaymillWrapper.Service{

/*

    public class TransactionService : AbstractService<Transaction>
    {
        public TransactionService(HttpClientRest client):base(client)
        {
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
                new URLEncoder().EncodeTransactionUpdate(transaction));
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
*/

    public class TransactionService : AbstractService<Transaction>, ICRService<Transaction>
    {
        public TransactionService(HttpClient client, string apiUrl)
            : base(Resource.Transactions, client, apiUrl)
        {
        }
        /// <summary>
        /// This function allows request a transaction list
        /// </summary>
        /// <returns>Returns a list transactions-object</returns>
        public IReadOnlyCollection<Transaction> GetTransactions()
        {
            return getList();
        }

        /// <summary>
        /// This function allows request a transaction list
        /// </summary>
        /// <param name="filter">Result filtered in the required way</param>
        /// <returns>Returns a list transaction-object</returns>
        public IReadOnlyCollection<Transaction> GetTransactionsByFilter(Filter filter)
        {
            return getList(filter);
        }

        /// <summary>
        /// This function creates a transaction object
        /// </summary>
        /// <param name="client">Object-transaction</param>
        /// <returns>New object-transaction just add</returns>
        public Transaction Create(Transaction transaction, Fee fee)
        {
            return Create(
                null,
                new UrlEncoder().EncodeTransaction(transaction, fee));
        }
        protected override string GetResourceId(Transaction obj)
        {
            return obj.Id;
        }

        protected override string GetEncodedUpdateParams(Transaction obj, UrlEncoder encoder)
        {
            return encoder.EncodeTransactionUpdate(obj);
        }
    }
}
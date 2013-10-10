using System.Net.Http;
using PaymillWrapper.Models;
using PaymillWrapper.Net;
using System.Collections.Generic;
using System;

namespace PaymillWrapper.Service
{
/*
    public class RefundService : AbstractService<Refund>
    {
        public RefundService(HttpClientRest client):base(client)
        {
        }


        /// <summary>
        /// To get the details of an object refund you’ll need to supply the refund ID
        /// </summary>
        /// <param name="clientID">Refund identifier</param>
        /// <returns>Refund-object</returns>
        public Refund Get(string refundID)
        {
            return get<Refund>(Resource.Refunds, refundID);
        }
*/
    public class RefundService : AbstractService<Refund>, ICRService<Refund>
    {
        public RefundService(HttpClient client, string apiUrl)
            : base(Resource.Refunds, client, apiUrl)
        {
        }
        /// <summary>
        /// This function allows request a refund list
        /// </summary>
        /// <returns>Returns a list refunds-object</returns>
        public IReadOnlyCollection<Refund> GetRefunds()
        {
            return getList();
        }

        /// <summary>
        /// This function allows request a refund list
        /// </summary>
        /// <param name="filter">Result filtered in the required way</param>
        /// <returns>Returns a list refund-object</returns>
        public IReadOnlyCollection<Refund> GetRefundsByFilter(Filter filter)
        {
            return getList(filter);
        }
        /// <summary>
        /// This function creates a refund object
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public Refund Create(Transaction transaction, int amount)
        {
            return Create(transaction.Id, amount, null);
        }
        /// <summary>
        /// This function creates a refund object
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="amount"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public Refund Create(Transaction transaction, int amount, String description)
        {
            return Create(transaction.Id, amount, description);
        }
        /// <summary>
        /// This function creates a refund object
        /// </summary>
        /// <param name="transactionId"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public Refund Create(String transactionId, int amount)
        {
            return Create(transactionId, amount, null);
        }
        /// <summary>
        /// This function creates a refund object
        /// </summary>
        /// <param name="transactionId"></param>
        /// <param name="amount"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public Refund Create(String transactionId, int amount, String description)
        {
            Refund refund = new Refund();
            refund.Amount = amount;
            refund.Description = description;
            refund.Transaction = new Transaction() { Id = transactionId };
            return Create(
              transactionId,
              new UrlEncoder().EncodeRefund(refund));
        }
        protected override string GetResourceId(Refund obj)
        {
            return obj.Id;
        }
        protected override string GetEncodedUpdateParams(Refund obj, UrlEncoder encoder)
        {
            throw new System.NotImplementedException();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PaymillWrapper.Models;
using PaymillWrapper.Net;

namespace PaymillWrapper.Service
{
    public class RefundService : AbstractService<Refund>
    {
        public RefundService(HttpClientRest client):base(client)
        {
        }

        /// <summary>
        /// This function allows request a refund list
        /// </summary>
        /// <returns>Returns a list refunds-object</returns>
        public List<Refund> GetRefunds()
        {
            return getList<Refund>(Resource.Refunds);
        }

        /// <summary>
        /// This function allows request a refund list
        /// </summary>
        /// <param name="filter">Result filtered in the required way</param>
        /// <returns>Returns a list refund-object</returns>
        public List<Refund> GetRefundsByFilter(Filter filter)
        {
            return getList<Refund>(Resource.Refunds, filter);
        }
        /// <summary>
        /// This function creates a refund object
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public Refund Create(Transaction transaction, int amount) {
		    return Create(transaction.Id, amount, null);
    	}
        /// <summary>
        /// This function creates a refund object
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="amount"></param>
        /// <param name="description"></param>
        /// <returns></returns>
	    public Refund Create(Transaction transaction, int amount, String description) {
	    	return Create(transaction.Id, amount, description);
	    }
        /// <summary>
        /// This function creates a refund object
        /// </summary>
        /// <param name="transactionId"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
	    public Refund Create(String transactionId, int amount) {
		    return Create(transactionId, amount, null);
	    }
        /// <summary>
        /// This function creates a refund object
        /// </summary>
        /// <param name="transactionId"></param>
        /// <param name="amount"></param>
        /// <param name="description"></param>
        /// <returns></returns>
	    public Refund Create(String transactionId, int amount, String description) {
		    Refund refund = new Refund();
		    refund.Amount = amount;
		    refund.Description = description;
            refund.Transaction = new Transaction() { Id = transactionId };
              return create<Refund>(
                Resource.Refunds,
                transactionId,
                new URLEncoder().EncodeRefund(refund));
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
    }
}
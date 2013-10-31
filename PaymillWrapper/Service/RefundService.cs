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
        public List<Refund> GetRefunds(Filter filter)
        {
            return getList<Refund>(Resource.Refunds, filter);
        }

        /// <summary>
        /// This function creates a refund object
        /// </summary>
        /// <param name="client">Object-refund</param>
        /// <returns>New object-refund just add</returns>
        public Refund CreateRefund(Refund refund)
        {
            return create<Refund>(
                Resource.Refunds,
                refund.Transaction.Id,
                new URLEncoder().EncodeRefund(refund));
        }

        /// <summary>
        /// To get the details of an object refund you’ll need to supply the refund ID
        /// </summary>
        /// <param name="clientID">Refund identifier</param>
        /// <returns>Refund-object</returns>
        public Refund GetRefund(string refundID)
        {
            return get<Refund>(Resource.Refunds, refundID);
        }
    }
}
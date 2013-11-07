using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PaymillWrapper.Models;
using PaymillWrapper.Net;

namespace PaymillWrapper.Service
{
    public class PreauthorizationService : AbstractService<Preauthorization>
    {
        public PreauthorizationService(HttpClientRest client)
            : base(client)
        {
        }

        /// <summary>
        /// This function allows request a preauthorization list
        /// </summary>
        /// <returns>Returns a list preauthorizations-object</returns>
        public List<Preauthorization> GetPreauthorizations()
        {
            return getList<Preauthorization>(Resource.Preauthorizations);
        }

        /// <summary>
        /// This function allows request a preauthorization list
        /// </summary>
        /// <param name="filter">Result filtered in the required way</param>
        /// <returns>Returns a list preauthorization-object</returns>
        public List<Preauthorization> GetPreauthorizationsByFilter(Filter filter)
        {
            return getList<Preauthorization>(Resource.Preauthorizations, filter);
        }

        /// <summary>
        /// This function creates a transaction object
        /// </summary>
        /// <param name="client">Object-transaction</param>
        /// <returns>New object-transaction just add</returns>
        public Preauthorization Create(Preauthorization preauthorization)
        {
            Preauthorization reply=null;

            Transaction replyTransaction = create<Transaction>(
                Resource.Preauthorizations,
                null,
                new URLEncoder().EncodePreauthorization(preauthorization));

            if (replyTransaction != null)
                reply = replyTransaction.Preauthorization;

            return reply;
        }

        /// <summary>
        /// To get the details of an existing preauthorization you’ll need to supply the transaction ID
        /// </summary>
        /// <param name="clientID">Preauthorization identifier</param>
        /// <returns>Preauthorization-object</returns>
        public Preauthorization Get(string preauthorizationID)
        {
            return get<Preauthorization>(Resource.Preauthorizations, preauthorizationID);
        }
    }
}
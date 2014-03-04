using System;
using System.Net.Http;
using System.Threading.Tasks;
using PaymillWrapper.Models;
using PaymillWrapper.Net;

namespace PaymillWrapper.Service
{
/*    public class PreauthorizationService : AbstractService<Preauthorization>
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

*/


    public class PreauthorizationService : AbstractService<Preauthorization>
    {
        public PreauthorizationService(HttpClient client, string apiUrl) 
            : base(Resource.Preauthorizations, client, apiUrl)
        {
        }

        protected override string GetResourceId(Preauthorization obj)
        {
            return obj.Id;
        }
          /// <summary>
        /// This function creates a transaction object
        /// </summary>
        /// <param name="token">token</param>
        ///  <param name="amount">amount</param>
        /// <param name="currency">currency</param>
        /// <returns>New object-transaction just add</returns>
        public Preauthorization CreateWithToken(String token, int amount, String currency)
        {
           /* return Create(
                 null,
                 new UrlEncoder().EncodePreauthorization(token, amount, currency));
            * */
            return null;
        }
        public Preauthorization CreateWithPayment(Payment payment, int amount, String currency)
        {
            /*
            return Create(
                null,
                new UrlEncoder().EncodePreauthorization(payment, amount, currency));
             * */
            return null;
        }
    }
}
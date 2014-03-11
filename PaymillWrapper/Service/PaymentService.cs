using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PaymillWrapper;
using PaymillWrapper.Models;
using PaymillWrapper.Net;

namespace PaymillWrapper.Service
{
/*
   
    public class PaymentService : AbstractService<Payment>
    {
        public PaymentService(HttpClientRest client)
            : base(client)
        {
        }



        /// <summary>
        /// To get the details of an existing payment you’ll need to supply the payment ID
        /// </summary>
        /// <param name="clientID">Payment identifier</param>
        /// <returns>Payment-object</returns>
        public Payment Get(string paymentID)
        {
            return get<Payment>(Resource.Payments, paymentID);
        }

        /// <summary>
        /// This function deletes a payment
        /// </summary>
        /// <param name="clientID">Payment identifier</param>
        /// <returns>Return true if remove was ok, false if not possible</returns>
        public bool Remove(string paymentID)
        {
            return remove<Payment>(Resource.Payments, paymentID);
        }

*/
    public class PaymentService : AbstractService<Payment>
    {
        public PaymentService(HttpClient client, string apiUrl)
            : base(Resource.Payments, client, apiUrl)
        {
        }
        /// <summary>
        /// This function allows request a payment list
        /// </summary>
        /// <returns>Returns a list payments-object</returns>
        public async Task< List<Payment> > GetPaymentsAsync()
        {
            return await ListAsync();
        }

        /// <summary>
        /// This function allows request a payment list
        /// </summary>
        /// <param name="filter">Result filtered in the required way</param>
        /// <returns>Returns a list payments-object</returns>
        public async Task< List<Payment>> GetPaymentsByFilter(Filter filter)
        {
            return await ListAsync(filter);
        }
        /// <summary>
        /// This function creates a payment object
        /// </summary>
        /// <param name="token">payment token</param>
        /// <returns>New object-payment just created</returns>
        public Payment Create(String token)
        {
            return Create(
                null,
                new UrlEncoder().EncodeObject(new { Token = token }));
        }
        /// <summary>
        /// This function creates a payment object with client
        /// </summary>
        /// <param name="token">payment token</param>
        /// <param name="token">payment client</param>
        /// <returns>New object-payment just created</returns>
        public Payment Create(String token, String client)
        {
            /*
            return Create(
                null,
                new UrlEncoder().EncodeObject(new { Token = token, Client = client }));
             * */
            return null;
        }
        protected override string GetResourceId(Payment obj)
        {
            return obj.Id;
        }

    }
}
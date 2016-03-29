using PaymillWrapper.Models;
using PaymillWrapper.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PaymillWrapper.Service
{
    public class PaymentService : AbstractService<Payment>
    {
        public PaymentService(HttpClient client, string apiUrl)
            : base(Resource.Payments, client, apiUrl)
        {
        }
        /// <summary>
        /// This function creates a payment object
        /// </summary>
        /// <param name="token">payment token</param>
        /// <returns>New object-payment just created</returns>
        public async Task<Payment> CreateWithTokenAsync(String token)
        {
            ValidationUtils.ValidatesToken(token);
            return await createAsync(null,
                new UrlEncoder().EncodeObject(new { Token = token }));
        }
        /// <summary>
        /// Creates the with token and client.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="client">The client.</param>
        /// <returns></returns>
        public async Task<Payment> CreateWithTokenAndClientAsync(String token, Client client)
        {
            ValidationUtils.ValidatesToken(token);
            ValidationUtils.ValidatesClient(client);
            return await createAsync(null,
                new UrlEncoder().EncodeObject(new { Token = token, Client = client.Id }));
        }
        /// <summary>
        /// This function creates a payment object with client
        /// </summary>
        /// <param name="token">payment token</param>
        /// <param name="clientId ">payment client id</param>
        /// <returns>New object-payment just created</returns>
        public async Task<Payment> CreateWithTokenAndClientAsync(String token, String clientId)
        {
            ValidationUtils.ValidatesToken(token);
            ValidationUtils.ValidatesId(clientId);
            return await createAsync(null,
                new UrlEncoder().EncodeObject(new { Token = token, Client = clientId }));
        }
        /// <summary>
        /// This function returns a <see cref="PaymillList"/>of PAYMILL Client objects. In which order this list is returned depends on the
        /// </summary>
        /// <param name="filter">Filter or null</param>
        /// <param name="order">Order or null.</param>
        /// <returns>PaymillList which contains a List of PAYMILL Client object and their total count.</returns>
        public async Task<PaymillWrapper.Models.PaymillList<Payment>> ListAsync(Payment.Filter filter, Payment.Order order)
        {
            return await base.listAsync(filter, order, null, null);
        }

        /// <summary>
        /// This function returns a <see cref="PaymillList"/> of PAYMILL objects. In which order this list is returned depends on the
        /// optional parameters. If null is given, no filter or order will be applied, overriding the default count and
        /// offset.
        /// </summary>
        /// <param name="filter">Filter or null</param>
        /// <param name="order">Order or null.</param>
        /// <param name="count">Max count of returned objects in the PaymillList</param>
        /// <param name="offset">The offset to start from.</param>
        /// <returns>PaymillList which contains a List of PAYMILL objects and their total count.</returns>
        public async Task<PaymillWrapper.Models.PaymillList<Payment>> ListAsync(Payment.Filter filter, Payment.Order order, int? count, int? offset)
        {
            return await base.listAsync(filter, order, count, offset);
        }
        protected override string GetResourceId(Payment obj)
        {
            return obj.Id;
        }

    }
}

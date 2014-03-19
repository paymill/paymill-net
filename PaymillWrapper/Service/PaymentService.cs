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
using PaymillWrapper.Utils;

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
        protected override string GetResourceId(Payment obj)
        {
            return obj.Id;
        }

    }
}
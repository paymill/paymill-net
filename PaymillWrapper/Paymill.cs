using PaymillWrapper.Models;
using PaymillWrapper.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using PaymillWrapper.Service;

namespace PaymillWrapper
{
    public static class Paymill
    {
        public static string ApiKey { get; set; }
        public static string ApiUrl { get; set; }
        public static HttpClientRest Client
        {
            get
            {
                if (string.IsNullOrEmpty(ApiKey))
                    throw new PaymillException("You need to set an api key before instantiating an HttpClientRest");

                if (string.IsNullOrEmpty(ApiUrl))
                    throw new PaymillException("You need to set an api url before instantiating an HttpClientRest");

                var client = new HttpClientRest(ApiUrl, ApiKey);
                client.Headers.Add("Accept", "application/json");

                string authInfo = ApiKey + ":";
                authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));
                client.Headers.Add("Authorization", "Basic " + authInfo);  

                return client;
            }
        }

        /// <summary>
        /// Allows get access to the data provider
        /// </summary>
        /// <typeparam name="AbstractService">Type of service</typeparam>
        /// <returns>New object-service</returns>
        public static AbstractService GetService<AbstractService>()
        {
            AbstractService reply = (AbstractService)Activator.CreateInstance(typeof(AbstractService),Client);

            return reply;
        }
    }
}
using System;
using System.Net.Http.Headers;
using System.Text;
using PaymillWrapper.Utils;
using PaymillWrapper.Exceptions;

namespace PaymillWrapper
{
    public static class Paymill
    {
        static string _apiUrl = String.Empty;
        public static string ApiKey { get; set; }
        public static string ApiUrl
        {
            get
            {
                return _apiUrl;
            }
            set
            {
                _apiUrl = value;
                if (value.EndsWith("/"))
                {
                    _apiUrl = value.TrimEnd('/');
                }

            }
        }
        public static HttpClientRest Client
        {
            get
            {
                if (string.IsNullOrEmpty(ApiKey))
                    throw new PaymillException("You need to set an api key before instantiating an HttpClientRest");

                if (string.IsNullOrEmpty(ApiUrl))
                    throw new PaymillException("You need to set an api url before instantiating an HttpClientRest");

                var client = new HttpClientRest(ApiUrl, ApiKey);
                client.DefaultRequestHeaders.Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string authInfo = ApiKey + ":";
                authInfo = Convert.ToBase64String(Encoding.UTF8.GetBytes(authInfo));
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", authInfo);

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
            AbstractService reply = (AbstractService)Activator.CreateInstance(typeof(AbstractService), Client);

            return reply;
        }
    }
}

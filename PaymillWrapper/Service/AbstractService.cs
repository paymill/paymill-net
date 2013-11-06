using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PaymillWrapper.Net;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace PaymillWrapper.Service
{
    public class AbstractService<T>
    {
        protected HttpClientRest _client;

        public enum Resource
        {
            Clients,
            Offers,
            Payments,
            Refunds,
            Subscriptions,
            Transactions,
            Preauthorizations,
            Webhooks
        }

        public AbstractService(HttpClientRest client)
        {
            _client = client;
        }

        protected List<T> getList<T>(Resource resource, Filter filter)
        {
            var lstPayments = new List<T>();

            string requestUri = Paymill.ApiUrl + "/" + resource.ToString().ToLower();

            if (filter != null)
                requestUri += String.Format("?{0}", filter.ToString());
            HttpResponseMessage response = _client.GetAsync(requestUri).Result;
            String data = readReponseMessage(response);
            lstPayments = Newtonsoft.Json.JsonConvert.DeserializeObject<List<T>>(data);
            return lstPayments;
        }

        protected List<T> getList<T>(Resource resource)
        {
            return getList<T>(resource, null);
        }
        protected T create<T>(Resource resource, string resourceID, string encodeParams)
        {
            T reply = default(T);

            var content = new StringContent(encodeParams);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
            string requestUri = Paymill.ApiUrl + "/" + resource.ToString().ToLower();

            if (!string.IsNullOrEmpty(resourceID))
            {
                requestUri += "/" + resourceID;
            }
            HttpResponseMessage response = _client.PostAsync(requestUri, content).Result;
            String data = readReponseMessage(response);
            reply = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(data);
            return reply;
        }

        protected T get<T>(Resource resource, string resourceID)
        {
            T reply = default(T);

            string requestUri = Paymill.ApiUrl + "/" + resource.ToString().ToLower() + "/" + resourceID;
            HttpResponseMessage response = _client.GetAsync(requestUri).Result;
            String data = readReponseMessage(response);
            reply = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(data);
            return reply;
        }
        private String readReponseMessage(HttpResponseMessage response)
        {
            var jsonArray = response.Content.ReadAsAsync<JObject>().Result;
            if (response.IsSuccessStatusCode)
            {
                return jsonArray["data"].ToString();
            }
            else
            {
                string error = jsonArray["error"].ToString();
                throw new PaymillRequestException(error, response.StatusCode);
            }
        }
        protected bool remove<T>(Resource resource, string resourceID)
        {
            string requestUri = Paymill.ApiUrl + "/" + resource.ToString().ToLower() + "/" + resourceID;

            HttpResponseMessage response = _client.DeleteAsync(requestUri).Result;
            readReponseMessage(response);
            return true;
        }
        protected T update<T>(Resource resource, object obj, string resourceID, string encodeParams)
        {
            T reply = default(T);

            var content = new StringContent(encodeParams);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

            string requestUri = Paymill.ApiUrl + "/" + resource.ToString().ToLower() + "/" + resourceID;
            HttpResponseMessage response = _client.PutAsync(requestUri, content).Result;
            String data = readReponseMessage(response);
            reply = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(data);
            return reply;
        }
    }
}
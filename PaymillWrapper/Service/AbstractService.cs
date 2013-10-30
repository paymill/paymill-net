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
            Preauthorizations
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
                requestUri += String.Format("?{0}",filter.ToString());
            HttpResponseMessage response  = _client.GetAsync(requestUri).Result;
            if (response.IsSuccessStatusCode)
            {
                var jsonArray = response.Content.ReadAsAsync<JObject>().Result;
                String jsonData = jsonArray["data"].ToString();
                lstPayments = Newtonsoft.Json.JsonConvert.DeserializeObject<List<T>>(jsonData);
            }
            else
            {
                throw new PaymillRequestException(response.ReasonPhrase, response.StatusCode);
            }
            return lstPayments;
        }

        protected List<T> getList<T>(Resource resource)
        {
            return getList<T>(resource,null);
        }
        protected T add<T>(Resource resource, object obj, string resourceID, string encodeParams)
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
            if (response.IsSuccessStatusCode)
            {
                var jsonArray = response.Content.ReadAsAsync<JObject>().Result;
                reply = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsonArray["data"].ToString());
            }
            else
            {
                throw new PaymillRequestException(response.ReasonPhrase, response.StatusCode);
            }
            return reply;
        }
        protected T get<T>(Resource resource, string resourceID)
        {
            T reply = default(T);

            string requestUri = Paymill.ApiUrl + "/" + resource.ToString().ToLower() + "/" + resourceID;
            HttpResponseMessage response = _client.GetAsync(requestUri).Result;
            if (response.IsSuccessStatusCode)
            {
                var jsonArray = response.Content.ReadAsAsync<JObject>().Result;
                reply = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsonArray["data"].ToString());
            }
            else
            {
                throw new PaymillRequestException(response.ReasonPhrase, response.StatusCode);
            }
            return reply;
        }
        protected bool remove<T>(Resource resource, string resourceID)
        {
            bool reply = false;

            string requestUri = Paymill.ApiUrl + "/" + resource.ToString().ToLower() + "/" + resourceID;

            HttpResponseMessage response = _client.DeleteAsync(requestUri).Result;
            if (response.IsSuccessStatusCode)
            {
                var jsonArray = response.Content.ReadAsAsync<JObject>().Result;
                string r = jsonArray["data"].ToString();
                reply = r.Equals("[]");
            }
            else
            {
                throw new PaymillRequestException(response.ReasonPhrase, response.StatusCode);
            }
            return reply;
        }
        protected T update<T>(Resource resource, object obj, string resourceID, string encodeParams)
        {
            T reply = default(T);

            var content = new StringContent(encodeParams);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

            string requestUri = Paymill.ApiUrl + "/" + resource.ToString().ToLower() + "/" + resourceID;

            HttpResponseMessage response = _client.DeleteAsync(requestUri).Result;
            if (response.IsSuccessStatusCode)
            {
                var jsonArray = response.Content.ReadAsAsync<JObject>().Result;
                reply = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsonArray["data"].ToString());
            }
            else
            {
                throw new PaymillRequestException(response.ReasonPhrase, response.StatusCode);
            }
            return reply;

           
            return reply;
        }
    }
}
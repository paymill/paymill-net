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

            var task = _client.GetAsync(requestUri)
                .ContinueWith(
                (result) =>
                {
                    var response = result.Result;
                    response.EnsureSuccessStatusCode();

                    var task2 = response.Content
                        .ReadAsAsync<JObject>()
                        .ContinueWith(readResult =>
                        {
                            var jsonArray = readResult.Result;
                            lstPayments = Newtonsoft.Json.JsonConvert
                                .DeserializeObject<List<T>>(jsonArray["data"].ToString());

                        });
                    task2.Wait();
                });
            task.Wait();

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
                requestUri += "/" + resourceID;

            var task = _client.PostAsync(requestUri, content).ContinueWith(
                (result) =>
                {
                    var response = result.Result;
                    response.EnsureSuccessStatusCode();

                    var task2 = response.Content
                        .ReadAsAsync<JObject>()
                        .ContinueWith(readResult =>
                        {
                            var jsonArray = readResult.Result;

                            reply = Newtonsoft.Json.JsonConvert
                                .DeserializeObject<T>(jsonArray["data"].ToString());
                        });
                    task2.Wait();
                });
            task.Wait();

            return reply;
        }
        protected T get<T>(Resource resource, string resourceID)
        {
            T reply = default(T);

            string requestUri = Paymill.ApiUrl + "/" + resource.ToString().ToLower() + "/" + resourceID;
            var task = _client.GetAsync(requestUri).ContinueWith(
                (result) =>
                {
                    var response = result.Result;
                    response.EnsureSuccessStatusCode();

                    var task2 = response.Content
                        .ReadAsAsync<JObject>()
                        .ContinueWith(readResult =>
                        {
                            var jsonArray = readResult.Result;
                            reply = Newtonsoft.Json.JsonConvert
                                .DeserializeObject<T>(jsonArray["data"].ToString());
                        });
                    task2.Wait();
                });
            task.Wait();

            return reply;
        }
        protected bool remove<T>(Resource resource, string resourceID)
        {
            bool reply = false;

            string requestUri = Paymill.ApiUrl + "/" + resource.ToString().ToLower() + "/" + resourceID;
            var task = _client.DeleteAsync(requestUri).ContinueWith(
                (result) =>
                {
                    var response = result.Result;
                    response.EnsureSuccessStatusCode();

                    var task2 = response.Content
                        .ReadAsAsync<JObject>()
                        .ContinueWith(readResult =>
                        {
                            var jsonArray = readResult.Result;
                            string r = jsonArray["data"].ToString();
                            reply = r.Equals("[]");
                        });
                    task2.Wait();
                });
            task.Wait();

            return reply;
        }
        protected T update<T>(Resource resource, object obj, string resourceID, string encodeParams)
        {
            T reply = default(T);

            var content = new StringContent(encodeParams);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

            string requestUri = Paymill.ApiUrl + "/" + resource.ToString().ToLower() + "/" + resourceID;
            var task = _client.PutAsync(requestUri, content).ContinueWith(
                (result) =>
                {
                    var response = result.Result;
                    response.EnsureSuccessStatusCode();

                    var task2 = response.Content
                        .ReadAsAsync<JObject>()
                        .ContinueWith(readResult =>
                        {
                            var jsonArray = readResult.Result;

                            reply = Newtonsoft.Json.JsonConvert
                                .DeserializeObject<T>(jsonArray["data"].ToString());
                        });
                    task2.Wait();
                });
            task.Wait();

            return reply;
        }
    }
}
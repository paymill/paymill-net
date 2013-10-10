using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PaymillWrapper.Net;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PaymillWrapper.Service
{
    public abstract class AbstractService<T> : ICRUDService<T>
    {
        private readonly Resource _resource;
        protected readonly HttpClient Client;
        private readonly string _apiUrl;

        internal AbstractService(Resource resource,
            HttpClient client,
            string apiUrl)
        {
            _resource = resource;
            Client = client;
            _apiUrl = apiUrl;
        }

        protected abstract string GetResourceId(T obj);
        protected abstract string GetEncodedUpdateParams(T obj, UrlEncoder encoder);

        public IReadOnlyCollection<T> getList(Filter filter)
        {
            var lst = new List<T>();
            string requestUri = _apiUrl + "/" + _resource.ToString().ToLower();
            if (filter != null)
                requestUri += String.Format("?{0}", filter.ToString());
            HttpResponseMessage response = Client.GetAsync(requestUri).Result;
            String data = readReponseMessage(response);
            lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<T>>(data);
            return lst;
        }

        public IReadOnlyCollection<T> getList()
        {
            return getList(null);
        }
        public T Create(string id, string encodeParams)
        {
            T reply = default(T);

            var content = new StringContent(encodeParams);
            var requestUri = _apiUrl + "/" + _resource.ToString().ToLower();
            if (!string.IsNullOrEmpty(id))
            {
                requestUri += "/" + id;
            }
            HttpResponseMessage response = Client.PostAsync(requestUri, content).Result;
            String data = readReponseMessage(response);
            reply = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(data);
            return reply;
        }

        /// <summary>
        /// Adds an "item". Use this call if the result returns a different class than you send in.
        /// </summary>
        /// <typeparam name="TResult">The resulting type.</typeparam>
        /// 

        /*
        protected async Task<TResult> AddAsync<TResult>(T obj)
        {
            var content = new StringContent(GetEncodedCreateParams(obj, new UrlEncoder()));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
            string requestUri = _apiUrl + "/" + resource.ToString().ToLower();
          */

        public T Get(string id)
        {
            T reply = default(T);
      
            string requestUri = _apiUrl + "/" + _resource.ToString().ToLower();
            if (!string.IsNullOrEmpty(id))
                requestUri += "/" + id;

            HttpResponseMessage response = Client.GetAsync(requestUri).Result;
            String data = readReponseMessage(response);
            reply = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(data);
            return reply;
        }

        public bool Remove(string id)
        {
            string requestUri = _apiUrl + "/" + _resource.ToString().ToLower() + "/" + id;
            HttpResponseMessage response = Client.DeleteAsync(requestUri).Result;
            readReponseMessage(response);
            return true;
        }
        public T Update(T obj)
        {
            T reply = default(T);
            String resourceId = GetResourceId(obj);
            var content = new StringContent(GetEncodedUpdateParams(obj, new UrlEncoder()));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
            string requestUri = _apiUrl + "/" + _resource.ToString().ToLower() + "/" + resourceId;
            HttpResponseMessage response = Client.PutAsync(requestUri, content).Result;
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
    }
}
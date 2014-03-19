using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PaymillWrapper.Net;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using PaymillWrapper.Exceptions;
using Newtonsoft.Json;

namespace PaymillWrapper.Service
{
    public abstract class AbstractService<T> 
    {
        private readonly Resource _resource;
        protected readonly HttpClient httpClient;
        private readonly string _apiUrl;

        internal AbstractService(Resource resource,
            HttpClient client,
            string apiUrl)
        {
            _resource = resource;
            httpClient = client;
            _apiUrl = apiUrl;
        }

        protected abstract string GetResourceId(T obj);

        /// <summary>
        /// Lists the asynchronous.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public async Task<List<T> > ListAsync(Filter filter)
        {
            var lst = new List<T>();
            string requestUri = _apiUrl + "/" + _resource.ToString().ToLower();
            if (filter != null)
                requestUri += String.Format("?{0}", filter.ToString());
            HttpResponseMessage response = httpClient.GetAsync(requestUri).Result;
            String data = await readReponseMessage(response);
            return JsonConvert.DeserializeObject<MultipleResults<T>>(data,new UnixTimestampConverter()).Data;
        }

        /// <summary>
        /// Lists the asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<List<T> > ListAsync()
        {
            return await ListAsync(null);
        }
        /// <summary>
        /// Creates the asynchronous.
        /// </summary>
        /// <param name="encodeParams">The encode parameters.</param>
        /// <returns></returns>
        protected async Task<T> createAsync(string encodeParams)
        {
            T reply = default(T);
            var content = new StringContent(encodeParams);
            var requestUri = _apiUrl + "/" + _resource.ToString().ToLower();
            HttpResponseMessage response = httpClient.PostAsync(requestUri, content).Result;
            String data = await readReponseMessage(response);
            return JsonConvert.DeserializeObject<SingleResult<T>>(data, new UnixTimestampConverter()).Data;
        }

        public async Task<T> GetAsync(string id)
        {
            T reply = default(T);
      
            string requestUri = _apiUrl + "/" + _resource.ToString().ToLower();
            if (!string.IsNullOrEmpty(id))
                requestUri += "/" + id;

            HttpResponseMessage response = httpClient.GetAsync(requestUri).Result;
            String data = await readReponseMessage(response);
            return JsonConvert.DeserializeObject<SingleResult<T>>(data, new UnixTimestampConverter()).Data;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            string requestUri = _apiUrl + "/" + _resource.ToString().ToLower() + "/" + id;
            HttpResponseMessage response = httpClient.DeleteAsync(requestUri).Result;
            await readReponseMessage(response);
            return true;
        }
        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public async Task<T> UpdateAsync(T obj)
        {
            T reply = default(T);
            String resourceId = GetResourceId(obj);
            var encoder = new UrlEncoder();
            var content = new StringContent(encoder.EncodeUpdate(obj));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
            string requestUri = _apiUrl + "/" + _resource.ToString().ToLower() + "/" + resourceId;
            HttpResponseMessage response = httpClient.PutAsync(requestUri, content).Result;
            String data = await readReponseMessage(response);
            return JsonConvert.DeserializeObject<SingleResult<T>>(data, new UnixTimestampConverter()).Data;
        }

        /// <summary>
        /// Reads the reponse message.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <returns></returns>
        /// <exception cref="PaymillRequestException"></exception>
        private async static Task<String> readReponseMessage(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                var jsonArray = response.Content.ReadAsAsync<JObject>();
                string error = (await jsonArray)["error"].ToString();
                throw new PaymillRequestException(error, response.StatusCode);
            }
        }
    }
}
﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PaymillWrapper.Exceptions;
using PaymillWrapper.Utils;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

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
        /// This function returns a List of PAYMILL objects.
        /// </summary>
        /// <returns></returns>
        public async Task<PaymillWrapper.Models.PaymillList<T>> ListAsync()
        {
            return await listAsync(null, null, null, null);
        }

        /// <summary>
        /// This function returns a <see cref="PaymillList"/> of PAYMILL objects, overriding the default count and offset.
        /// </summary>
        /// <param name="count">Max count of returned objects in the PaymillList</param>
        /// <param name="offset">The offset to start from.</param>
        /// <returns>{@link PaymillList} which contains a {@link List} of PAYMILL {@link Client}s and their total count.</returns>
        public async Task<PaymillWrapper.Models.PaymillList<T>> ListAsync(int? count, int? offset)
        {
            return await listAsync(null, null, count, offset);
        }

        /// <summary>
        /// This function returns a <see cref="PaymillList"/>of PAYMILL objects. In which order this list is returned depends on the
        /// </summary>
        /// <param name="filter">Filter or null</param>
        /// <param name="order">Order or null.</param>
        /// <returns>PaymillList which contains a List of PAYMILL object and their total count.</returns>
        protected async Task<PaymillWrapper.Models.PaymillList<T>> listAsync(Object filter, Object order)
        {
            return await listAsync(filter, order, null, null);
        }

        /// <summary>
        /// This function returns a <see cref="PaymillList"/> of PAYMILL  objects. In which order this list is returned depends on the
        /// optional parameters. If null is given, no filter or order will be applied, overriding the default count and
        /// offset.
        /// </summary>
        /// <param name="filter">Filter or null</param>
        /// <param name="order">Order or null.</param>
        /// <param name="count">Max count of returned objects in the PaymillList</param>
        /// <param name="offset">The offset to start from.</param>
        /// <returns>PaymillList which contains a List of PAYMILL objects and their total count.</returns>
        protected async Task<PaymillWrapper.Models.PaymillList<T>> listAsync(Object filter, Object order, int? count, int? offset)
        {
            var encoder = new UrlEncoder();
            String encodedParam = encoder.EncodeFilterParameters(filter, order, count, offset);
            string requestUri = _apiUrl + "/" + _resource.ToString().ToLower();
            UriBuilder urlBuilder = new UriBuilder(requestUri);
            urlBuilder.Query = encodedParam;
            HttpResponseMessage response = httpClient.GetAsync(urlBuilder.Uri).Result;
            String data = await readReponseMessage(response);
            return ReadResults<T>(data);
        }

        /// <summary>
        /// Creates the asynchronous.
        /// </summary>
        /// <param name="encodeParams">The encode parameters.</param>
        /// <returns></returns>
        protected async Task<ST> createSubClassAsync<ST>(String subResource, string encodeParams)
        {
            var content = new StringContent(encodeParams);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
            var requestUri = _apiUrl + "/" + subResource.ToString().ToLower();
            HttpResponseMessage response = httpClient.PostAsync(requestUri, content).Result;
            String data = await readReponseMessage(response);
            return ReadResult<ST>(data);
        }
        /// <summary>
        /// Creates the asynchronous.
        /// </summary>
        /// <param name="encodeParams">The encode parameters.</param>
        /// <returns></returns>
        protected async Task<T> createAsync(String resourceId, string encodeParams)
        {
            var content = new StringContent(encodeParams);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
            var requestUri = _apiUrl + "/" + _resource.ToString().ToLower();
            if (String.IsNullOrEmpty(resourceId) == false)
            {
                requestUri += "/" + resourceId;
            }
            HttpResponseMessage response = httpClient.PostAsync(requestUri, content).Result;
            String data = await readReponseMessage(response);
            return ReadResult<T>(data);
        }

        public async Task<T> GetAsync(string id)
        {
            string requestUri = _apiUrl + "/" + _resource.ToString().ToLower();
            if (!string.IsNullOrEmpty(id))
                requestUri += "/" + id;

            HttpResponseMessage response = httpClient.GetAsync(requestUri).Result;
            String data = await readReponseMessage(response);
            return ReadResult<T>(data);
        }

        public virtual async Task<bool> DeleteAsync(string id)
        {
            string requestUri = _apiUrl + "/" + _resource.ToString().ToLower() + "/" + id;
            HttpResponseMessage response = httpClient.DeleteAsync(requestUri).Result;
            await readReponseMessage(response);
            return true;
        }
        /// <summary>
        /// Delete object the asynchronous.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public virtual async Task<bool> DeleteAsync(T obj)
        {
            string requestUri = _apiUrl + "/" + _resource.ToString().ToLower() + "/" + GetResourceId(obj);
            HttpResponseMessage response = httpClient.DeleteAsync(requestUri).Result;
            await readReponseMessage(response);
            return true;
        }
        /// <summary>
        /// Delete object the asynchronous.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        protected virtual async Task<T> deleteParamAsync(String resourceId, Object param)
        {
            var encoder = new UrlEncoder();

            string requestUri = _apiUrl + "/" + _resource.ToString().ToLower() + "/" + resourceId;
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, requestUri);
            ;
            request.Content = new StringContent(encoder.EncodeObject(param));
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
            HttpResponseMessage response = httpClient.SendAsync(request).Result;
            String data = await readReponseMessage(response);
            return ReadResult<T>(data);
        }
        /// <summary>
        /// Delete object the asynchronous.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        protected async Task<Boolean> deleteParamBoolAsync(String resourceId, Object param)
        {
            var encoder = new UrlEncoder();

            string requestUri = _apiUrl + "/" + _resource.ToString().ToLower() + "/" + resourceId;
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, requestUri);
            ;
            request.Content = new StringContent(encoder.EncodeObject(param));
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
            HttpResponseMessage response = httpClient.SendAsync(request).Result;
            await readReponseMessage(response);
            return true;
        }
        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public virtual async Task<T> UpdateAsync(T obj)
        {
            String resourceId = GetResourceId(obj);
            var encoder = new UrlEncoder();
            String paramStr = encoder.EncodeUpdate(obj);
            var content = new StringContent(paramStr);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
            string requestUri = _apiUrl + "/" + _resource.ToString().ToLower() + "/" + resourceId;
            HttpResponseMessage response = httpClient.PutAsync(requestUri, content).Result;
            String data = await readReponseMessage(response);

            return ReadResult<T>(data);
        }
        /// <summary>
        /// Updates the asynchronous with custom params
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        protected virtual async Task<T> updateParamAsync(String resourceId, Object param)
        {
            var encoder = new UrlEncoder();
            var content = new StringContent(encoder.EncodeObject(param));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
            string requestUri = _apiUrl + "/" + _resource.ToString().ToLower() + "/" + resourceId;
            HttpResponseMessage response = httpClient.PutAsync(requestUri, content).Result;
            String data = await readReponseMessage(response);
            return ReadResult<T>(data);
        }
        /// Updates the asynchronous with custom params
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        protected virtual async Task<T> updateWithContentAsync(String resourceId, string contentStr)
        {

            var content = new StringContent(contentStr);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
            string requestUri = _apiUrl + "/" + _resource.ToString().ToLower() + "/" + resourceId;
            HttpResponseMessage response = httpClient.PutAsync(requestUri, content).Result;
            String data = await readReponseMessage(response);
            return ReadResult<T>(data);
        }
        /// <summary>
        /// Reads the reponse message.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <returns></returns>
        /// <exception cref="PaymillRequestException"></exception>
        private static Task<String> readReponseMessage(HttpResponseMessage response)
        {
            try
            {
                String json = response.Content.ReadAsStringAsync().Result;
                var jsonArray = JObject.Parse(json);
                if (jsonArray["data"] != null)
                {
                    return response.Content.ReadAsStringAsync();
                }
                else if (jsonArray["error"] != null)
                {
                    string error = jsonArray["error"].ToString();
                    throw new PaymillRequestException(error, response.StatusCode);
                }
            }
            catch (System.IO.IOException exc)
            {
                throw new PaymillException(exc.Message);
            }
            // For .Net 4.0 use the code bellow 
            /*        return Task.Run(() =>
                        {
                            return String.Empty;
                        });*/
            return Task.Factory.StartNew<String>(() =>
            {
                return String.Empty;
            });
        }

        public static TE ReadResult<TE>(string data)
        {
            return JsonConvert.DeserializeObject<SingleResult<TE>>(data, customConverters).Data;
        }

        internal static PaymillWrapper.Models.PaymillList<TE> ReadResults<TE>(string data)
        {
            return JsonConvert.DeserializeObject<PaymillWrapper.Models.PaymillList<TE>>(data,
                                customConverters);
        }

        internal static Newtonsoft.Json.JsonConverter[] customConverters = { new UnixTimestampConverter(),
                                                                            new StringToNIntConverter()};
    }
}

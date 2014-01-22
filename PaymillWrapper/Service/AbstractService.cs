using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PaymillWrapper.Net;
using System.Net;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Text;

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
            var lst = new List<T>();
            string requestUri = Paymill.ApiUrl + "/" + resource.ToString().ToLower();
            if (filter != null)
                requestUri += String.Format("?{0}", filter.ToString());
            try
            {
                String jsonResponse = _client.DownloadString(requestUri);
                String data = readReponseMessage(jsonResponse, HttpStatusCode.OK);
                lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<T>>(data);
            }
            catch (WebException ex)
            {
                var response = ((HttpWebResponse)ex.Response);
                throwResponseError(response);
            }
            return lst;
        }

        protected List<T> getList<T>(Resource resource)
        {
            return getList<T>(resource, null);
        }
        protected T create<T>(Resource resource, string resourceID, string encodeParams)
        {
            T reply = default(T);
            string requestUri = Paymill.ApiUrl + "/" + resource.ToString().ToLower();
            try
            {
                _client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                String jsonResponse = _client.UploadString(requestUri, "POST", encodeParams);
                String data = readReponseMessage(jsonResponse, HttpStatusCode.OK);
                reply = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(data);
            }
            catch (WebException ex)
            {
                var response = ((HttpWebResponse)ex.Response);
                throwResponseError(response);
            }
            return reply;
        }

        protected T get<T>(Resource resource, string resourceID)
        {
            T reply = default(T);
            string requestUri = Paymill.ApiUrl + "/" + resource.ToString().ToLower() + "/" + resourceID;
            try
            {
                String jsonResponse = _client.DownloadString(requestUri);
                String data = readReponseMessage(jsonResponse, HttpStatusCode.OK);
                reply = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(data);
            }
            catch (WebException ex)
            {
                var response = ((HttpWebResponse)ex.Response);
                throwResponseError(response);
            }
             return reply;
        }
        protected bool remove<T>(Resource resource, string resourceID)
        {
            string requestUri = Paymill.ApiUrl + "/" + resource.ToString().ToLower() + "/" + resourceID;
            try
            {
                String jsonResponse = _client.UploadString(requestUri, "DELETE", "");
                readReponseMessage(jsonResponse, HttpStatusCode.OK);
            }
            catch (WebException ex)
            {
                var response = ((HttpWebResponse)ex.Response);
                throwResponseError(response);
            }
            return true;
        }

        protected T update<T>(Resource resource, object obj, string resourceID, string encodeParams)
        {
            T reply = default(T);
            try
            {
                string requestUri = Paymill.ApiUrl + "/" + resource.ToString().ToLower() + "/" + resourceID;
                _client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                String jsonResponse = _client.UploadString(requestUri, "PUT", encodeParams);
                String data = readReponseMessage(jsonResponse, HttpStatusCode.OK);
                reply = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(data);
            }
            catch (WebException ex)
            {
                var response = ((HttpWebResponse)ex.Response);
                throwResponseError(response);
            }
            return reply;
        }
        private static String readReponseMessage(String stringFullOfJson, HttpStatusCode responseCode)
        {
            JToken token = JObject.Parse(stringFullOfJson);
            if (responseCode == HttpStatusCode.OK)
            {
                return token.SelectToken("data").ToString();
            }
            else
            {
                return token.SelectToken("error").ToString();
            }
        }
        private static void throwResponseError(HttpWebResponse response)
        {
            StringBuilder sb = new StringBuilder();
            Byte[] buf = new byte[8192];
            Stream resStream = response.GetResponseStream();
            int count = 0;
            do
            {
                count = resStream.Read(buf, 0, buf.Length);
                if (count != 0)
                {
                    sb.Append(Encoding.UTF8.GetString(buf, 0, count)); // just hardcoding UTF8 here
                }
            } while (count > 0);
            String responseErrorInJson = sb.ToString();
            String error = readReponseMessage(responseErrorInJson, response.StatusCode);
            throw new PaymillRequestException(error, response.StatusCode);
        }

    }
}
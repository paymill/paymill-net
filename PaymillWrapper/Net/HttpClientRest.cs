using Newtonsoft.Json.Linq;
using PaymillWrapper.Models;
using PaymillWrapper.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace PaymillWrapper.Net
{
    public class HttpClientRest : HttpClient
    {
        private string _apiUrl = "";
        private string _apiKey = "";
        private UrlEncoder _urlEncoder;

        /// <summary>
        /// Create an HttpClientRest-object
        /// </summary>
        /// <param name="apiUrl">API Endpoint URL</param>
        /// <param name="apiKey">Private key</param>
        public HttpClientRest(string apiUrl, string apiKey)
        {
            this._apiUrl = apiUrl;
            this._apiKey = apiKey;

            try
            {
                Uri uri = new Uri(apiUrl);
            }
            catch
            {
                throw new PaymillException("ApiURL is not a valid format Uri");
            }

            this._urlEncoder = new UrlEncoder();
        }

    }
}
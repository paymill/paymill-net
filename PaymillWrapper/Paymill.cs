
﻿using System.Net.Http;
using System;
using System.Net.Http.Headers;
using System.Text;
using PaymillWrapper.Models;
using PaymillWrapper.Service;

namespace PaymillWrapper
{
    public class Paymill
    {
        public Paymill(String apiKey)
        {
            ApiUrl = @"https://api.paymill.com/v2";
            if (string.IsNullOrEmpty(apiKey))
                throw new ArgumentException("You need to set an API key", "apiKey");

            if (string.IsNullOrEmpty(ApiUrl))
                throw new ArgumentException("You need to set an API URL.", "apiUrl");

            _clients = new Lazy<ClientService>(() => new ClientService(Client, ApiUrl));
            _offers = new Lazy<OfferService>(() => new OfferService(Client, ApiUrl));
            _payments = new Lazy<PaymentService>(() => new PaymentService(Client, ApiUrl));
            _preauthorizations = new Lazy<PreauthorizationService>(() => new PreauthorizationService(Client, ApiUrl));
            _refunds = new Lazy<RefundService>(() => new RefundService(Client, ApiUrl));
            _subscriptions = new Lazy<SubscriptionService>(() => new SubscriptionService(Client, ApiUrl));
            _transactions = new Lazy<TransactionService>(() => new TransactionService(Client, ApiUrl));
        }

        public static string ApiKey { get; private set; }
        public static string ApiUrl { get; private set; }

        private HttpClient _httpClient;
        protected HttpClient Client
        {
            get
            {
                if (_httpClient == null)
                {
                    _httpClient = new HttpClient();
                    _httpClient.DefaultRequestHeaders.Accept
                        .Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var authInfo = ApiKey + ":";
                    authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authInfo);
                }

                return _httpClient;
            }
        }

        #region Service members
        private readonly Lazy<ClientService> _clients;
        private readonly Lazy<OfferService> _offers;
        private readonly Lazy<PaymentService> _payments;
        private readonly Lazy<PreauthorizationService> _preauthorizations;
        private readonly Lazy<RefundService> _refunds;
        private readonly Lazy<SubscriptionService> _subscriptions;
        private readonly Lazy<TransactionService> _transactions;
        #endregion

        #region Public services
        public ClientService Clients
        {
            get { return _clients.Value; }
        }

        public OfferService Offers
        {
            get { return _offers.Value; }
        }

        public PaymentService Payments
        {
            get { return _payments.Value; }
        }

        public PreauthorizationService Preauthorizations
        {
            get { return _preauthorizations.Value; }
        }

        public RefundService Refunds
        {
            get { return _refunds.Value; }
        }

        public SubscriptionService Subscriptions
        {
            get { return _subscriptions.Value; }
        }

        public TransactionService Transactions
        {
            get { return _transactions.Value; }
        }
        #endregion
    }
}
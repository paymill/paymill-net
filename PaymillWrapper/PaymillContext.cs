using PaymillWrapper.Service;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace PaymillWrapper
{
    public class PaymillContext
    {
        public PaymillContext(String apiKey)
        {
            ApiUrl = @"https://api.paymill.com/v2.1";
            if (string.IsNullOrEmpty(apiKey))
                throw new ArgumentException("You need to set an API key", "apiKey");
            ApiKey = apiKey;
            _clientService = new Lazy<ClientService>(() => new ClientService(Client, ApiUrl));
            _offerService = new Lazy<OfferService>(() => new OfferService(Client, ApiUrl));
            _paymentService = new Lazy<PaymentService>(() => new PaymentService(Client, ApiUrl));
            _preauthorizationService = new Lazy<PreauthorizationService>(() => new PreauthorizationService(Client, ApiUrl));
            _refundService = new Lazy<RefundService>(() => new RefundService(Client, ApiUrl));
            _subscriptionService = new Lazy<SubscriptionService>(() => new SubscriptionService(Client, ApiUrl));
            _transactionService = new Lazy<TransactionService>(() => new TransactionService(Client, ApiUrl));
            _webhookService = new Lazy<WebhookService>(() => new WebhookService(Client, ApiUrl));
            _checksumServicee = new Lazy<ChecksumService>(() => new ChecksumService(Client, ApiUrl));
        }
        public static String GetProjectName()
        {
            return "paymill-net";
        }

        public static String GetProjectVersion()
        {                        
            return "dnxcore50";
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
                    authInfo = Convert.ToBase64String(Encoding.UTF8.GetBytes(authInfo));
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authInfo);
                }

                return _httpClient;
            }
        }

        #region Service members
        private readonly Lazy<ChecksumService> _checksumServicee;
        private readonly Lazy<ClientService> _clientService;
        private readonly Lazy<OfferService> _offerService;
        private readonly Lazy<PaymentService> _paymentService;
        private readonly Lazy<PreauthorizationService> _preauthorizationService;
        private readonly Lazy<RefundService> _refundService;
        private readonly Lazy<SubscriptionService> _subscriptionService;
        private readonly Lazy<TransactionService> _transactionService;
        private readonly Lazy<WebhookService> _webhookService;
        #endregion

        #region Public services
        public ChecksumService ChecksumService
        {
            get { return _checksumServicee.Value; }
        }
        public ClientService ClientService
        {
            get { return _clientService.Value; }
        }

        public OfferService OfferService
        {
            get { return _offerService.Value; }
        }

        public PaymentService PaymentService
        {
            get { return _paymentService.Value; }
        }

        public PreauthorizationService PreauthorizationService
        {
            get { return _preauthorizationService.Value; }
        }

        public WebhookService WebhookService
        {
            get { return _webhookService.Value; }
        }


        public RefundService RefundService
        {
            get { return _refundService.Value; }
        }

        public SubscriptionService SubscriptionService
        {
            get { return _subscriptionService.Value; }
        }

        public TransactionService TransactionService
        {
            get { return _transactionService.Value; }
        }
        #endregion
    }
}

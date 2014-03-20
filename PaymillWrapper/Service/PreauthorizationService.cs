using System;
using System.Net.Http;
using System.Threading.Tasks;
using PaymillWrapper.Models;
using PaymillWrapper.Utils;

namespace PaymillWrapper.Service
{


    public class PreauthorizationService : AbstractService<Preauthorization>
    {
        public PreauthorizationService(HttpClient client, string apiUrl)
            : base(Resource.Preauthorizations, client, apiUrl)
        {
        }

        protected override string GetResourceId(Preauthorization obj)
        {
            return obj.Id;
        }
        /// <summary>
        /// Creates Use either a token or an existing payment to Authorizes the given amount with the given token.
        /// </summary>
        /// <param name="token">The identifier of a token.</param>
        ///  <param name="amount">Amount (in cents) which will be charged.</param>
        /// <param name="currency">ISO 4217 formatted currency code.</param>
        /// <returns>Object with the Preauthorization as sub object.d</returns>
        public async Task<Preauthorization> CreateWithTokenAsync(String token, int amount, String currency)
        {
            ValidationUtils.ValidatesToken(token);
            ValidationUtils.ValidatesAmount(amount);
            ValidationUtils.ValidatesCurrency(currency);

            Transaction replyTransaction = await createSubClassAsync<Transaction>(Resource.Preauthorizations.ToString(),
                 new UrlEncoder().EncodeObject(new { Token = token, Amount = amount, Currency = currency }));
            if (replyTransaction != null)
            {
                return replyTransaction.Preauthorization;
            }

            return null;
        }
        /// <summary>
        /// Authorizes the given amount with the given Payment. Works only for credit cards. Direct debit not supported.
        /// </summary>
        /// <param name="payment">The Payment itself (only creditcard-object)</param>
        /// <param name="amount">Amount (in cents) which will be charged.</param>
        /// <param name="currency">ISO 4217 formatted currency code.</param>
        /// <returns>Transaction object with the Preauthorization as sub object.</returns>
        public async Task<Preauthorization> CreateWithPaymentAsync(Payment payment, int amount, String currency)
        {
            ValidationUtils.ValidatesPayment(payment);
            ValidationUtils.ValidatesAmount(amount);
            ValidationUtils.ValidatesCurrency(currency);

            String srcValue = String.Format("{0}-{1}", Paymill.GetProjectName(), Paymill.GetProjectVersion());

            Transaction replyTransaction = await createSubClassAsync<Transaction>(Resource.Preauthorizations.ToString(),
                new UrlEncoder().EncodeObject(new 
                {
                    Payment = payment.Id,
                    Amount = amount,
                    Currency = currency,
                    Source = srcValue
                }));
            if (replyTransaction != null)
            {
                return replyTransaction.Preauthorization;
            }

            return null;
        }
        public override async Task<Preauthorization> UpdateAsync(Preauthorization obj)
        {
            return await Task<Preauthorization>.Factory.StartNew(() =>
            {
                throw new PaymillWrapper.Exceptions.PaymillException("Now Supported");
            });
        }
    }
}
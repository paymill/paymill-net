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

        /// <summary>
        /// Creates Use either a token or an existing payment to Authorizes the given amount with the given token.
        /// </summary>
        /// <param name="token">The identifier of a token.</param>
        ///  <param name="amount">Amount (in cents) which will be charged.</param>
        /// <param name="currency">ISO 4217 formatted currency code.</param>
        /// <returns>Object with the Preauthorization as sub object.d</returns>
        public async Task<Preauthorization> CreateWithTokenAsync(String token, int amount, String currency)
        {
           
            return await CreateWithTokenAsync(token, amount, currency, null);
        }
        /// <summary>
        /// Creates Use either a token or an existing payment to Authorizes the given amount with the given token.
        /// </summary>
        /// <param name="token">The identifier of a token.</param>
        ///  <param name="amount">Amount (in cents) which will be charged.</param>
        /// <param name="currency">ISO 4217 formatted currency code.</param>
        /// <param name="description">A short description for the preauthorization</param>
        /// <returns>Object with the Preauthorization as sub object.d</returns>
        public async Task<Preauthorization> CreateWithTokenAsync(String token, int amount, String currency, String description)
        {
            ValidationUtils.ValidatesToken(token);
            ValidationUtils.ValidatesAmount(amount);
            ValidationUtils.ValidatesCurrency(currency);

            Transaction replyTransaction = await createSubClassAsync<Transaction>(Resource.Preauthorizations.ToString(),
                 new UrlEncoder().EncodeObject(new
                 {
                     Token = token,
                     Amount = amount,
                     Currency = currency,
                     Description = description
                 }));

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
            return await CreateWithPaymentAsync(payment, amount, currency, null);
        }

        /// <summary>
        /// Authorizes the given amount with the given Payment. Works only for credit cards. Direct debit not supported.
        /// </summary>
        /// <param name="payment">The Payment itself (only creditcard-object)</param>
        /// <param name="amount">Amount (in cents) which will be charged.</param>
        /// <param name="currency">ISO 4217 formatted currency code.</param>
        /// <param name="description">A short description for the preauthorization</param>
        /// <returns>Transaction object with the Preauthorization as sub object.</returns>
        public async Task<Preauthorization> CreateWithPaymentAsync(Payment payment, int amount, String currency, String description)
        {
            ValidationUtils.ValidatesPayment(payment);
            ValidationUtils.ValidatesAmount(amount);
            ValidationUtils.ValidatesCurrency(currency);

            String srcValue = String.Format("{0}-{1}", PaymillContext.GetProjectName(), PaymillContext.GetProjectVersion());
            Transaction replyTransaction = await createSubClassAsync<Transaction>(Resource.Preauthorizations.ToString(),
                new UrlEncoder().EncodeObject(new
                {
                    Payment = payment.Id,
                    Amount = amount,
                    Currency = currency,
                    Source = srcValue,
                    Description = description
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
        /// <summary>
        /// This function returns a <see cref="PaymillList"/>of PAYMILL Client objects. In which order this list is returned depends on the
        /// </summary>
        /// <param name="filter">Filter or null</param>
        /// <param name="order">Order or null.</param>
        /// <returns>PaymillList which contains a List of PAYMILL Client object and their total count.</returns>
        public async Task<PaymillWrapper.Models.PaymillList<Preauthorization>> ListAsync(Preauthorization.Filter filter, Preauthorization.Order order)
        {
            return await base.listAsync(filter, order, null, null);
        }

        /// <summary>
        /// This function returns a <see cref="PaymillList"/> of PAYMILL objects. In which order this list is returned depends on the
        /// optional parameters. If null is given, no filter or order will be applied, overriding the default count and
        /// offset.
        /// </summary>
        /// <param name="filter">Filter or null</param>
        /// <param name="order">Order or null.</param>
        /// <param name="count">Max count of returned objects in the PaymillList</param>
        /// <param name="offset">The offset to start from.</param>
        /// <returns>PaymillList which contains a List of PAYMILL objects and their total count.</returns>
        public async Task<PaymillWrapper.Models.PaymillList<Preauthorization>> ListAsync(Preauthorization.Filter filter, Preauthorization.Order order, int? count, int? offset)
        {
            return await base.listAsync(filter, order, count, offset);
        }

        protected override string GetResourceId(Preauthorization obj)
        {
            return obj.Id;
        }
    }
}
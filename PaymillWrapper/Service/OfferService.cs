using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PaymillWrapper.Models;
using PaymillWrapper.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace PaymillWrapper.Service
{

    public class OfferService : AbstractService<Offer>
    {
        public OfferService(HttpClient client, string apiUrl)
            : base(Resource.Offers, client, apiUrl)
        {
        }


        /// <summary>
        /// Creates an offer via the API.
        /// </summary>
        /// <param name="amount">Amount in cents > 0.</param>
        /// <param name="currency">ISO 4217 formatted currency code.</param>
        /// <param name="interval">Defining how often the Client should be charged. Format: number DAY | WEEK | MONTH | YEAR</param>
        /// <param name="name">Your name for this offer</param>
        /// <returns>Object with id, which represents a PAYMILL offer.</returns>
        public async Task<Offer> CreateAsync(int amount, String currency, String interval, String name)
        {
            return await CreateAsync(amount, currency, interval, name, null);
        }

        /// <summary>
        /// Creates an offer via the API.
        /// </summary>
        /// <param name="amount">Amount in cents > 0.</param>
        /// <param name="currency">ISO 4217 formatted currency code.</param>
        /// <param name="interval">Defining how often the Client should be charged. Format: number DAY | WEEK | MONTH | YEAR</param>
        /// <param name="name">Your name for this offer</param>
        /// <param name="trialPeriodDays">Give it a try or charge directly. Can be null</param>
        /// <returns>Object with id, which represents a PAYMILL offer</returns>
        public async Task<Offer> CreateAsync(int amount, String currency, String interval, String name, int? trialPeriodDays)
        {
            ValidationUtils.ValidatesAmount(amount);
            ValidationUtils.ValidatesCurrency(currency);
            ValidationUtils.ValidatesInterval(interval);
            ValidationUtils.ValidatesName(name);
            ValidationUtils.ValidatesTrialPeriodDays(trialPeriodDays);

            return await createAsync(
                    new UrlEncoder().EncodeObject(new
                    {
                        Amount = amount,
                        Currency = currency,
                        Interval = interval,
                        Name = name,
                        Trial_Period_Days = trialPeriodDays
                    }));
        }
        protected override string GetResourceId(Offer obj)
        {
            return obj.Id;
        }


    }
}
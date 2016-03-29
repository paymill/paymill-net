using PaymillWrapper.Models;
using PaymillWrapper.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
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
        /// <param name="interval">Defining how often the Client should be charged</param>
        /// <param name="name">Your name for this offer</param>
        /// <returns>Object with id, which represents a PAYMILL offer.</returns>
        public async Task<Offer> CreateAsync(int amount, String currency, Interval.Period interval, String name)
        {
            return await CreateAsync(amount, currency, interval, name, null);
        }

        /// <summary>
        /// Creates an offer via the API.
        /// </summary>
        /// <param name="amount">Amount in cents > 0.</param>
        /// <param name="currency">ISO 4217 formatted currency code.</param>
        /// <param name="interval">Defining how often the Client should be charged.</param>
        /// <param name="name">Your name for this offer</param>
        /// <param name="trialPeriodDays">Give it a try or charge directly. Can be null</param>
        /// <returns>Object with id, which represents a PAYMILL offer</returns>
        public async Task<Offer> CreateAsync(int amount, String currency, Interval.Period interval, String name, int? trialPeriodDays)
        {
            ValidationUtils.ValidatesAmount(amount);
            ValidationUtils.ValidatesCurrency(currency);
            ValidationUtils.ValidatesIntervalPeriod(interval);
            ValidationUtils.ValidatesName(name);
            ValidationUtils.ValidatesTrialPeriodDays(trialPeriodDays);

            return await createAsync(null,
                    new UrlEncoder().EncodeObject(new
                    {
                        Amount = amount,
                        Currency = currency,
                        Interval = interval.ToString(),
                        Name = name,
                        Trial_Period_Days = trialPeriodDays
                    }));
        }
        /// <summary>
        /// This function returns a <see cref="PaymillList"/>of PAYMILL Offer objects. In which order this list is returned depends on the
        /// </summary>
        /// <param name="filter">Filter or null</param>
        /// <param name="order">Order or null.</param>
        /// <returns>PaymillList which contains a List of PAYMILL object and their total count.</returns>
        public async Task<PaymillWrapper.Models.PaymillList<Offer>> ListAsync(Offer.Filter filter, Offer.Order order)
        {
            return await base.listAsync(filter, order, null, null);
        }
        public override async Task<bool> DeleteAsync(string id)
        {
            throw new PaymillWrapper.Exceptions.PaymillException("Depricated");
        }
        public override async Task<bool> DeleteAsync(Offer offer)
        {
            throw new PaymillWrapper.Exceptions.PaymillException("Depricated");
        }
        public virtual async Task<bool> DeleteAsync(string id, Boolean removeWithSubscriptions)
        {
            return await deleteParamBoolAsync(id,
                new
                {
                    remove_with_subscriptions = removeWithSubscriptions.ToString().ToLower()
                });
        }
        public virtual async Task<bool> DeleteAsync(Offer offer, Boolean removeWithSubscriptions)
        {
            return await deleteParamBoolAsync(offer.Id,
                new
                {
                    remove_with_subscriptions = removeWithSubscriptions.ToString().ToLower()
                });
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
        public async Task<PaymillWrapper.Models.PaymillList<Offer>> ListAsync(Offer.Filter filter, Offer.Order order, int? count, int? offset)
        {
            return await base.listAsync(filter, order, count, offset);
        }
        public virtual async Task<Offer> UpdateAsync(Offer obj, Boolean updateSubscriptions)
        {
            var encoder = new UrlEncoder();
            String param = encoder.EncodeObject(new
            {
                update_subscriptions = updateSubscriptions.ToString().ToLower()
            });
            var content = encoder.EncodeUpdate(obj);
            return await updateWithContentAsync(obj.Id,
               String.Format("{0}&{1}", content, param));
        }
        protected override string GetResourceId(Offer obj)
        {
            return obj.Id;
        }


    }
}

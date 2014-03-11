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
/*
    public class OfferService : AbstractService<Offer>
    {
        public OfferService(HttpClientRest client):base(client)
        {
        }


        /// <summary>
        /// To get the details of an existing offer you’ll need to supply the offer ID
        /// </summary>
        /// <param name="clientID">Offer identifier</param>
        /// <returns>Offer-object</returns>
        public Offer Get(string offerID)
        {
            return get<Offer>(Resource.Offers, offerID);
        }

        /// <summary>
        /// This function deletes a offer
        /// </summary>
        /// <param name="clientID">Offer identifier</param>
        /// <returns>Return true if remove was ok, false if not possible</returns>
        public bool Remove(string offerID)
        {
            return remove<Offer>(Resource.Offers, offerID);
        }

        /// <summary>
        /// This function updates the data of a offer
        /// </summary>
        /// <param name="client">Object-offer</param>
        /// <returns>Object-offer just updated</returns>
        public Offer Update(Offer offer)
        {
            return update<Offer>(
                Resource.Offers,
                offer,
                offer.Id,
                new URLEncoder().EncodeOfferUpdate(offer));
        }
*/
    public class OfferService : AbstractService<Offer>
    {
        public OfferService(HttpClient client, string apiUrl)
            : base(Resource.Offers, client, apiUrl)
        {
        }
       
        /// <summary>
        /// This function allows request a offer list
        /// </summary>
        /// <returns>Returns a list offers-object</returns>
        public async Task< List<Offer>> GetOffers()
        {
            return await ListAsync();
        }

        /// <summary>
        /// This function allows request a offer list
        /// </summary>
        /// <param name="filter">Result filtered in the required way</param>
        /// <returns>Returns a list offer-object</returns>
        public async Task< List<Offer> > GetOffersByFilter(Filter filter)
        {
            return await ListAsync(filter);
        }

        /// <summary>
        /// This function creates a offer object
        /// </summary>
        /// <param name="client">Object-offer</param>
        /// <returns>New object-offer just add</returns>
        public Offer Create(Offer offer)
        {
            /*
            return Create(
                null,
                new UrlEncoder().EncodeOfferAdd(offer));
             * */
            return null;
        }
        protected override string GetResourceId(Offer obj)
        {
            return obj.Id;
        }


    }
}
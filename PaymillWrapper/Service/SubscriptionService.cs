using System.Net.Http;
using PaymillWrapper.Models;
using PaymillWrapper.Net;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace PaymillWrapper.Service
{
    public class SubscriptionService : AbstractService<Subscription>
    {
        public SubscriptionService(HttpClient client, string apiUrl)
            : base(Resource.Subscriptions, client, apiUrl)
        {
        }


        protected override string GetResourceId(Subscription obj)
        {
            return obj.Id;
        }

        /// <summary>
        /// This function creates a <see cref="Subscription" /> between a <see cref="Client" /> and an <see cref="Offer" />. 
        /// A <see cref="Client" /> can have several
        /// <see cref="Subscription" />s to different <see cref="Offer" />s, but only one <see cref="Subscription" />
        /// to the same <see cref="Offer" />. The
        /// <see cref="Client" />s is charged for each billing interval entered. 
        /// NOTE:
        /// This call will use the <see cref="Client" /> from the <see cref="Payment" /> object.
        /// </summary>
        /// <param name="offer">An Offer to subscribe to.</param>
        /// <param name="payment">A Payment used for charging.</param>
        /// <returns>Subscription which allows you to charge recurring payments.</returns>
        public async Task<Subscription> CreateWithOfferAndPaymentAsync(Offer offer, Payment payment)
        {
            return await CreateWithOfferAndPaymentAsync(offer, payment, null);
        }
        /// <summary>
        /// This function creates a <see cref="Subscription" /> between a <see cref="Client} and an <see cref="Offer" />. A <see cref="Client" /> can have several
        /// <see cref="Subscription" />s to different <see cref="Offer" />s, but only one <see cref="Subscription" /> to the same <see cref="Offer" />. The
        /// <see cref="Client" />s is charged for each billing interval entered.
        /// NOTE:
        /// This call will use the <see cref="Client" /> from the <see cref="Payment" /> object.
        /// </summary>
        /// <param name="offerId">An Offer" to subscribe to</param>
        /// <param name="paymentId">A Payment" used for charging..</param>
        /// <returns><see cref="Subscription" />, which allows you to charge recurring payments.</returns>
        public async Task<Subscription> CreateWithOfferAndPaymentAsync(String offerId, String paymentId)
        {

            return await CreateWithOfferAndPaymentAsync(new Offer(offerId), new Payment(paymentId));
        }

        /// <summary>
        /// This function creates a <see cref="Subscription" /> between a <see cref="Client" /> 
        /// and an <see cref="Offer" />. A <see cref="Client" /> can have several
        /// <see cref="Subscription" />s to different <see cref="Offer}s, but only one <see cref="Subscription" /> 
        /// to the same <see cref="Offer" />. The
        /// <see cref="Client" />s is charged for each billing interval entered. 
        ///  This call will use the <see cref="Client" /> from the <see cref="Payment" /> object.
        /// </summary>
        /// <param name="offerId">The Id of an Offe to subscribe to.</param>
        /// <param name="paymentId">The Id of a Payment used for charging.</param>
        /// <param name="trialStart">Date representing trial period start.</param>
        /// <returns>Subscription which allows you to charge recurring payments.</returns>
        public async Task<Subscription> CreateWithOfferAndPaymentAsync(String offerId, String paymentId, DateTime? trialStart)
        {
            return await CreateWithOfferAndPaymentAsync(new Offer(offerId), new Payment(paymentId), trialStart);
        }


        /// <summary>
        /// This function creates a <see cref="Subscription" /> 
        /// between a <see cref="Client" /> and an <see cref="Offer" />. A <see cref="Client" /> can have several
        ///  <see cref="Subscription" />s to different <see cref="Offer}s, but only one <see cref="Subscription" /> 
        ///  to the same <see cref="Offer" />. The
        ///  <see cref="Client" />s is charged for each billing interval entered.
        ///  NOTE:
        ///  This call will use the <see cref="Client" /> from the <see cref="Payment" /> object.
        /// </summary>
        /// <param name="offer">An Offer to subscribe to.</param>
        /// <param name="payment">A <see Payment used for charging.</param>
        /// <param name="trialStart">Date representing trial period start.</param>
        /// <returns>Subscription, which allows you to charge recurring payments.</returns>
        public async Task<Subscription> CreateWithOfferAndPaymentAsync(Offer offer, Payment payment, DateTime? trialStart)
        {
            ValidationUtils.ValidatesOffer(offer);
            ValidationUtils.ValidatesPayment(payment);

            return await createAsync(new UrlEncoder().EncodeObject(new { Offer = offer.Id, Payment = payment.Id, Start_At = trialStart }));
        }


        /// <summary>
        /// This function creates a <see cref="Subscription" /> between a <see cref="Client" /> and an <see cref="Offer" />. 
        /// A <see cref="Client" /> can have several
        /// <see cref="Subscription" />s to different <see cref="Offer" />s, but only one <see cref="Subscription" /> 
        /// to the same <see cref="Offer" />. The
        /// <see cref="Client" />s is charged for each billing interval entered. 
        /// NOTE:
        /// If <see cref="Client" /> not provided the <see cref="Client" /> from the payment is being used..
        /// </summary>
        /// <param name="offer">An Offer to subscribe to.</param>
        /// <param name="payment">A Payment used for charging..</param>
        /// <param name="client">A Client to subscribe.</param>
        /// <returns>Subscription, which allows you to charge recurring payments.</returns>
        public async Task<Subscription> CreateWithOfferPaymentAndClientAsync(Offer offer, Payment payment, Client client)
        {
            return await CreateWithOfferPaymentAndClientAsync(offer, payment, client, null);
        }


        /// <summary>
        /// This function creates a <see cref="Subscription" /> between a <see cref="Client" /> and an <see cref="Offer}. A <see cref="Client" /> can have several
        /// <see cref="Subscription" />s to different <see cref="Offer" />s, but only one <see cref="Subscription" /> to the same <see cref="Offer" />. The
        /// <see cref="Client" />s is charged for each billing interval entered. <br />
        /// NOTE:
        /// If <see cref="Client" /> not provided the <see cref="Client" /> from the payment is being used.
        /// </summary>
        /// <param name="offerId">The Id of an Offer to subscribe to.</param>
        /// <param name="paymentId">The Id of a Payment used for charging.</param>
        /// <param name="clientId">The Id of a Client to subscribe.</param>
        /// <returns>Subscription, which allows you to charge recurring payments.</returns>
        public async Task<Subscription> CreateWithOfferPaymentAndClientAsync(String offerId, String paymentId, String clientId)
        {
            return await CreateWithOfferPaymentAndClientAsync(new Offer(offerId), new Payment(paymentId), new Client(clientId), null);
        }


        /// <summary>
        /// This function creates a <see cref="Subscription" /> between a <see cref="Client} and an <see cref="Offer" />. 
        /// A <see cref="Client" /> can have several
        ///  <see cref="Subscription" />s to different <see cref="Offer" />s, 
        ///  but only one <see cref="Subscription" /> to the same <see cref="Offer}. The
        ///  <see cref="Client" />s is charged for each billing interval entered. <br />
        ///  NOTE:
        ///  If <see cref="Client" /> not provided the <see cref="Client" /> from the payment is being used.
        /// </summary>
        /// <param name="offerId">The Id of an Offer to subscribe to.</param>
        /// <param name="paymentId">The Id of a Payment used for charging.</param>
        /// <param name="clientId"> The Id of a Client to subscribe.</param>
        /// <param name="trialStart">Date representing trial period start.</param>
        /// <returns>Subscription, which allows you to charge recurring payments.</returns>
        public async Task<Subscription> CreateWithOfferPaymentAndClientAsync(String offerId, String paymentId, String clientId, DateTime? trialStart)
        {
            return await CreateWithOfferPaymentAndClientAsync(new Offer(offerId), new Payment(paymentId), new Client(clientId), trialStart);
        }

        /// <summary>
        /// This function creates a <see cref="Subscription" /> between a <see cref="Client" /> and an <see cref="Offer" />.
        /// A <see cref="Client" /> can have several
        /// <see cref="Subscription" />s to different <see cref="Offer" />s, 
        /// but only one <see cref="Subscription" /> to the same <see cref="Offer" />. The
        /// <see cref="Client" />s is charged for each billing interval entered. 
        /// NOTE:
        /// If <see cref="Client" /> not provided the <see cref="Client" /> from the payment is being used.
        /// </summary>
        /// <param name="offer">An Offer to subscribe to.</param>
        /// <param name="payment">A Payment used for charging.</param>
        /// <param name="client">A Client to subscribe.</param>
        /// <param name="trialStart">Date representing trial period start.</param>
        /// <returns>Subscription, which allows you to charge recurring payments.</returns>
        public async Task<Subscription> CreateWithOfferPaymentAndClientAsync(Offer offer, Payment payment, Client client, DateTime? trialStart)
        {
            ValidationUtils.ValidatesOffer(offer);
            ValidationUtils.ValidatesPayment(payment);
            ValidationUtils.ValidatesClient(client);


            return await createAsync(new UrlEncoder().EncodeObject(new { Offer = offer.Id, Payment = payment.Id, Client = client.Id, Start_At = trialStart }));
        }
    }
}

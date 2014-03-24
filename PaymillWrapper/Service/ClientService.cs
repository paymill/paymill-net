using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using PaymillWrapper.Models;
using PaymillWrapper.Utils;
using System.Threading.Tasks;

namespace PaymillWrapper.Service
{

    public class ClientService : AbstractService<Client>
    {
        public ClientService(HttpClient client,
            string apiUrl)
            : base(Resource.Clients, client, apiUrl)
        {

        }
        /// <summary>
        /// Creates Client instance.
        /// </summary>
        /// <returns>
        /// New object-client just add
        /// </returns>
        public async Task<Client> CreateAsync()
        {
            return await CreateWithEmailAndDescriptionAsync(null, null);
        }
        /// <summary>
        /// Creates Client instance with the given description.
        /// </summary>
        /// <param name="description">The description.</param>
        /// <returns>
        /// New object-client just add
        /// </returns>
        public async Task<Client> CreateWithDescriptionAsync(String description)
        {

            return await CreateWithEmailAndDescriptionAsync(null, description);
        }
        /// <summary>
        /// Creates Client instance with the given email.
        /// </summary>
        /// <param name="email">Mail address for the Client or null</param>
        /// <returns>
        /// New object-client just add
        /// </returns>
        public async Task<Client> CreateWithEmailAsync(String email)
        {

            return await CreateWithEmailAndDescriptionAsync(email, null);
        }
        /// <summary>
        /// Creates Client instance with the given description and email.
        /// </summary>
        /// <param name="email">Object-client</param>
        /// <param name="description">Object-client</param>
        /// <returns>
        /// New object-client just add
        /// </returns>
        public async Task<Client> CreateWithEmailAndDescriptionAsync(String email, String description)
        {
            return await createAsync(null, 
                new UrlEncoder().EncodeObject(new { Email = email, Description = description }));
        }
        /// <summary>
        /// This function returns a <see cref="PaymillList"/>of PAYMILL Client objects. In which order this list is returned depends on the
        /// </summary>
        /// <param name="filter">Filter or null</param>
        /// <param name="order">Order or null.</param>
        /// <returns>PaymillList which contains a List of PAYMILL Client object and their total count.</returns>
        public async Task<PaymillWrapper.Models.PaymillList<Client>> ListAsync(Client.Filter filter, Client.Order order)
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
        public async Task<PaymillWrapper.Models.PaymillList<Client>> ListAsync(Client.Filter filter, Client.Order order, int? count, int? offset)
        {
            return await base.listAsync(filter, order, count, offset);
        }
        protected override string GetResourceId(Client obj)
        {
            return obj.Id;
        }
    }
}
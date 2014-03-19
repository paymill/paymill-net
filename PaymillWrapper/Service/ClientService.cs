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
        protected override string GetResourceId(Client obj)
        {
            return obj.Id;
        }
    }
}
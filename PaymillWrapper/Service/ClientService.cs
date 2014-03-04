using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using PaymillWrapper.Models;
using PaymillWrapper.Net;

namespace PaymillWrapper.Service
{

    public class ClientService : AbstractService<Client>
    {
        public ClientService(HttpClient client,
            string apiUrl): base(Resource.Clients, client, apiUrl)
        {

        }
        /// <summary>
        /// This function creates a client object
        /// </summary>
        /// <param name="client">Object-client</param>
        /// <returns>New object-client just add</returns>
        public Client Create(String email, String description)
        {
            /*
            return Create(
                null,
                new UrlEncoder().EncodeObject(new { Email = email, Description = description }));
             */
            return null;
        }
        protected override string GetResourceId(Client obj)
        {
            return obj.Id;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using PaymillWrapper.Models;
using PaymillWrapper.Net;

namespace PaymillWrapper.Service
{
    /*
    public class ClientService : AbstractService<Client>
    {
        public ClientService(HttpClientRest client):base(client)
        {
        }
        
       
        
        /// <summary>
        /// To get the details of an existing client you’ll need to supply the client ID
        /// </summary>
        /// <param name="clientID">Client identifier</param>
        /// <returns>Client-object</returns>
        public Client Get(string clientID)
        {
            return get<Client>(Resource.Clients, clientID);
        }

        /// <summary>
        /// This function deletes a client, but your transactions aren’t deleted
        /// </summary>
        /// <param name="clientID">Client identifier</param>
        /// <returns>Return true if remove was ok, false if not possible</returns>
        public bool Remove(string clientID)
        {
            return remove<Client>(Resource.Clients, clientID);
        }

        /// <summary>
        /// This function updates the data of a client
        /// </summary>
        /// <param name="client">Object-client</param>
        /// <returns>Object-client just updated</returns>
        public Client Update(Client client)
        {
            return update<Client>(
                Resource.Clients,
                client,
                client.Id,
                new URLEncoder().EncodeClientUpdate(client));
        }
*/
    public class ClientService : AbstractService<Client>
    {
        public ClientService(HttpClient client, String apiUrl)
            : base(Resource.Clients, client, apiUrl)
        {
        }
        /// <summary>
        /// This function allows request a client list
        /// </summary>
        /// <returns>Returns a list clients-object</returns>
        public IReadOnlyCollection<Client> GetClients()
        {
            return getList();
        }

        /// <summary>
        /// This function allows request a client list
        /// </summary>
        /// <param name="filter">Result filtered in the required way</param>
        /// <returns>Returns a list client-object</returns>
        public IReadOnlyCollection<Client> GetClientsByFilter(Filter filter)
        {
            return getList(filter);
        }

        /// <summary>
        /// This function creates a client object
        /// </summary>
        /// <param name="client">Object-client</param>
        /// <returns>New object-client just add</returns>
        public Client Create(String email, String description)
        {
            return Create(
                null,
                new UrlEncoder().EncodeObject(new { Email = email, Description = description }));
        }
        protected override string GetResourceId(Client obj)
        {
            return obj.Id;
        }

        protected override string GetEncodedUpdateParams(Client obj, UrlEncoder encoder)
        {
            return encoder.EncodeClientUpdate(obj);
        }

    }
}
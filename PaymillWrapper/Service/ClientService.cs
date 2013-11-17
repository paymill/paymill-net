using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PaymillWrapper.Models;
using PaymillWrapper.Net;

namespace PaymillWrapper.Service
{
    public class ClientService : AbstractService<Client>
    {
        public ClientService(HttpClientRest client):base(client)
        {
        }
        
        /// <summary>
        /// This function allows request a client list
        /// </summary>
        /// <returns>Returns a list clients-object</returns>
        public List<Client> GetClients()
        {
            return getList<Client>(Resource.Clients);
        }

        /// <summary>
        /// This function allows request a client list
        /// </summary>
        /// <param name="filter">Result filtered in the required way</param>
        /// <returns>Returns a list client-object</returns>
        public List<Client> GetClientsByFilter(Filter filter)
        {
            return getList<Client>(Resource.Clients, filter);
        }

        /// <summary>
        /// This function creates a client object
        /// </summary>
        /// <param name="client">Object-client</param>
        /// <returns>New object-client just add</returns>
        public Client Create(String email, String description)
        {
            return create<Client>(
                Resource.Clients,
                null,
                new URLEncoder().EncodeObject( new {Email= email, Description = description}));
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
    }
}
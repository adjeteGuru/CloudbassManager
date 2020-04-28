
using Cloudbass.DataAccess.Repositories.Contracts;
using Cloudbass.Database;
using Cloudbass.Database.Models;
using HotChocolate;
using HotChocolate.Types;
using System.Linq;


namespace CloudbassManager.Queries
{
    [ExtendObjectType(Name = "Query")]
    public class ClientQuery
    {
        private readonly IClientRepository _clientRepository;
        public ClientQuery(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }
        /// <summary>
        /// Gets all clients.
        /// </summary>

        [UseFiltering]

        public IQueryable<Client> GetClients([Service] CloudbassContext db)
        {
            return db.Clients.AsQueryable();
        }
        //public IQueryable<Client> GetClients(CloudbassContext db)
        //{
        //    var db = new CloudbassContext();
        //    //db.Clients;
        //    var clientJobs = from x in db.Jobs
        //                     where x.ClientId == 1
        //                     select x.Client;
        //    IQueryable<Client> clients = (IQueryable<Client>)clientJobs;
        //}

        /// <summary>
        /// Gets a client by its id.
        /// </summary>

        public IQueryable<Client> GetClient([Service] CloudbassContext db, int id) =>

            db.Clients.Where(t => t.Id == id);

    }
}

using Cloudbass.DataAccess.Repositories.Contracts;
using Cloudbass.Database;
using Cloudbass.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cloudbass.DataAccess.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly CloudbassContext _db;
        public ClientRepository(CloudbassContext db)
        {
            _db = db;
        }
        public Client GetClient(int id)
        {
            return _db.Clients.SingleOrDefault(x => x.Id == id);
        }

        public IQueryable<Client> GetAll()
        {
            return _db.Clients;
        }


        //
        //public IEnumerable<object> Search(string text)
        //{
        //    IEnumerable<Client> filteredClients = _db.Clients
        //       .Where(t => t.Name.Contains(text,
        //           StringComparison.OrdinalIgnoreCase));

        //    foreach (Client client in filteredClients)
        //    {
        //        yield return client;
        //    }

        //}
    }
}


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
        public ClientQuery()
        {

        }
        /// <summary>
        /// Gets all clients.
        /// </summary>

        [UseFiltering]
        public IQueryable<Client> GetClients([Service] CloudbassContext db) =>

            db.Clients;

        /// <summary>
        /// Gets a client by its id.
        /// </summary>

        public IQueryable<Client> GetClient([Service] CloudbassContext db, int id) =>

            db.Clients.Where(t => t.Id == id);

    }
}

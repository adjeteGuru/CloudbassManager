using Cloudbass.Database;
using Cloudbass.Database.Models;
using HotChocolate;
using HotChocolate.Resolvers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cloudbass.Utilities.Resolvers
{
    //collaborators that we can associate with the fields of schema (type)
    //will take care of recovering data from these fields
    public class ClientResolvers
    {
        private readonly CloudbassContext _db;

        public ClientResolvers([Service] CloudbassContext db)
        {
            _db = db;
        }



        public Client GetClient(Job job, IResolverContext ctx)
        {
            return _db.Clients.Where(a => a.Id == job.ClientId).FirstOrDefault();
        }
    }


}

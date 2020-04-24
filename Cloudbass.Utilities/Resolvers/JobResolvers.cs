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
    public class JobResolvers
    {
        private readonly CloudbassContext _db;
        public JobResolvers([Service] CloudbassContext db)
        {
            _db = db;
        }



        public IEnumerable<Job> GetJobs(Client client, IResolverContext ctx)
        {
            return _db.Jobs.Where(a => a.ClientId == client.Id);
        }
    }
}

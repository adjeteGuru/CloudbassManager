using Cloudbass.DataAccess.Repositories;
using Cloudbass.Database;
using Cloudbass.Database.Models;
using HotChocolate;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudbassManager.Queries
{
    [ExtendObjectType(Name = "Query")]
    public class JobQuery
    {
        [UseFiltering]
        public IQueryable<Job> GetJobs([Service] CloudbassContext db) => db.Jobs;

        public IQueryable<Job> GetJobsForClient([Service] CloudbassContext db, Guid clientId)
        {
            return db.Jobs.Where(x => x.ClientId == clientId);

        }


        public IQueryable<Job> GetJobsForClient([Service] CloudbassContext db, Guid clientId, int lastJob)
        {
            return db.Jobs.Where(x => x.ClientId == clientId)
                 //by decending order
                 .OrderByDescending(x => x.CreatedAt)
                 //take as many as client want
                 .Take(lastJob);
        }
    }
}

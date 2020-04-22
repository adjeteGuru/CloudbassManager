using Cloudbass.DataAccess.Repositories.Contracts;
using Cloudbass.Database;
using Cloudbass.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cloudbass.DataAccess.Repositories
{
    public class JobRepository : IJobRepository
    {
        private readonly CloudbassContext _db;
        public JobRepository(CloudbassContext db)
        {
            _db = db;
        }

        public IEnumerable<Job> GetJobsForClient(int clientId)
        {
            return _db.Jobs.Where(x => x.ClientId == clientId);
        }

        public IEnumerable<Job> GetJobsForClient(int clientId, int lastJob)
        {
            return _db.Jobs.Where(x => x.ClientId == clientId)
                 //by decending order
                 .OrderByDescending(x => x.CreatedAt)
                 //take as many as client want
                 .Take(lastJob);
        }


        //
        public IEnumerable<object> Search(string text)
        {
            IEnumerable<Job> filteredJobs = _db.Jobs
               .Where(t => t.Text.Contains(text,
                   StringComparison.OrdinalIgnoreCase));

            foreach (Job job in filteredJobs)
            {
                yield return job;
            }

        }

    }
}


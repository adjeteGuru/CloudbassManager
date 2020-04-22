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
        public IQueryable<Job> GetJobsForClient([Service] CloudbassContext db, int clientId)
        {
            return db.Jobs.Where(x => x.ClientId == clientId);

        }

        //public IEnumerable<Job> GetJobsForClient(int[] jobIds,JobRepository jobRepository /*CloudbassContext db*/)
        //{
        //    foreach (int jobId in jobIds)
        //    {
        //        Job job = jobRepository.GetJobsForClient(jobId);
        //        if (job == null)
        //        {
        //            jobRepository.ReportError(
        //                "Could not resolve a charachter for the " +
        //                $"character-id {jobId}.");
        //        }
        //        else
        //        {
        //            yield return job;
        //        }
        //    }
        //}

        public IQueryable<Job> GetJobsForClient([Service] CloudbassContext db, int clientId, int lastJob)
        {
            return db.Jobs.Where(x => x.ClientId == clientId)
                 //by decending order
                 .OrderByDescending(x => x.CreatedAt)
                 //take as many as client want
                 .Take(lastJob);
        }
    }
}

using Cloudbass.Database.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cloudbass.DataAccess.Repositories.Contracts
{
    public interface IJobRepository
    {
        IEnumerable<Job> GetJobs();
        //IEnumerable<Job> GetJobsForClient(int clientId);
        //IEnumerable<Job> GetJobsForClient(int clientId, int lastJob);

        Job Create(CreateJobInput input);
        Job Delete(DeleteJobInput input);

    }
}

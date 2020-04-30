using Cloudbass.Database;
using Cloudbass.Database.Models;
using HotChocolate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cloudbass.DataAccess.Repositories.Contracts
{
    public interface IJobRepository
    {
        IQueryable<Job> GetAll();
        //IEnumerable<Job> GetJobsForClient(int clientId);
        //IEnumerable<Job> GetJobsForClient(int clientId, int lastJob);

        Job Create(CreateJobInput input);
        Job Delete(DeleteJobInput input);
        Job Update(UpdateJobInput input, int id);
    }
}

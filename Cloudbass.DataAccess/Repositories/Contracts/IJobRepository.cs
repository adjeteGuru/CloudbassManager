using Cloudbass.Database;
using Cloudbass.Database.Models;
using HotChocolate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cloudbass.DataAccess.Repositories.Contracts
{
    public interface IJobRepository
    {
        IQueryable<Job> GetAllJobs();

        Task<Job> GetJobAsync(Guid jobId);
        Task<IEnumerable<Job>> GetJobByClientIdAsync(Guid clientId);
        Task<ILookup<Guid, Job>> GetJobsByClientIdAsync(
            IEnumerable<Guid> clientIds, CancellationToken cancellationToken);
        Task<IReadOnlyDictionary<Guid, Job>> GetJobsAsync(
            IReadOnlyList<Guid> ids, CancellationToken cancellationToken);
        Task<Job> CreateJobAsync(Job job, CancellationToken cancellationToken);


        //IEnumerable<Job> GetJobsForClient(int clientId);
        //IEnumerable<Job> GetJobsForClient(int clientId, int lastJob);

        //Job Create(CreateJobInput input);
        //Job Delete(DeleteJobInput input);
        //Job Update(UpdateJobInput input, Guid id);

    }
}

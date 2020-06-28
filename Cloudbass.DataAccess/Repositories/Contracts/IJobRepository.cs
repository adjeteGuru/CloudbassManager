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
        IQueryable<Job> GetJobs();

        Task<Job> GetJobByIdAsync(Guid jobId);
        Task<IEnumerable<Job>> GetJobsByClientIdAsync(int clientId);
        Task<ILookup<Guid, Job>> GetJobsByClientIdAsync(IEnumerable<int> clientIds);
        Task<IReadOnlyDictionary<Guid, Job>> GetJobsAsync(
            IReadOnlyList<Guid> ids, CancellationToken cancellationToken);
        Task<Job> CreateJobAsync(Job job);

        //Task UpdateJobAsync(Job job, CancellationToken cancellationToken);

        //IEnumerable<Job> GetJobsForClient(int clientId);
        //IEnumerable<Job> GetJobsForClient(int clientId, int lastJob);

        //Job Create(CreateJobInput input);
        //Job Delete(DeleteJobInput input);
        //Job Update(UpdateJobInput input, Guid id);

    }
}

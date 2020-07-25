using Cloudbass.DataAccess.Repositories.Contracts;
using Cloudbass.Database.Models;
using GreenDonut;
using HotChocolate.DataLoader;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cloudbass.Types.Jobs
{
    public class JobByIdDataLoader : BatchDataLoader<Guid, Job>
    {

        private readonly IJobRepository _jobRepository;
        public JobByIdDataLoader(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }


        protected override async Task<IReadOnlyDictionary<Guid, Job>> LoadBatchAsync(
        IReadOnlyList<Guid> keys, CancellationToken cancellationToken)
        {
            return await _jobRepository
            .GetJobsByIdAsync(keys, cancellationToken)
            .ConfigureAwait(false);
        }
    }
}


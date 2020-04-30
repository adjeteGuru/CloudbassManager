using Cloudbass.DataAccess.Repositories.Contracts;
using Cloudbass.Database;
using Cloudbass.Database.Models;
using Cloudbass.Types.Payload;
using HotChocolate;
using HotChocolate.Subscriptions;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudbassManager.Mutations
{
    [ExtendObjectType(Name = "Mutation")]
    public class JobMutations
    {
        private readonly IJobRepository _jobRepository;
        public JobMutations(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public Job CreateJob(CreateJobInput input)
        {
            return _jobRepository.Create(input);
        }

        public Job DeleteJob(DeleteJobInput input)
        {
            return _jobRepository.Delete(input);

        }

        public Job UpdateJob(UpdateJobInput input, int? id)
        {
            if (input is null && id == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            //return _jobRepository.Update(input);
            return _jobRepository.Update(id: input.Id);
        }
    }
}

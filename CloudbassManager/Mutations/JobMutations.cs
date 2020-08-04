using Cloudbass.DataAccess.Repositories.Contracts;
using Cloudbass.Database;
using Cloudbass.Database.Models;
using Cloudbass.Types.Input.Job;
using Cloudbass.Types.Payload;
using HotChocolate;
using HotChocolate.Subscriptions;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CloudbassManager.Mutations
{
    [ExtendObjectType(Name = "Mutation")]
    public class JobMutations
    {
        private readonly IJobRepository _jobRepository;
        public JobMutations(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository ?? throw new ArgumentNullException(nameof(jobRepository));
        }



        public async Task<Job> CreateJobAsync(JobInput input,
            //[Service] IJobRepository jobRepository,
            [Service] ITopicEventSender eventSender
            /*CancellationToken cancellationToken*/)
        {
            var addedJob = new Job
            {

                Name = input.Name,
                Description = input.Description,
                Location = input.Location,
                //CreatedAt = input.CreatedAt,
                StartDate = input.StartDate,
                EndDate = input.EndDate,
                TXDate = input.TXDate,
                Paid = input.Paid,
                Coordinator = input.Coordinator,
                CommercialLead = input.CommercialLead,
                ClientId = input.ClientId,
                Status = input.Status
            };


            await
            _jobRepository.CreateJobAsync(addedJob/*, cancellationToken*/);

            await eventSender.SendAsync("CreateJob", addedJob);

            return addedJob;

        }

        //public Job CreateJob(CreateJobInput input)
        //{
        //    return _jobRepository.Create(input);
        //}

        //public Job DeleteJob(DeleteJobInput input)
        //{
        //    return _jobRepository.Delete(input);
        //}


        //public Job UpdateJob(UpdateJobInput input, Guid id)
        //{
        //    return _jobRepository.Update(input, id);
        //}
    }
}

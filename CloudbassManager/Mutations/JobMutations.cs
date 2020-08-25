using Cloudbass.DataAccess.Repositories.Contracts;
using Cloudbass.Database;
using Cloudbass.Database.Models;
using Cloudbass.Types.Jobs;
using Cloudbass.Types.Payload;
using HotChocolate;
using HotChocolate.Execution;
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
        //CREATE
        public async Task<CreateJobPayload> AddJobAsync(
            CreateJobInput input,
            [Service] ITopicEventSender eventSender,
             [Service] IJobRepository jobRepository,
            CancellationToken cancellationToken)
        {

            var addedJob = new Job
            {

                Name = input.Name,
                Description = input.Description,
                Location = input.Location,
                StartDate = input.StartDate,
                EndDate = input.EndDate,
                TXDate = input.TXDate,
                Paid = input.Paid,
                Coordinator = input.Coordinator,
                CommercialLead = input.CommercialLead,
                ClientId = input.ClientId,
                Status = input.Status,
                CreatedBy = input.CreatedBy
            };


            await jobRepository.CreateJobAsync(addedJob, cancellationToken).ConfigureAwait(false);

            await eventSender.SendAsync(addedJob, cancellationToken).ConfigureAwait(false);

            return new CreateJobPayload(addedJob);
        }


        //UPDATE
        public async Task<UpdateJobPayload> UpdateJobAsync(
            UpdateJobInput input, Guid id,
           [Service] ITopicEventSender eventSender,
           [Service] IJobRepository jobRepository,
           CancellationToken cancellationToken)
        {

            var jobToUpdate = await jobRepository.GetJobByIdAsync(id);



            if (jobToUpdate == null)
            {
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("matching Job id not found in database.")
                        .SetCode("JOB_NOT_FOUND")
                        .Build());
            }

            if (!string.IsNullOrWhiteSpace(input.Name))
            {
                jobToUpdate.Name = input.Name;
            }

            if (!string.IsNullOrWhiteSpace(input.Description))
            {
                jobToUpdate.Description = input.Description;
            }

            if (!string.IsNullOrWhiteSpace(input.Location))
            {
                jobToUpdate.Location = input.Location;
            }


            if (!string.IsNullOrWhiteSpace(input.Coordinator))
            {
                jobToUpdate.Coordinator = input.Coordinator;
            }

            if (!string.IsNullOrWhiteSpace(input.CommercialLead))
            {
                jobToUpdate.CommercialLead = input.CommercialLead;
            }



            if (!string.IsNullOrWhiteSpace(input.Status.ToString()))
            {
                jobToUpdate.Status = input.Status;
            }

            if (input.StartDate != null)
            {
                jobToUpdate.StartDate = input.StartDate;
            }

            if (input.TXDate != null)
            {
                jobToUpdate.TXDate = input.TXDate;
            }

            if (input.EndDate != null)
            {
                jobToUpdate.EndDate = input.EndDate;
            }

            bool? paid = false;
            if (paid == true)
            {
                jobToUpdate.Paid = input.Paid;
            }

            if (!string.IsNullOrWhiteSpace(input.CreatedBy))
            {
                jobToUpdate.CreatedBy = input.CreatedBy;
            }


            await jobRepository.UpdateJobAsync(jobToUpdate, cancellationToken).ConfigureAwait(false);

            await eventSender.SendAsync(jobToUpdate, cancellationToken).ConfigureAwait(false);

            return new UpdateJobPayload(jobToUpdate);

        }

    }
}

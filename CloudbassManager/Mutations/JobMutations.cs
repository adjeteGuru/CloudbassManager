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
        public async Task<CreateJobPayload> CreateJob(CreateJobInput inputJob, [Service] CloudbassContext db, [Service] ITopicEventSender eventSender)
        {
            var job = new Job
            {
                Text = inputJob.Text,

                Description = inputJob.Description,
                Location = inputJob.Location,
                CreatedAt = inputJob.CreatedAt,
                StartDate = inputJob.StartDate,
                EndDate = inputJob.EndDate,
                TXDate = inputJob.TXDate,
                Paid = inputJob.Paid,
                Coordinator = inputJob.Coordinator,
                CommercialLead = inputJob.CommercialLead,
                ClientId = inputJob.ClientId,
                Status = inputJob.Status

            };

            db.Jobs.Add(job);
            await db.SaveChangesAsync();
            await eventSender.SendAsync("CreateJob", job);
            return new CreateJobPayload(job);

        }
    }
}

using Cloudbass.DataAccess.Repositories.Contracts;
using Cloudbass.Database.Models;
using Cloudbass.Types.Schedules;
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
    public class ScheduleMutations
    {
        //CREATE
        public async Task<CreateSchedulePayload> AddScheduleAsync(
            [Service] IScheduleRepository scheduleRepository,
            [Service] ITopicEventSender eventSender,
            CreateScheduleInput input,
            CancellationToken cancellationToken
            )
        {

            var addedSchedule = new Schedule
            {
                Name = input.Name,
                Description = input.Description,
                StartDate = input.StartDate,
                EndDate = input.EndDate,
                Status = input.Status,
                JobId = input.JobId
            };

            await scheduleRepository.CreateScheduleAsync(addedSchedule, cancellationToken).ConfigureAwait(false);

            await eventSender.SendAsync(addedSchedule, cancellationToken).ConfigureAwait(false);

            return new CreateSchedulePayload(addedSchedule);
        }


        //UPDATE
        public async Task<UpdateSchedulePayload> UpdateScheduleAsync(
            UpdateScheduleInput input, Guid id,
           [Service] ITopicEventSender eventSender,
           [Service] IScheduleRepository scheduleRepository,
           CancellationToken cancellationToken)
        {

            var scheduleToUpdate = await scheduleRepository.GetScheduleByIdAsync(id);



            if (scheduleToUpdate == null)
            {
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("matching schedule id not found in database.")
                        .SetCode("SCHEDULE_NOT_FOUND")
                        .Build());
            }

            if (!string.IsNullOrWhiteSpace(input.Name))
            {
                scheduleToUpdate.Name = input.Name;
            }

            if (!string.IsNullOrWhiteSpace(input.Description))
            {
                scheduleToUpdate.Description = input.Description;
            }

            //if (!Guid(input.JobId))
            //{
            //    scheduleToUpdate.JobId = input.JobId;
            //}



            if (!string.IsNullOrWhiteSpace(input.Status.ToString()))
            {
                scheduleToUpdate.Status = input.Status;
            }

            if (input.StartDate != null)
            {
                scheduleToUpdate.StartDate = input.StartDate;
            }


            if (input.EndDate != null)
            {
                scheduleToUpdate.EndDate = input.EndDate;
            }


            await scheduleRepository.UpdateScheduleAsync(scheduleToUpdate, cancellationToken).ConfigureAwait(false);

            await eventSender.SendAsync(scheduleToUpdate, cancellationToken).ConfigureAwait(false);

            return new UpdateSchedulePayload(scheduleToUpdate);

        }

    }
}
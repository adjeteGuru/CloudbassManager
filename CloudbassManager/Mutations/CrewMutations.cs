using Cloudbass.DataAccess.Repositories.Contracts;
using Cloudbass.Database.Models;
using Cloudbass.Types.Crews;
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
    public class CrewMutations
    {

        //CREATE
        public async Task<CreateCrewPayload> AddCrewAsync(
            [Service] ICrewRepository crewRepository,
           [Service] ITopicEventSender eventSender,
           CreateCrewInput input,
           CancellationToken cancellationToken)
        {
            var addedCrew = new Crew
            {
                HasRoleId = input.HasRoleId,
                JobId = input.JobId,
                TotalDays = input.TotalDays

            };

            await crewRepository.CreateCrewAsync(addedCrew, cancellationToken).ConfigureAwait(false);

            await eventSender.SendAsync(addedCrew, cancellationToken).ConfigureAwait(false);

            return new CreateCrewPayload(addedCrew);

        }


        //update
        public async Task<UpdateCrewPayload> UpdateCrewAsync(
          [Service] ICrewRepository crewRepository,
          [Service] ITopicEventSender eventSender,
          UpdateCrewInput input, CancellationToken cancellationToken)
        {
            var crewToUpdate = await crewRepository.GetCrewMemberByIdAsync(input.HasRoleId, input.JobId);


            if (crewToUpdate.HasRoleId == null || crewToUpdate.JobId == null)
            {
                throw new QueryException(
                       ErrorBuilder.New()
                           .SetMessage("matching crew id not found in database.")
                           .SetCode("CREW_NOT_FOUND")
                           .Build());
            }




            if (crewToUpdate.TotalDays.HasValue)
            {
                crewToUpdate.TotalDays = input.TotalDays;
            }


            await crewRepository.UpdateCrewAsync(crewToUpdate, cancellationToken).ConfigureAwait(false);

            await eventSender.SendAsync(crewToUpdate, cancellationToken).ConfigureAwait(false);

            return new UpdateCrewPayload(crewToUpdate);

        }


        ////delete

        //public async Task<Crew> DeleteCrewAsync(
        //   [Service] ICrewRepository crewRepository,
        //   [Service] ITopicEventSender eventSender,
        //   DeleteCrewInput input, CancellationToken cancellationToken)
        //{
        //    var crewToDelete = await crewRepository.GetCrewMemberByIdAsync(input.HasRoleId, input.JobId);

        //    await crewRepository.DeleteCrewAsync(crewToDelete, cancellationToken).ConfigureAwait(false);

        //    await eventSender.SendAsync(crewToDelete, cancellationToken).ConfigureAwait(false);

        //    return crewToDelete;

        //}
    }
}

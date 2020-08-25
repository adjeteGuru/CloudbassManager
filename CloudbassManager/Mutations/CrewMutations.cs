﻿using Cloudbass.DataAccess.Repositories.Contracts;
using Cloudbass.Database.Models;
using Cloudbass.Types.Crews;
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
    public class CrewMutations
    {
        public async Task<CreateCrewPayload> AddCrewAsync(
            [Service] ICrewRepository crewRepository,
           [Service] ITopicEventSender eventSender,
           CreateCrewInput input,
           CancellationToken cancellationToken)
        {
            var addedCrew = new Crew
            {
                EmployeeId = input.EmployeeId,
                JobId = input.JobId,

            };

            await crewRepository.CreateCrewAsync(addedCrew, cancellationToken).ConfigureAwait(false);

            await eventSender.SendAsync(addedCrew, cancellationToken).ConfigureAwait(false);

            return new CreateCrewPayload(addedCrew);

        }
    }
}

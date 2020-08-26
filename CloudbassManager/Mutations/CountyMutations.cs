using Cloudbass.DataAccess.Repositories.Contracts;
using Cloudbass.Database.Models;
using Cloudbass.Types.Counties;
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
    public class CountyMutations
    {
        public async Task<CreateCountyPayload> AddCountyAsync(
             [Service] ICountyRepository countyRepository,
            [Service] ITopicEventSender eventSender,
            CreateCountyInput input,
            CancellationToken cancellationToken)
        {
            var addedCounty = new County
            {
                Name = input.Name,

            };

            await countyRepository.CreateCountyAsync(addedCounty, cancellationToken).ConfigureAwait(false);

            await eventSender.SendAsync(addedCounty, cancellationToken).ConfigureAwait(false);

            return new CreateCountyPayload(addedCounty);

        }

        //UPDATE
        public async Task<UpdateCountyPayload> UpdateCountyAsync(
            UpdateCountyInput input, Guid id,
           [Service] ITopicEventSender eventSender,
           [Service] ICountyRepository countyRepository,
           CancellationToken cancellationToken)
        {

            var countyToUpdate = await countyRepository.GetCountyByIdAsync(id);



            if (countyToUpdate == null)
            {
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("matching county id not found in database.")
                        .SetCode("COUNTY_NOT_FOUND")
                        .Build());
            }

            if (!string.IsNullOrWhiteSpace(input.Name))
            {
                countyToUpdate.Name = input.Name;
            }


            await countyRepository.UpdateCountyAsync(countyToUpdate, cancellationToken).ConfigureAwait(false);

            await eventSender.SendAsync(countyToUpdate, cancellationToken).ConfigureAwait(false);

            return new UpdateCountyPayload(countyToUpdate);

        }

    }
}

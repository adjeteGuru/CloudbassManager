using Cloudbass.DataAccess.Repositories.Contracts;
using Cloudbass.Database.Models;
using Cloudbass.Types.HasRoles;
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
    public class HasRoleMutations
    {
        //CREATE
        public async Task<CreateHasRolePayload> AddHasRoleAsync(CreateHasRoleInput input,
            [Service] IHasRoleRepository hasRoleRepository,
            [Service] ITopicEventSender eventSender,
            CancellationToken cancellationToken)
        {
            var addedHasRole = new HasRole
            {
                EmployeeId = input.EmployeeId,
                RoleId = input.RoleId,
                Rate = input.Rate,
                TotalDays = input.TotalDays
            };

            await hasRoleRepository.CreateHasRoleAsync(addedHasRole, cancellationToken).ConfigureAwait(false);
            await eventSender.SendAsync(addedHasRole, cancellationToken).ConfigureAwait(false);

            return new CreateHasRolePayload(addedHasRole);
        }

        //UPDATE

        public async Task<UpdateHasRolePayload> UpdateHasRoleAsync(UpdateHasRoleInput input, Guid id,
            [Service] IHasRoleRepository hasRoleRepository,
            [Service] ITopicEventSender eventSender,
            CancellationToken cancellationToken)
        {
            var hasRoleToUpdate = await hasRoleRepository.GetHasRoleByIdAsync(id);
            if (hasRoleToUpdate == null)
            {
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("matching hasRole id not found in database.")
                        .SetCode("HASROLE_NOT_FOUND")
                        .Build());
            }


            if (input.Rate != null)
            {
                hasRoleToUpdate.Rate = input.Rate;
            }

            await hasRoleRepository.UpdateHasRoleAsync(hasRoleToUpdate, cancellationToken).ConfigureAwait(false);

            await eventSender.SendAsync(hasRoleToUpdate, cancellationToken).ConfigureAwait(false);

            return new UpdateHasRolePayload(hasRoleToUpdate);

        }

        //delete

        public async Task<HasRole> DeleteHasRoleAsync(
           [Service] IHasRoleRepository hasRoleRepository,
           [Service] ITopicEventSender eventSender,
           DeleteHasRoleInput input, CancellationToken cancellationToken)
        {
            var hasRoleToDelete = await hasRoleRepository.GetHasRoleByIdAsync(input.Id);

            await hasRoleRepository.DeleteHasRoleAsync(hasRoleToDelete, cancellationToken).ConfigureAwait(false);

            await eventSender.SendAsync(hasRoleToDelete, cancellationToken).ConfigureAwait(false);

            return hasRoleToDelete;

        }

    }
}

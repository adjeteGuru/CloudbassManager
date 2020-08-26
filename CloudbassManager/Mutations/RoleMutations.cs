using Cloudbass.DataAccess.Repositories.Contracts;
using Cloudbass.Database.Models;
using Cloudbass.Types.Roles;
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
    //[ExtendObjectType(Name = "Mutation")]

    //CREATE
    //public class RoleMutations
    //{
    //    public async Task<CreateRolePayload> AddRoleAsync(
    //         [Service] IRoleRepository roleRepository,
    //        [Service] ITopicEventSender eventSender,
    //        CreateRoleInput input,
    //        CancellationToken cancellationToken)
    //    {
    //        var addedRole = new Role
    //        {
    //            Name = input.Name,

    //        };

    //        await roleRepository.CreateRoleAsync(addedRole, cancellationToken).ConfigureAwait(false);

    //        await eventSender.SendAsync(addedRole, cancellationToken).ConfigureAwait(false);

    //        return new CreateRolePayload(addedRole);

    //    }
    //}
}

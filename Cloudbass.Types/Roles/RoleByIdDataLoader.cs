using Cloudbass.DataAccess.Repositories.Contracts;
using Cloudbass.Database.Models;
using HotChocolate.DataLoader;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cloudbass.Types.Roles
{
    //public class RoleByIdDataLoader : BatchDataLoader<Guid, Role>
    //{
    //    private readonly IRoleRepository _roleRepository;
    //    public RoleByIdDataLoader(IRoleRepository roleRepository)
    //    {
    //        _roleRepository = roleRepository;
    //    }


    //    protected override async Task<IReadOnlyDictionary<Guid, Role>> LoadBatchAsync(
    //        IReadOnlyList<Guid> keys, CancellationToken cancellationToken)
    //    {
    //        //return await _roleRepository
    //        //    .GetRolesByIdAsync(keys, cancellationToken)
    //        //    .ConfigureAwait(false);
    //    }
    //}
}

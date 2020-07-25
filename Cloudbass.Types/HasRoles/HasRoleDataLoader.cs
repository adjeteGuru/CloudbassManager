using Cloudbass.DataAccess.Repositories.Contracts;
using Cloudbass.Database.Models;
using GreenDonut;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cloudbass.Types.HasRoles
{
    //public class HasRoleDataLoader : DataLoaderBase<int, HasRole>
    //{
    //private readonly IHasRoleRepository _hasRoleRepository;
    //public HasRoleDataLoader(IHasRoleRepository hasRoleRepository)
    //{
    //    _hasRoleRepository = hasRoleRepository;
    //}
    //protected override async Task<IReadOnlyList<Result<HasRole>>> FetchAsync(
    //    IReadOnlyList<int> keys, CancellationToken cancellationToken)
    //{
    //    return await _hasRoleRepository.GetHasRoleByIdAsync(keys)
    //        .ConfigureAwait(false);
    //}
    //}
}

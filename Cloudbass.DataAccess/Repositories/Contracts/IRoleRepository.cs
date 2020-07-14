using Cloudbass.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cloudbass.DataAccess.Repositories.Contracts
{
    public interface IRoleRepository
    {

        Task<IEnumerable<Role>> GetAllRolesAsync();

        Task<IReadOnlyDictionary<int, Role>> GetRolesByIdAsync(
            IReadOnlyList<int> ids, CancellationToken cancellation);
        Task<Role> CreateRoleAsync(Role role, CancellationToken cancellation);
    }
}

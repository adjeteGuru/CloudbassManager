using Cloudbass.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cloudbass.DataAccess.Repositories.Contracts
{
    public interface IHasRoleRepository
    {
        IQueryable<HasRole> GetHasRoles();

        Task<HasRole> GetHasRoleByIdAsync(int id);

        Task<IReadOnlyDictionary<int, HasRole>> GetHasRolesByIdAsync(
           IReadOnlyList<int> ids, CancellationToken cancellationToken);

        Task<HasRole> CreateHasRoleAsync(HasRole hasRole, CancellationToken cancellationToken);

        Task<HasRole> GetHasRoleByRoleOrEmployee(string employeeName, string roleName);
    }
}

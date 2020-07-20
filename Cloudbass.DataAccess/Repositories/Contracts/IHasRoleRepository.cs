using Cloudbass.Database.Models;
using GreenDonut;
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
        Task<IEnumerable<HasRole>> GetAllHasRolesAsync();
        Task<HasRole> GetHasRoleByIdAsync(int id);
        Task<IReadOnlyDictionary<int, HasRole>> GetHasRolesByIdAsync(
           IReadOnlyList<int> ids, CancellationToken cancellationToken);

      //  //on test
      //  Task<ILookup<int, HasRole>> GetEmployeesByJob(
      //IReadOnlyList<Guid> onjobs);
        Task<HasRole> CreateHasRoleAsync(HasRole hasRole, CancellationToken cancellationToken);
        Task<HasRole> GetHasRoleByRoleOrEmployee(string employeeName, string roleName);
        //Task<IReadOnlyList<Result<HasRole>>> GetHasRoleByIdAsync(IReadOnlyList<int> keys);
    }
}

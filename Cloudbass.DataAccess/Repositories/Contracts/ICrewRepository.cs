using Cloudbass.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cloudbass.DataAccess.Repositories.Contracts
{
    public interface ICrewRepository
    {
        Task<IEnumerable<Crew>> GetCrewAsync();

        Task<Crew> GetCrewMemberByIdAsync(Guid id);
        Task<IReadOnlyDictionary<Guid, Crew>> GetCrewMembersByIdAsync(
           IReadOnlyList<Guid> ids, CancellationToken cancellationToken);

        //  //on test
        //  Task<ILookup<int, Crew>> GetEmployeesByJob(
        //IReadOnlyList<Guid> onjobs);
        Task<Crew> CreateCrewAsync(Crew crew);
    }
}

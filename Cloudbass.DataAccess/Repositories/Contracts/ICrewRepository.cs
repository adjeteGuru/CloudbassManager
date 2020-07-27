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

        Task<Crew> CreateCrewAsync(Crew crew);

        //Task<Crew> GetCrewMemberAsync(Crew crew);

        //Task<IReadOnlyDictionary<Guid, Crew>> GetCrewMembersByIdAsync(
        //   IReadOnlyList<Guid> ids, CancellationToken cancellationToken);

        //  //on test
        //  Task<ILookup<Guid, Crew>> GetCrewMembersByJobIdAsync(
        //IReadOnlyList<Guid> jobIds, CancellationToken cancellationToken);

    }
}

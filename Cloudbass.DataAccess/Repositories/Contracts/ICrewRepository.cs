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

        Task<Crew> CreateCrewAsync(Crew crew, CancellationToken cancellationToken);

        Task<Crew> GetCrewMemberByIdAsync(Guid jobId, Guid hasRoleId);

        Task<IReadOnlyDictionary<Guid, Crew>> GetCrewMembersByIdAsync(
           IReadOnlyList<Guid> ids, CancellationToken cancellationToken);

        //Task<Crew> DeleteCrewAsync(Crew crew, CancellationToken cancellationToken);

    }
}

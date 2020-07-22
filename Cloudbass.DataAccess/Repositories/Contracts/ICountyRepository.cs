using Cloudbass.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cloudbass.DataAccess.Repositories.Contracts
{
    public interface ICountyRepository
    {

        Task<IEnumerable<County>> GetAllCountyAsync();

        Task<County> GetCountyByIdAsync(Guid countyId);
        Task<IReadOnlyDictionary<Guid, County>> GetCountiesByIdAsync(
            IReadOnlyList<Guid> ids, CancellationToken cancellationToken);
        Task<County> CreateCountyAsync(County county, CancellationToken cancellationToken);
    }
}

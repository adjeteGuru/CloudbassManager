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

        IQueryable<County> GetAllCountyAsync();

        Task<County> GetCountyByIdAsync(int countyId);
        Task<IReadOnlyDictionary<int, County>> GetCountiesByIdAsync(
            IReadOnlyList<int> ids, CancellationToken cancellationToken);
        Task<County> CreateCountyAsync(County county, CancellationToken cancellationToken);
    }
}

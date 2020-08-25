using Cloudbass.DataAccess.Repositories.Contracts;
using Cloudbass.Database;
using Cloudbass.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cloudbass.DataAccess.Repositories
{
    public class CountyRepository : ICountyRepository
    {
        private readonly CloudbassContext _db;
        public CountyRepository(CloudbassContext db)
        {
            _db = db;
        }

        public async Task<County> CreateCountyAsync(County county, CancellationToken cancellationToken)
        {
            var addedCounty = await _db.Counties.AddAsync(county)
                .ConfigureAwait(false);
            await _db.SaveChangesAsync();
            return addedCounty.Entity;
        }

        public async Task<IEnumerable<County>> GetAllCountyAsync()
        {

            return await _db.Counties.AsNoTracking().ToListAsync();
        }



        public async Task<IReadOnlyDictionary<Guid, County>> GetCountiesByIdAsync(
            IReadOnlyList<Guid> ids, CancellationToken cancellationToken)
        {
            var list = await _db.Counties.AsQueryable()
                .Where(x => ids.Contains(x.Id))
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);
            return list.ToDictionary(x => x.Id);
        }

        public async Task<County> GetCountyByIdAsync(Guid countyId)
        {
            return await _db.Counties.SingleOrDefaultAsync(x => x.Id == countyId);
        }

        public async Task<County> UpdateCountyAsync(
            County county, CancellationToken cancellationToken)
        {
            var countyToUpdate = await _db.Counties.FindAsync(county.Id);
            var updatedCounty = _db.Counties.Update(countyToUpdate);
            await _db.SaveChangesAsync();
            return updatedCounty.Entity;
        }


    }
}

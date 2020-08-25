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
    public class CrewRepository : ICrewRepository

    {
        private readonly CloudbassContext _db;
        public CrewRepository(CloudbassContext db)
        {
            _db = db;
        }

        public async Task<Crew> CreateCrewAsync(Crew crew, CancellationToken cancellationToken)
        {
            var addedCrew = await _db.Crews.AddAsync(crew);
            await _db.SaveChangesAsync();
            return addedCrew.Entity;
        }

        public async Task<IEnumerable<Crew>> GetCrewAsync()
        {
            return await _db.Crews
                 .AsNoTracking().ToListAsync();
        }

        public Task<Crew> GetCrewMemberByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyDictionary<Guid, Crew>> GetCrewMembersByIdAsync(
            IReadOnlyList<Guid> ids, CancellationToken cancellationToken)
        {
            var list = await _db.Crews.AsQueryable()
                .Where(x => ids.Contains(x.EmployeeId))
                .ToListAsync();
            return list.ToDictionary(x => x.EmployeeId);
        }
    }
}

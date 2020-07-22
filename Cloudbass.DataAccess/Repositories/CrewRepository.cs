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

        public Task<Crew> CreateCrewAsync(Crew crew)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyDictionary<Guid, Crew>> GetCrewMembersByIdAsync(
            IReadOnlyList<Guid> ids, CancellationToken cancellationToken)
        {
            var list = await _db.CrewMembers.AsQueryable()
                .Where(x => ids.Contains(x.Id))
                .ToListAsync(cancellationToken);
            return list.ToDictionary(x => x.Id);

        }

        public IQueryable<Crew> GetAllCrewAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Crew>> GetCrewAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Crew> GetCrewMemberByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        //search employees involved in a job
        public async Task<ILookup<Guid, Crew>> GetEmployeesByJobC(
            IReadOnlyList<Guid> onjobs)
        {
            var employees = await _db.CrewMembers
                .Where(x => onjobs.Contains(x.HasRole.EmployeeId))
                .ToListAsync();
            return employees.ToLookup(x => x.Id);
        }
    }
}

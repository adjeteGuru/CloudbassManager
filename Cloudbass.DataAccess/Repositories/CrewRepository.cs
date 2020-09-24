using Cloudbass.DataAccess.Repositories.Contracts;
using Cloudbass.Database;
using Cloudbass.Database.Models;
using HotChocolate;
using HotChocolate.Execution;
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


        public async Task<Crew> GetCrewMemberByIdAsync(Guid jobId, Guid hasRoleId)
        {
            // return 
            return await _db.Crews.FindAsync(jobId, hasRoleId);

        }

        public async Task<IReadOnlyDictionary<Guid, Crew>> GetCrewMembersByIdAsync(
            IReadOnlyList<Guid> ids, CancellationToken cancellationToken)
        {
            var list = await _db.Crews.AsQueryable()
                .Where(x => ids.Contains(x.HasRoleId))
                .ToListAsync();
            return list.ToDictionary(x => x.HasRoleId);
        }


        //delete method
        public async Task<Crew> UpdateCrewAsync(Crew crew, CancellationToken cancellationToken)
        {
            var crewToUpdate = await _db.Crews.FindAsync(crew.HasRoleId, crew.JobId);
            var updatedCrew = _db.Crews.Update(crewToUpdate);
            await _db.SaveChangesAsync();
            return updatedCrew.Entity;
        }


        ////delete function
        //public async Task<Crew> DeleteCrewAsync(Crew crew, CancellationToken cancellationToken)
        //{
        //    var crewToDelete = await _db.Crews.FindAsync(crew.JobId, crew.HasRoleId);

        //    if (crewToDelete.HasRoleId == null || crewToDelete.JobId == null)
        //    {
        //        throw new QueryException(
        //           ErrorBuilder.New()
        //               .SetMessage("Crew not found in database.")
        //               .SetCode("CREW_NOT_FOUND")
        //               .Build());
        //    }

        //    _db.Crews.Remove(crewToDelete);

        //    await _db.SaveChangesAsync();

        //    return crewToDelete;
        //}
    }
}

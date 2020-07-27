using Cloudbass.DataAccess.Repositories.Contracts;
using Cloudbass.Database;
using Cloudbass.Database.Models;
using GreenDonut;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cloudbass.DataAccess.Repositories
{
    public class HasRoleRepository : IHasRoleRepository
    {
        private readonly CloudbassContext _db;
        public HasRoleRepository(CloudbassContext db)
        {
            _db = db;
        }

        //public async Task<HasRole> CreateHasRoleAsync(HasRole hasRole, CancellationToken cancellationToken)
        //{
        //    var addedHasRole = await _db.HasRoles.AddAsync(hasRole);
        //    await _db.SaveChangesAsync()
        //        .ConfigureAwait(false);
        //    return addedHasRole.Entity;
        //}



        //public async Task<HasRole> GetHasRoleByIdAsync(Guid id)
        //{
        //    return await _db.HasRoles.FindAsync(id);
        //}

        //public Task<HasRole> GetHasRoleByRoleOrEmployee(string employeeName, string roleName)
        //{
        //    return _db.HasRoles.FirstAsync(x => x.Employee.FullName == employeeName || x.Role.Name == roleName);
        //}

        //public async Task<IEnumerable<HasRole>> GetAllHasRolesAsync()
        //{
        //    return await _db.HasRoles.AsNoTracking().ToListAsync();
        //}

        //// this GetHasRolesAsync method takes a list of hasRole ids and returns a dictionary of hasRoles
        ////with their ids as keys.
        //public async Task<IReadOnlyDictionary<Guid, HasRole>> GetHasRolesByIdAsync(
        //    IReadOnlyList<Guid> ids, CancellationToken cancellationToken)
        //{
        //    var list = await _db.HasRoles.AsQueryable()
        //        .Where(x => ids.Contains(x.Id))
        //        .ToListAsync(cancellationToken)
        //        .ConfigureAwait(false);
        //    return list.ToDictionary(x => x.Id);
        //}



        //search employees involved in a job
        //public async Task<ILookup<Guid, HasRole>> GetEmployeesByJobIdAsync(
        //    IReadOnlyList<Guid> jobIds, CancellationToken cancellationToken)
        //{
        //    var employees = await _db.HasRoles
        //         .Where(x => jobIds.Contains(x.EmployeeId))
        //         .ToListAsync(cancellationToken);
        //    return employees.ToLookup(x => x.EmployeeId);
        //}
    }
}

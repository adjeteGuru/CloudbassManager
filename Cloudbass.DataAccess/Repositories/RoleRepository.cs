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
    public class RoleRepository : IRoleRepository
    {
        private readonly CloudbassContext _db;
        public RoleRepository(CloudbassContext db)
        {
            _db = db;
        }

        //public async Task<Role> CreateRoleAsync(Role role, CancellationToken cancellation)
        //{
        //    var addedRole = await _db.Roles.AddAsync(role);
        //    await _db.SaveChangesAsync();
        //    return addedRole.Entity;
        //}

        //public async Task<IEnumerable<Role>> GetAllRolesAsync()
        //{
        //    return await _db.Roles.AsNoTracking().ToListAsync();
        //}

        //public async Task<Role> GetRoleByIdAsync(Guid roleId)
        //{
        //    return await _db.Roles.FindAsync(roleId);
        //}

        //public async Task<IReadOnlyDictionary<Guid, Role>> GetRolesByIdAsync(
        //    IReadOnlyList<Guid> ids, CancellationToken cancellation)
        //{
        //    var list = await _db.Roles.AsQueryable()
        //        .Where(x => ids.Contains(x.Id))
        //        .ToListAsync(cancellation);
        //    return list.ToDictionary(x => x.Id);
        //}


        //public IQueryable<Role> GetAllRole()
        //{
        //    return _db.Roles.AsQueryable();
        //}

        //public Role GetRole(int id)
        //{
        //    return _db.Roles.SingleOrDefault(x => x.Id == id);
        //}
    }
}

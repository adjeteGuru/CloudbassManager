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
    public class RoleRepository : IRoleRepository
    {
        private readonly CloudbassContext _db;
        public RoleRepository(CloudbassContext db)
        {
            _db = db;
        }

        public async Task<Role> CreateRoleAsync(Role role, CancellationToken cancellation)
        {
            var addedRole = await _db.Roles.AddAsync(role)
                .ConfigureAwait(false);
            await _db.SaveChangesAsync();
            return addedRole.Entity;
        }


        public async Task<IEnumerable<Role>> GetAllRolesAsync()
        {
            return await _db.Roles.AsNoTracking().ToListAsync();
        }

        public async Task<Role> GetRoleByIdAsync(Guid roleId)
        {
            return await _db.Roles.FindAsync(roleId);
        }

        public async Task<IReadOnlyDictionary<Guid, Role>> GetRolesByIdAsync(
            IReadOnlyList<Guid> ids, CancellationToken cancellation)
        {
            var list = await _db.Roles.AsQueryable()
                .Where(x => ids.Contains(x.Id))
                .ToListAsync(cancellation);
            return list.ToDictionary(x => x.Id);
        }


        public async Task<Role> DeleteRoleAsync(Role role, CancellationToken cancellationToken)
        {
            //create a variable and check if job id match id field from the db
            var roleToDelete = await _db.Roles.FirstOrDefaultAsync(x => x.Id == role.Id);

            if (roleToDelete == null)
            {
                throw new QueryException(
                   ErrorBuilder.New()
                       .SetMessage("Role not found in database.")
                       .SetCode("ROLE_NOT_FOUND")
                       .Build());
            }

            _db.Roles.Remove(roleToDelete);

            await _db.SaveChangesAsync();

            return roleToDelete;
        }

        public async Task<Role> UpdateRoleAsync(
           Role role, CancellationToken cancellationToken)
        {
            var roleToUpdate = await _db.Roles.FindAsync(role.Id);
            var updatedRole = _db.Roles.Update(roleToUpdate);
            await _db.SaveChangesAsync();
            return updatedRole.Entity;
        }


    }
}

using Cloudbass.DataAccess.Repositories.Contracts;
using Cloudbass.Database;
using Cloudbass.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cloudbass.DataAccess.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly CloudbassContext _db;
        public RoleRepository(CloudbassContext db)
        {
            _db = db;
        }
        public IQueryable<Role> GetAll()
        {
            return _db.Roles.AsQueryable();
        }

        public Role GetRole(int id)
        {
            return _db.Roles.SingleOrDefault(x => x.Id == id);
        }
    }
}

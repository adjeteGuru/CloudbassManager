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
            throw new NotImplementedException();
        }

        public Role GetRole(int id)
        {
            throw new NotImplementedException();
        }
    }
}

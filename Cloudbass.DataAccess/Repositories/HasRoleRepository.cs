using Cloudbass.DataAccess.Repositories.Contracts;
using Cloudbass.Database;
using Cloudbass.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cloudbass.DataAccess.Repositories
{
    public class HasRoleRepository : IHasRoleRepository
    {
        private readonly CloudbassContext _db;
        public HasRoleRepository(CloudbassContext db)
        {
            _db = db;
        }
        public IQueryable<HasRole> GetAll()
        {
            throw new NotImplementedException();
        }

        public HasRole GetHasRoleById(int id)
        {
            return _db.HasRoles.SingleOrDefault(x => x.Id == id);
        }
    }
}

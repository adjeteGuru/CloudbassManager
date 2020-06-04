using Cloudbass.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cloudbass.DataAccess.Repositories.Contracts
{
    public interface IRoleRepository
    {
        public Role GetRole(int id);
        IQueryable<Role> GetAll();
    }
}

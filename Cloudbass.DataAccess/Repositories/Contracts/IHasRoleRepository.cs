using Cloudbass.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cloudbass.DataAccess.Repositories.Contracts
{
    public interface IHasRoleRepository
    {
        IQueryable<HasRole> GetAll();

    }
}

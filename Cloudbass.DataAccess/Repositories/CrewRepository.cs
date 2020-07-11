using Cloudbass.DataAccess.Repositories.Contracts;
using Cloudbass.Database;
using Cloudbass.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public IQueryable<Crew> GetAllCrewAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Crew>> GetCrewAsync()
        {
            throw new NotImplementedException();
        }
    }
}

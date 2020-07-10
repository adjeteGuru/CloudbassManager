using Cloudbass.DataAccess.Repositories.Contracts;
using Cloudbass.Database;
using Cloudbass.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cloudbass.DataAccess.Repositories
{
    public class CountyRepository : ICountyRepository
    {
        private readonly CloudbassContext _db;
        public CountyRepository(CloudbassContext db)
        {
            _db = db;
        }

        public IQueryable<County> GetAllCounty()
        {
            return _db.Counties.AsQueryable();
        }

        public County GetById(int id)
        {
            //throw new NotImplementedException();
            return _db.Counties.SingleOrDefault(x => x.Id == id);
        }
    }
}

using Cloudbass.DataAccess.Repositories.Contracts;
using Cloudbass.Database.Models;
using HotChocolate;
using HotChocolate.Resolvers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cloudbass.DataAccess.Resolvers
{
    public class CountyResolver
    {
        private readonly ICountyRepository _countyRepository;
        public CountyResolver([Service] ICountyRepository countyRepository)
        {
            _countyRepository = countyRepository;
        }

        public County GetCounty(Employee employee, IResolverContext ctx)
        {
            return _countyRepository.GetAllCounty().Where(x => x.Id == employee.CountyId).FirstOrDefault();
        }
    }
}

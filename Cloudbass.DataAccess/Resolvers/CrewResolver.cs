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
    public class CrewResolver
    {
        private readonly ICrewRepository _crewRepository;
        public CrewResolver([Service] ICrewRepository crewRepository)
        {
            _crewRepository = crewRepository;
        }

        //public IEnumerable<Crew> GetCrews(HasRole hasRole, IResolverContext ctx)
        //{
        //    return _crewRepository.GetAll().Where(x => x.HasRoleId == hasRole.Id);
        //}

        //public IEnumerable<Crew> GetCrews(Job job, IResolverContext ctx)
        //{
        //    return _crewRepository.GetAll().Where(x => x.JobId == job.Id);
        //}

        public IEnumerable<Crew> GetCrews(HasRole hasRole, Job job, IResolverContext ctx)
        {
            return _crewRepository.GetAll().Where(x => x.HasRoleId == hasRole.Id && x.JobId == job.Id);
        }
    }
}

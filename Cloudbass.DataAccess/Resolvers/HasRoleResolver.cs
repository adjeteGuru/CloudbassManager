using Cloudbass.DataAccess.Repositories.Contracts;
using Cloudbass.Database.Models;
using HotChocolate.Resolvers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cloudbass.DataAccess.Resolvers
{
    public class HasRoleResolver
    {
        private readonly IHasRoleRepository _hasRoleRepository;
        public HasRoleResolver(IHasRoleRepository hasRoleRepository)
        {
            _hasRoleRepository = hasRoleRepository;
        }

        public IEnumerable<HasRole> GetHasRoles(Crew crew, IResolverContext ctx)
        {
            return _hasRoleRepository.GetAll().Where(x => x.Id == crew.HasRoleId);
        }
    }
}

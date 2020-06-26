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
    public class RoleResolver
    {
        private readonly IRoleRepository _roleRepository;
        public RoleResolver([Service] IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public Role GetRole(HasRole hasRole, IResolverContext ctx)
        {
            return _roleRepository.GetAll().Where(x => x.Id == hasRole.RoleId).FirstOrDefault();
        }
    }
}

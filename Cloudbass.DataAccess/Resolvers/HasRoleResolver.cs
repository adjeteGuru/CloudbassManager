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
    public class HasRoleResolver
    {
        private readonly IHasRoleRepository _hasRoleRepository;
        public HasRoleResolver([Service] IHasRoleRepository hasRoleRepository)
        {
            _hasRoleRepository = hasRoleRepository;
        }

        //public HasRole GetHasRoleOnCrew(Crew crew, IResolverContext ctx)
        //{
        //    return _hasRoleRepository.GetAll().Where(x => x.Id == crew.HasRoleId).FirstOrDefault();
        //}

        //public IEnumerable<HasRole> GetHasRoles(Role role, IResolverContext ctx)
        //{
        //    return _hasRoleRepository.GetAll().Where(x => x.RoleId == role.Id);
        //}
        //public IEnumerable<HasRole> GetHasRoles(Employee employee, IResolverContext ctx)
        //{
        //    return _hasRoleRepository.GetAll().Where(x => x.EmployeeId == employee.Id);
        //}

        //public IEnumerable<HasRole> GetHasRoles(Role role, Employee employee, IResolverContext ctx)
        //{
        //    return _hasRoleRepository.GetAll().Where(x => x.RoleId == role.Id && x.EmployeeId == employee.Id);
        //}
    }
}

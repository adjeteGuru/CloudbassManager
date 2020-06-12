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
    public class UserResolver
    {
        private readonly IUserRepository _userRepository;
        public UserResolver([Service] IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IEnumerable<User> GetUsers(Employee employee, IResolverContext ctx)
        {
            return _userRepository.GetAll().Where(x => x.EmployeeId == employee.Id);
        }


        //public IEnumerable<User> GetUsers(Job job, IResolverContext ctx)
        //{
        //    return _userRepository.GetAll()
        //        .Where(x => x.HasRoles.Contains(Role.Id && x.HasRoles.Contains(User.Equals())) == job.Id);
        //}
    }
}

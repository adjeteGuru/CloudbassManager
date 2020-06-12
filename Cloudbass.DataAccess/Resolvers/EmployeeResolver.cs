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
    public class EmployeeResolver
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeResolver([Service] IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public IEnumerable<Employee> GetEmplyees(Client client, IResolverContext ctx)
        {
            return _employeeRepository.GetAll().Where(x => x.CountyId == client.Id);
        }

        public Employee GetEmployee(HasRole hasRole, IResolverContext ctx)
        {
            return _employeeRepository.GetAll().Where(x => x.Id == hasRole.EmployeeId).FirstOrDefault();
        }
    }
}

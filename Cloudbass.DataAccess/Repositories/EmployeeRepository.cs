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
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly CloudbassContext _db;
        public EmployeeRepository(CloudbassContext db)
        {
            _db = db;
        }
        public Task AddEmployeeAsync(Employee employee)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Employee> GetAll()
        {
            throw new NotImplementedException();
        }

        public Employee GetEmployee(int id)
        {
            throw new NotImplementedException();
        }
    }
}

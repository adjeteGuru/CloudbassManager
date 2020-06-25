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
            return _db.Employees.AsQueryable();
        }

        public Employee GetEmployee(Guid id)
        {
            return _db.Employees.SingleOrDefault(x => x.Id == id);
        }
    }
}

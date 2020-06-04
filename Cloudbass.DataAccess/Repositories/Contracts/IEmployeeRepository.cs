using Cloudbass.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudbass.DataAccess.Repositories.Contracts
{
    public interface IEmployeeRepository
    {
        public Employee GetEmployee(int id);
        IQueryable<Employee> GetAll();

        Task AddEmployeeAsync(Employee employee);

    }
}

using Cloudbass.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cloudbass.DataAccess.Repositories.Contracts
{
    public interface IEmployeeRepository
    {

        Task<IEnumerable<Employee>> GetAllEmployeesAsync();
        Task<Employee> GetEmployeeByCountyAsync(string countyName);

        Task<Employee> GetEmployeeByIdAsync(Guid id);
        Task<IReadOnlyDictionary<Guid, Employee>> GetEmployeesByIdAsync(
           IReadOnlyList<Guid> ids,
           CancellationToken cancellationToken);

        Task<IReadOnlyDictionary<string, Employee>> GetEmployeesByEmailAsync(
          IReadOnlyList<string> emails,
          CancellationToken cancellationToken);

        //on test
        Task<ILookup<Guid, Employee>> GetEmployeesByJob(
       IReadOnlyList<Guid> onjobs);

        Task<Employee> CreateEmployeeAsync(Employee employee, CancellationToken cancellationToken);

        Task<Employee> UpdateEmployeeAsync(Employee employee, CancellationToken cancellationToken);

    }
}

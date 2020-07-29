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

        Task<Employee> GetEmployeeByIdAsync(Guid id);
        Task<IReadOnlyDictionary<Guid, Employee>> GetEmployeesByIdAsync(
           IReadOnlyList<Guid> ids,
           CancellationToken cancellationToken);

        Task<IReadOnlyDictionary<string, Employee>> GetEmployeesByNameAsync(
           IReadOnlyList<string> nameList,
           CancellationToken cancellationToken);

        Task<IReadOnlyDictionary<string, Employee>> GetEmployeesByEmailAsync(
           IReadOnlyList<string> emails,
          CancellationToken cancellationToken);

        //on test
        Task<ILookup<Guid, Employee>> GetEmployeesByCountyIdAsync(
       IReadOnlyList<Guid> countyIds, CancellationToken cancellationToken);

        Task<ILookup<Guid, Crew>> GetEmployeesByJobIdAsync(
         IReadOnlyList<Guid> jobIds, CancellationToken cancellationToken);




        Task<Employee> CreateEmployeeAsync(Employee employee, CancellationToken cancellationToken);

        Task<Employee> UpdateEmployeeAsync(Employee employee, CancellationToken cancellationToken);

    }
}

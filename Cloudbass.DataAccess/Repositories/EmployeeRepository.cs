using Cloudbass.DataAccess.Repositories.Contracts;
using Cloudbass.Database;
using Cloudbass.Database.Models;
using HotChocolate;
using HotChocolate.Execution;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

        public async Task<Employee> CreateEmployeeAsync(
           Employee employee, CancellationToken cancellationToken)
        {
            //check dupication of the new entry
            var checkEmployee = await _db.Employees
                .FirstOrDefaultAsync(x => x.FullName == employee.FullName);

            if (checkEmployee != null)
            {
                // throw error if the new name is already taken
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("Name \"" + employee.FullName + "\" is already taken")
                        .SetCode("NAME_EXIST")
                        .Build());
            }


            var addedEmployee = await _db.Employees.AddAsync(employee)
                .ConfigureAwait(false);
            await _db.SaveChangesAsync();
            return addedEmployee.Entity;

        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            return await _db.Employees.AsNoTracking().ToListAsync();
        }

        //this
        public async Task<IReadOnlyDictionary<Guid, Employee>> GetEmployeesByIdAsync(
            IReadOnlyList<Guid> ids, CancellationToken cancellationToken)
        {
            var list = await _db.Employees.AsQueryable()
                 .Where(x => ids.Contains(x.Id))
                 .ToListAsync(cancellationToken)
                 .ConfigureAwait(false);
            return list.ToDictionary(x => x.Id);
        }

        public async Task<IReadOnlyDictionary<string, Employee>> GetEmployeesByEmailAsync(
            IReadOnlyList<string> emails, CancellationToken cancellationToken)
        {
            var list = await _db.Employees.AsQueryable()
                .Where(x => emails.Contains(x.Email))
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);

            return list.ToDictionary(x => x.Email);
        }

        public async Task<Employee> UpdateEmployeeAsync(
            Employee employee, CancellationToken cancellationToken)
        {
            var employeeToUpdate = await _db.Employees.FindAsync(employee.Id);
            var updatedEmployee = _db.Employees.Update(employeeToUpdate);
            await _db.SaveChangesAsync()
               .ConfigureAwait(false);
            return updatedEmployee.Entity;

        }


        public async Task<Employee> GetEmployeeByIdAsync(Guid id)
        {
            return await _db.Employees.FindAsync(id);
        }


        //this is to track and fetch every employee involved in selected jobId
        public async Task<ILookup<Guid, Crew>> GetEmployeesByJobIdAsync(
            IReadOnlyList<Guid> jobIds, CancellationToken cancellationToken)
        {
            var filterEmployee = await _db.Crews
                .Where(x => jobIds.Contains(x.JobId))
                .ToListAsync();

            return filterEmployee.ToLookup(x => x.JobId);
        }

        public async Task<IReadOnlyDictionary<string, Employee>> GetEmployeesByNameAsync(
        IReadOnlyList<string> nameList, CancellationToken cancellationToken)
        {
            var list = await _db.Employees.AsQueryable()
                .Where(x => nameList.Contains(x.FullName))
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);

            return list.ToDictionary(x => x.FullName);
        }

        public async Task<ILookup<Guid, Employee>> GetEmployeesByCountyIdAsync(
            IReadOnlyList<Guid> countyIds, CancellationToken cancellationToken)
        {
            var list = await _db.Employees
                .Where(x => countyIds.Contains(x.CountyId))
                .ToListAsync(cancellationToken);
            return list.ToLookup(x => x.CountyId);
        }

        public async Task<Employee> DeleteEmployeeAsync(Employee employee, CancellationToken cancellationToken)
        {
            var employeeToDelete = await _db.Employees.FindAsync(employee.Id);

            if (employeeToDelete == null)
            {
                throw new QueryException(
                   ErrorBuilder.New()
                       .SetMessage("Employee not found in database.")
                       .SetCode("EMPLOYEE_NOT_FOUND")
                       .Build());
            }

            _db.Employees.Remove(employeeToDelete);

            await _db.SaveChangesAsync();
            return employeeToDelete;
        }
    }
}

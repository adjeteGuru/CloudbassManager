using Cloudbass.DataAccess.Repositories.Contracts;
using Cloudbass.Database;
using Cloudbass.Database.Models;
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
            var addedEmployee = await _db.Employees.AddAsync(employee);
            await _db.SaveChangesAsync()
                .ConfigureAwait(false);
            return addedEmployee.Entity;

        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            return await _db.Employees.AsNoTracking().ToListAsync();
        }

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

        //search employees involved in a job
        //public async Task<ILookup<string, Employee>> GetEmployeesByJob(
        //    IReadOnlyList<string> onjobs)
        //{
        //    var employees = await _db.Employees
        //        .Where(x => onjobs.Contains(x.Id))
        //        .ToListAsync();
        //    return employees.ToLookup(x => x.Id);


        //}

        public async Task<ILookup<string, Employee>> GetEmployeesByCounty(
            IReadOnlyList<string> onjobs)
        {
            var filterEmployee = await _db.Employees
                .Where(x => onjobs.Contains(x.Countys))
                .ToListAsync();

            return filterEmployee.ToLookup(x => x.Countys);
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


        //public Task AddEmployeeAsync(Employee employee)
        //{
        //    throw new NotImplementedException();
        //}

        //public IQueryable<Employee> GetAll()
        //{
        //    return _db.Employees.AsQueryable();
        //}

        //public Employee GetEmployee(Guid id)
        //{
        //    return _db.Employees.SingleOrDefault(x => x.Id == id);
        //}
    }
}

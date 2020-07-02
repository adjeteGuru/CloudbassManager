using Cloudbass.DataAccess.Repositories.Contracts;
using Cloudbass.Database;
using Cloudbass.Database.Models;
using HotChocolate.DataLoader;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cloudbass.Types.Employees
{
    public class EmployeeByIdDataLoader : BatchDataLoader<Guid, Employee>
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeByIdDataLoader(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        protected override async Task<IReadOnlyDictionary<Guid, Employee>> LoadBatchAsync(
            IReadOnlyList<Guid> keys, CancellationToken cancellationToken)
        {
            return await _employeeRepository
                .GetEmployeesAsync(keys, cancellationToken)
                .ConfigureAwait(false);
        }
    }
}

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
    public class EmployeeByEmailDataLoader : BatchDataLoader<string, Employee>
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeByEmailDataLoader(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        protected override async Task<IReadOnlyDictionary<string, Employee>> LoadBatchAsync(
            IReadOnlyList<string> keys, CancellationToken cancellationToken)
        {
            return await _employeeRepository
                .GetEmployeesByEmailAsync(keys, cancellationToken)
                .ConfigureAwait(false);

        }
    }
}

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
    public class EmployeeByEmailLoader : BatchDataLoader<string, Employee>
    {
        private readonly CloudbassContext _db;
        public EmployeeByEmailLoader(CloudbassContext db)
        {
            _db = db;
        }
        protected override async Task<IReadOnlyDictionary<string, Employee>> LoadBatchAsync(IReadOnlyList<string> keys, CancellationToken cancellationToken)
        {
            return await _db.Employees
                .Where(x => keys.Contains(x.Email))
                .ToDictionaryAsync(x => x.Email);
        }
    }
}

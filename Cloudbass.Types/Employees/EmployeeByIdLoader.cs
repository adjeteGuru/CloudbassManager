﻿using Cloudbass.Database;
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
    public class EmployeeByIdLoader : BatchDataLoader<Guid, Employee>
    {
        private readonly CloudbassContext _db;
        public EmployeeByIdLoader(CloudbassContext db)
        {
            _db = db;
        }
        protected override async Task<IReadOnlyDictionary<Guid, Employee>> LoadBatchAsync(IReadOnlyList<Guid> keys, CancellationToken cancellationToken)
        {
            return await _db.Employees
                .Where(x => keys.Contains(x.Id))
                .ToDictionaryAsync(x => x.Id);
        }
    }
}

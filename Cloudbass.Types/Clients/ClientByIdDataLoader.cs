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

namespace Cloudbass.Types.Clients
{
    public class ClientByIdDataLoader : BatchDataLoader<Guid, Client>
    {
        private readonly CloudbassContext _db;
        public ClientByIdDataLoader(CloudbassContext db)
        {
            _db = db;
        }
        protected override async Task<IReadOnlyDictionary<Guid, Client>> LoadBatchAsync(
            IReadOnlyList<Guid> keys, CancellationToken cancellationToken)
        {
            return await _db.Clients
                .Where(x => keys.Contains(x.Id))
                .ToDictionaryAsync(x => x.Id);
        }
    }
}

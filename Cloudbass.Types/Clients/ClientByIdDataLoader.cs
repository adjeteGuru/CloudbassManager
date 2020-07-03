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

namespace Cloudbass.Types.Clients
{
    public class ClientByIdDataLoader : BatchDataLoader<Guid, Client>
    {
        private readonly IClientRepository _clientRepository;
        public ClientByIdDataLoader(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }
        protected override async Task<IReadOnlyDictionary<Guid, Client>> LoadBatchAsync(
            IReadOnlyList<Guid> keys, CancellationToken cancellationToken)
        {
            return await _clientRepository
                .GetClientsAsync(keys, cancellationToken)
                .ConfigureAwait(false);
        }
    }
}

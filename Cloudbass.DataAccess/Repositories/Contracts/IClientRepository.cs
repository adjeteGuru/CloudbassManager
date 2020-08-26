
using Cloudbass.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cloudbass.DataAccess.Repositories.Contracts
{
    public interface IClientRepository
    {
        Task<IEnumerable<Client>> GetAllClientsAsync();
        Task<Client> GetClientByIdAsync(Guid clientId);
        Task<IReadOnlyDictionary<Guid, Client>> GetClientsByIdAsync(
            IReadOnlyList<Guid> ids, CancellationToken cancellationToken);
        Task<Client> CreateClientAsync(Client client, CancellationToken cancellationToken);
        Task<Client> UpdateClientAsync(Client client, CancellationToken cancellationToken);

        Task<Client> DeleteClientAsync(Client client, CancellationToken cancellationToken);

    }
}

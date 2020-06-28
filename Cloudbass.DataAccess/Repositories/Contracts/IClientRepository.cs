using Cloudbass.DataAccess.Repositories.Contracts.Inputs;
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
        IQueryable<Client> GetClients();
        Task<Client> GetClientByIdAsync(int clientId);
        Task<IReadOnlyDictionary<int, Client>> GetClientsAsync(
            IReadOnlyList<int> ids, CancellationToken cancellationToken);
        Task<Client> CreateClientAsync(Client client);

        // Task UpdateClientAsync(Client client);

        //public Client GetClient(int id);
        //Client Create(CreateClientInput input);
        //Client Delete(DeleteClientInput input);
        //Client Update(UpdateClientInput input, int id);


    }
}

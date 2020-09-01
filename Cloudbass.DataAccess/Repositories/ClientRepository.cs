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
    public class ClientRepository : IClientRepository
    {
        private readonly CloudbassContext _db;
        public ClientRepository(CloudbassContext db)
        {
            _db = db;
        }
        //public Client GetClient(int id)
        //{
        //    return _db.Clients.SingleOrDefault(x => x.Id == id);
        //}

        //public IQueryable<Client> GetAll()
        //{
        //    return _db.Clients;
        //}

        //public Client Create(CreateClientInput input)
        //{
        //    //duplication check
        //    var clientCheck = _db.Clients.FirstOrDefault(x => x.Name == input.Name);

        //    if (clientCheck != null)
        //    {
        //        throw new QueryException(
        //           ErrorBuilder.New()
        //               .SetMessage("Name " + input.Name + " is already taken")
        //               .SetCode("NAME_EXIST")
        //               .Build());
        //    }

        //    Client client = new Client()
        //    {
        //        Name = input.Name,
        //        Email = input.Email,
        //        Tel = input.Tel,
        //        ToContact = input.ToContact,
        //        Address = input.Address
        //    };

        //    _db.Clients.Add(client);
        //    _db.SaveChanges();

        //    return client;
        //}

        //public Client Delete(DeleteClientInput input)
        //{
        //    var clientToDelete = _db.Clients.FirstOrDefault(x => x.Id == input.Id);

        //    if (clientToDelete == null)
        //    {
        //        throw new QueryException(
        //           ErrorBuilder.New()
        //               .SetMessage("Client name not found")
        //               .SetCode("NAME_EXIST")
        //               .Build());
        //    }

        //    _db.Clients.Remove(clientToDelete);

        //    _db.SaveChanges();

        //    return clientToDelete;
        //}

        //public Client Update(UpdateClientInput input, int id)
        //{
        //    var clientToUpdate = _db.Clients.Find(id);

        //    if (clientToUpdate == null)
        //    {
        //        throw new QueryException(
        //           ErrorBuilder.New()
        //               .SetMessage("Client name not found")
        //               .SetCode("NAME_EXIST")
        //               .Build());
        //    }

        //    if (!string.IsNullOrEmpty(input.Name))
        //    {
        //        clientToUpdate.Name = input.Name;
        //    }

        //    if (!string.IsNullOrEmpty(input.Address))
        //    {
        //        clientToUpdate.Address = input.Address;
        //    }

        //    if (!string.IsNullOrEmpty(input.Email))
        //    {
        //        clientToUpdate.Email = input.Email;
        //    }

        //    if (!string.IsNullOrEmpty(input.Tel))
        //    {
        //        clientToUpdate.Tel = input.Tel;
        //    }

        //    if (!string.IsNullOrEmpty(input.ToContact))
        //    {
        //        clientToUpdate.ToContact = input.ToContact;
        //    }

        //    _db.Clients.Update(clientToUpdate);

        //    _db.SaveChanges();

        //    return clientToUpdate;
        //}

        public async Task<IEnumerable<Client>> GetAllClientsAsync()
        {
            return await _db.Clients.AsNoTracking().ToListAsync();
        }

        public async Task<Client> GetClientByIdAsync(Guid clientId)
        {
            return await _db.Clients.FindAsync(clientId);
        }

        // this GetClientsAsync method takes a list of client ids and returns a dictionary of clients
        //with their ids as keys.
        public async Task<IReadOnlyDictionary<Guid, Client>> GetClientsByIdAsync(
            IReadOnlyList<Guid> ids, CancellationToken cancellationToken)
        {
            var list = await _db.Clients.AsQueryable()
                .Where(x => ids.Contains(x.Id))
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);
            return list.ToDictionary(x => x.Id);

        }

        public async Task<Client> CreateClientAsync(Client client, CancellationToken cancellationToken)
        {
            //check dupication of the new entry
            var checkClient = await _db.Clients
                .FirstOrDefaultAsync(x => x.Name == client.Name);

            if (checkClient != null)
            {
                // throw error if the new name is already taken
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("Name \"" + client.Name + "\" is already taken")
                        .SetCode("NAME_EXIST")
                        .Build());
            }

            var addedClient = await _db.Clients.AddAsync(client).ConfigureAwait(false);
            await _db.SaveChangesAsync();

            return addedClient.Entity;
        }

        public async Task<Client> UpdateClientAsync(
            Client client, CancellationToken cancellationToken)
        {
            var clientToUpdate = await _db.Clients.FindAsync(client.Id);
            var updatedClient = _db.Clients.Update(clientToUpdate);
            await _db.SaveChangesAsync();

            return updatedClient.Entity;
        }

        public async Task<Client> DeleteClientAsync(Client client, CancellationToken cancellationToken)
        {
            var clientToDelete = await _db.Clients.FindAsync(client.Id);

            if (clientToDelete == null)
            {
                throw new QueryException(
                   ErrorBuilder.New()
                       .SetMessage("Client not found in database.")
                       .SetCode("CLIENT_NOT_FOUND")
                       .Build());
            }

            _db.Clients.Remove(clientToDelete);

            await _db.SaveChangesAsync();

            return clientToDelete;
        }
    }
}

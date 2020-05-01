using Cloudbass.DataAccess.Repositories.Contracts;
using Cloudbass.DataAccess.Repositories.Contracts.Inputs;
using Cloudbass.Database;
using Cloudbass.Database.Models;
using HotChocolate;
using HotChocolate.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cloudbass.DataAccess.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly CloudbassContext _db;
        public ClientRepository(CloudbassContext db)
        {
            _db = db;
        }
        public Client GetClient(int id)
        {
            return _db.Clients.SingleOrDefault(x => x.Id == id);
        }

        public IQueryable<Client> GetAll()
        {
            return _db.Clients;
        }

        public Client Create(CreateClientInput input)
        {
            //duplication check
            var clientCheck = _db.Clients.FirstOrDefault(x => x.Name == input.Name);

            if (clientCheck != null)
            {
                throw new QueryException(
                   ErrorBuilder.New()
                       .SetMessage("There is existing client Name found in the database..Please chose another name ")
                       .SetCode("NAME_EXIST")
                       .Build());
            }

            Client client = new Client()
            {
                Name = input.Name,
                Email = input.Email,
                Tel = input.Tel,
                ToContact = input.ToContact,
                Address = input.Address
            };

            _db.Clients.Add(client);
            _db.SaveChanges();

            return client;
        }

        public Client Delete(DeleteClientInput input)
        {
            throw new NotImplementedException();
        }

        public Client Update(UpdateClientInput input)
        {
            throw new NotImplementedException();
        }
    }
}

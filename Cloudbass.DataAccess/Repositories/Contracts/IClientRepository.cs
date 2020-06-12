using Cloudbass.DataAccess.Repositories.Contracts.Inputs;
using Cloudbass.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cloudbass.DataAccess.Repositories.Contracts
{
    public interface IClientRepository
    {
        IQueryable<Client> GetAll();
        public Client GetClient(int id);
        Client Create(CreateClientInput input);
        Client Delete(DeleteClientInput input);
        Client Update(UpdateClientInput input, int id);


    }
}

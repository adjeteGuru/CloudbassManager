using Cloudbass.Database.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cloudbass.DataAccess.Repositories.Contracts
{
    public interface IClientRepository
    {
        IEnumerable<Client> GetClients();
        Client GetClient(int id);

    }
}

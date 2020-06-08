using Cloudbass.DataAccess.Repositories.Contracts;
using Cloudbass.Database.Models;
using HotChocolate;
using HotChocolate.Resolvers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cloudbass.DataAccess.Resolvers
{
    public class ClientResolver
    {
        private readonly IClientRepository _clientRepository;
        public ClientResolver([Service] IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public Client GetClient(Job job, IResolverContext ctx)
        {
            return _clientRepository.GetAll().Where(a => a.Id == job.ClientId).FirstOrDefault();
        }

    }
}

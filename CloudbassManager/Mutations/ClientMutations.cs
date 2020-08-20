using Cloudbass.DataAccess.Repositories;
using Cloudbass.DataAccess.Repositories.Contracts;
using Cloudbass.Database.Models;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudbassManager.Mutations
{
    [ExtendObjectType(Name = "Mutation")]
    public class ClientMutations
    {
        private readonly IClientRepository _clientRepository;
        public ClientMutations(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }
        //public Client CreateClient(CreateClientInput input)
        //{
        //    return _clientRepository.Create(input);
        //}

        //public Client DeleteClient(DeleteClientInput input)
        //{
        //    return _clientRepository.Delete(input);
        //}

        //public Client UpdateClient(UpdateClientInput input, int id)
        //{
        //    return _clientRepository.Update(input, id);
        //}
    }
}

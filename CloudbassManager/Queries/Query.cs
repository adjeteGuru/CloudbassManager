using Cloudbass.DataAccess.Repositories.Contracts;
using Cloudbass.Database.Models;
using Cloudbass.Types;
using Cloudbass.Types.Jobs;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CloudbassManager.Queries
{
    public class Query
    {
        private readonly IClientRepository _clientRepository;
        private readonly IJobRepository _jobRepository;
        public Query(IClientRepository clientRepository, IJobRepository jobRepository)
        {
            _clientRepository = clientRepository;
            _jobRepository = jobRepository;
        }


        [UsePaging(SchemaType = typeof(ClientType))]
        [UseFiltering]
        public IQueryable<Client> Clients => _clientRepository.GetAll();



        [UsePaging(SchemaType = typeof(JobType))]
        [UseFiltering]
        public IQueryable<Job> Jobs => _jobRepository.GetAll();
    }
}

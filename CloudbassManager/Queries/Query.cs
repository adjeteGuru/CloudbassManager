using Cloudbass.DataAccess.Repositories.Contracts;
using Cloudbass.Database.Models;
using Cloudbass.Types;
using Cloudbass.Types.Jobs;
using Cloudbass.Types.Schedules;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CloudbassManager.Queries
{
    [ExtendObjectType(Name = "Query")]
    public class Query
    {
        private readonly IClientRepository _clientRepository;
        private readonly IJobRepository _jobRepository;
        private readonly IScheduleRepository _scheduleRepository;
        public Query(IClientRepository clientRepository, IJobRepository jobRepository, IScheduleRepository scheduleRepository)
        {
            _clientRepository = clientRepository;
            _jobRepository = jobRepository;
            _scheduleRepository = scheduleRepository;
        }


        [UsePaging(SchemaType = typeof(ClientType))]
        [UseFiltering]
        public IQueryable<Client> Clients => _clientRepository.GetAll();



        [UsePaging(SchemaType = typeof(JobType))]
        [UseFiltering]
        public IQueryable<Job> Jobs => _jobRepository.GetAll();


        [UsePaging(SchemaType = typeof(ScheduleType))]
        [UseFiltering]
        public IQueryable<Schedule> Schedules => _scheduleRepository.GetAll();
    }
}

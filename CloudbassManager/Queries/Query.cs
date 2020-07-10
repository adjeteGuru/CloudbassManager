using Cloudbass.DataAccess.Repositories.Contracts;
using Cloudbass.Database.Models;
using Cloudbass.Types;
using Cloudbass.Types.Counties;
using Cloudbass.Types.Crews;
using Cloudbass.Types.Employees;
using Cloudbass.Types.HasRoles;
using Cloudbass.Types.Jobs;
using Cloudbass.Types.Roles;
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
        private readonly ICountyRepository _countyRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IHasRoleRepository _hasRoleRepository;
        private readonly ICrewRepository _crewRepository;
        public Query(IClientRepository clientRepository,
            IJobRepository jobRepository,
            IScheduleRepository scheduleRepository,
            ICountyRepository countyRepository,
            IEmployeeRepository employeeRepository,
            IRoleRepository roleRepository,
            IHasRoleRepository hasRoleRepository,
            ICrewRepository crewRepository
            )
        {
            _clientRepository = clientRepository;
            _jobRepository = jobRepository;
            _scheduleRepository = scheduleRepository;
            _countyRepository = countyRepository;
            _employeeRepository = employeeRepository;
            _roleRepository = roleRepository;
            _hasRoleRepository = hasRoleRepository;
            _crewRepository = crewRepository;

        }


        [UsePaging(SchemaType = typeof(ClientType))]
        [UseFiltering]
        public IQueryable<Client> Clients => _clientRepository.GetAllClients();



        [UsePaging(SchemaType = typeof(JobType))]
        [UseFiltering]
        public IQueryable<Job> Jobs => _jobRepository.GetAllJobs();


        [UsePaging(SchemaType = typeof(ScheduleType))]
        [UseFiltering]
        public IQueryable<Schedule> Schedules => _scheduleRepository.GetAll();


        [UsePaging(SchemaType = typeof(CountyType))]
        [UseFiltering]
        public IQueryable<County> Counties => _countyRepository.GetAllCounty();

        [UsePaging(SchemaType = typeof(RoleType))]
        [UseFiltering]
        public IQueryable<Role> Roles => _roleRepository.GetAllRole();

        //[UsePaging(SchemaType = typeof(HasRoleType))]
        //[UseFiltering]
        //public IQueryable<HasRole> HasRoles => _hasRoleRepository.GetAll();

        //[UsePaging(SchemaType = typeof(CrewType))]
        //[UseFiltering]
        //public IQueryable<Crew> Crews => _crewRepository.GetAll();

        [UsePaging(SchemaType = typeof(EmployeeType))]
        [UseFiltering]
        public IQueryable<Employee> Employees => _employeeRepository.GetAllEmployee();
    }
}

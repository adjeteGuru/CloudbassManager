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
using Cloudbass.Types.Users;
using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
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
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IHasRoleRepository _hasRoleRepository;
        private readonly ICrewRepository _crewRepository;
        public Query(IClientRepository clientRepository,
            IJobRepository jobRepository,
            IScheduleRepository scheduleRepository,
            ICountyRepository countyRepository,
            IEmployeeRepository employeeRepository,
            IUserRepository userRepository,
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
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _hasRoleRepository = hasRoleRepository;
            _crewRepository = crewRepository;

        }


        [UsePaging(SchemaType = typeof(ClientType))]
        [UseFiltering]
        public Task<IEnumerable<Client>> Clients => _clientRepository.GetAllClientsAsync();



        [UsePaging(SchemaType = typeof(JobType))]
        [UseFiltering]
        public Task<IEnumerable<Job>> Jobs => _jobRepository.GetAllJobsAsync();


        [UsePaging(SchemaType = typeof(ScheduleType))]
        [UseFiltering]
        public Task<IEnumerable<Schedule>> Schedules => _scheduleRepository.GetAllSchedulesAsync();


        [UsePaging(SchemaType = typeof(CountyType))]
        [UseFiltering]
        public Task<IEnumerable<County>> Counties => _countyRepository.GetAllCountyAsync();

        [UsePaging(SchemaType = typeof(RoleType))]
        [UseFiltering]
        public Task<IEnumerable<Role>> Roles => _roleRepository.GetAllRolesAsync();

        [UsePaging(SchemaType = typeof(HasRoleType))]
        [UseFiltering]
        public Task<IEnumerable<HasRole>> HasRoles => _hasRoleRepository.GetAllHasRolesAsync();

        [UsePaging(SchemaType = typeof(CrewType))]
        [UseFiltering]
        public Task<IEnumerable<Crew>> Crews => _crewRepository.GetCrewAsync();

        [UsePaging(SchemaType = typeof(EmployeeType))]
        [UseFiltering]
        public Task<IEnumerable<Employee>> Employees => _employeeRepository.GetAllEmployeesAsync();

        [Authorize]
        [UsePaging(SchemaType = typeof(UserType))]
        [UseFiltering]
        public Task<IEnumerable<User>> Users => _userRepository.GetAllUsersAsync();



        [UseFiltering]
        [UseSorting]
        public Task<Employee> GetEmployeeByEmailAsync(
                   string email,
                   EmployeeByEmailDataLoader employeeByEmail,
                   CancellationToken cancellationToken) =>
            employeeByEmail.LoadAsync(email, cancellationToken);



        [UseFiltering]
        [UseSorting]
        public Task<Employee> GetEmployeeByIdAsync(
            Guid id,
            EmployeeByIdDataLoader employeeById,
            CancellationToken cancellationToken) =>
            employeeById.LoadAsync(id, cancellationToken);

    }
}

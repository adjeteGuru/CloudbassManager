using Cloudbass.DataAccess.Repositories;
using Cloudbass.Database.Models;
using Cloudbass.Types.Employees;
using Cloudbass.Types.HasRoles;
using Cloudbass.Types.Jobs;
using GreenDonut;
using HotChocolate.Resolvers;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cloudbass.Types.Crews
{
    public class CrewType : ObjectType<Crew>
    {
        protected override void Configure(IObjectTypeDescriptor<Crew> descriptor)
        {
            //descriptor.Field(x => x.Id).Type<IdType>();
            descriptor.Field(x => x.EmployeeId).Type<IdType>();
            descriptor.Field(x => x.JobId).Type<IdType>();


            //invoke the resolver to allow data fetching with N+1 problems eradicated             
            descriptor.Field("job").Type<NonNullType<JobType>>().Resolver(ctx =>
            {
                var jobRepository = ctx.Service<JobRepository>();

                IDataLoader dataloader = ctx.BatchDataLoader<Guid, Job>(
                    "GetJobsById",

                    jobRepository.GetJobsByIdAsync);

                return dataloader.LoadAsync(ctx.Parent<Job>().Id);
            });


            descriptor.Field("employee").Type<NonNullType<EmployeeType>>().Resolver(ctx =>
            {
                var employeeRepository = ctx.Service<EmployeeRepository>();

                IDataLoader dataloader = ctx.BatchDataLoader<Guid, Employee>(
                    "GetEmployeesById",

                    employeeRepository.GetEmployeesByIdAsync);

                return dataloader.LoadAsync(ctx.Parent<Crew>().EmployeeId);
            });

            descriptor.Field(x => x.EmployeeId)
                .Type<ListType<EmployeeType>>();


            //descriptor.Ignore(t => t.Id);
            //descriptor.Ignore(t => t.JobId);
            //descriptor.Ignore(t => t.EmployeeId);

        }
    }
}

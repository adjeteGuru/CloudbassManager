using Cloudbass.DataAccess.Repositories;
using Cloudbass.Database.Models;
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

            //invoke the resolver to allow data fetching with N+1 problems eradicated             
            descriptor.Field("job").Type<NonNullType<JobType>>().Resolver(ctx =>
            {
                JobRepository jobRepository = ctx.Service<JobRepository>();
                IDataLoader dataloader = ctx.BatchDataLoader<Guid, Job>(
                    "JobById",
                    jobRepository.GetJobsByIdAsync);

                return dataloader.LoadAsync(ctx.Parent<Crew>().JobId);
            });


            descriptor.Field("employee").Type<NonNullType<HasRoleType>>().Resolver(ctx =>
            {
                HasRoleRepository hasRoleRepository = ctx.Service<HasRoleRepository>();
                IDataLoader dataloader = ctx.BatchDataLoader<Guid, HasRole>(
                    "EmployeeRoleById",
                    hasRoleRepository.GetHasRolesByIdAsync);

                return dataloader.LoadAsync(ctx.Parent<Crew>().HasRole.Employee.Id);
            });


            //descriptor.Ignore(t => t.JobId);
            //descriptor.Ignore(t => t.HasRoleId);

        }
    }
}

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

        //NEW
        protected override void Configure(IObjectTypeDescriptor<Crew> descriptor)
        {
            descriptor.Field(x => x.HasRoleId).Type<IdType>();
            descriptor.Field(x => x.JobId).Type<IdType>();
            descriptor.Field(x => x.TotalDays).Type<FloatType>();



            //invoke the resolver to eradicate data fetching N+1 problems             
            descriptor.Field("job").Type<NonNullType<JobType>>().Resolver(ctx =>
            {
                var jobRepository = ctx.Service<JobRepository>();

                IDataLoader dataloader = ctx.BatchDataLoader<Guid, Job>(
                    "GetJobsById",

                    jobRepository.GetJobsByIdAsync);

                //return dataloader.LoadAsync(ctx.Parent<Job>().Id);
                return dataloader.LoadAsync(ctx.Parent<Crew>().JobId);
            });


            descriptor.Field("member").Type<NonNullType<HasRoleType>>().Resolver(ctx =>
            {
                var hasRoleRepository = ctx.Service<HasRoleRepository>();

                IDataLoader dataloader = ctx.BatchDataLoader<Guid, HasRole>(
                    "EmployeeRoleById",

                    hasRoleRepository.GetHasRolesByIdAsync);

                //return dataloader.LoadAsync(ctx.Parent<Crew>().HasRoleId);

                return dataloader.LoadAsync(ctx.Parent<Crew>().HasRoleId);
            });


            descriptor.Ignore(t => t.JobId);
            descriptor.Ignore(t => t.HasRoleId);

        }
        //END
    }
}

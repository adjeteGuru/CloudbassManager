using Cloudbass.DataAccess.Repositories;
using Cloudbass.Database.Models;
using Cloudbass.Types.Crews;
using Cloudbass.Types.HasRoles;
using Cloudbass.Types.Jobs;
using GreenDonut;
using HotChocolate.Resolvers;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cloudbass.Types.Queries
{
    public class QueryType : ObjectType
    {
        protected override void Configure(IObjectTypeDescriptor descriptor)
        {
            descriptor.Field("jobs")
               .UsePaging<JobType>()
               .Resolver(ctx => ctx.Service<JobRepository>().GetAllJobsAsync());


            ////second
            //descriptor.Field("employeesByJob")
            //    .Argument("job", x => x.Type<NonNullType<StringType>>())
            //    .Type<NonNullType<ListType<NonNullType<HasRoleType>>>>()
            //    .Resolver(ctx =>
            //    {
            //        var hasRoleRepository = ctx.Service<HasRoleRepository>();

            //        IDataLoader<string, HasRole[]> dataloader = ctx.GroupDataLoader<string, HasRole>(
            //            "employeesByJob",
            //            hasRoleRepository.GetHasRolesByIdAsync);

            //        return dataloader.LoadAsync(ctx.Argument<Guid>("job"));
            //    });//end
        }
    }
}

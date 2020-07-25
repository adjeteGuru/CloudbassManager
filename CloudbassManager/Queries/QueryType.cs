using Cloudbass.DataAccess.Repositories;
using Cloudbass.Database.Models;
using Cloudbass.Types.Employees;
using Cloudbass.Types.Jobs;
using GreenDonut;
using HotChocolate.Resolvers;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudbassManager.Queries
{
    public class QueryType : ObjectType
    {
        protected override void Configure(IObjectTypeDescriptor descriptor)
        {

            descriptor.Field("jobs")
                .UsePaging<JobType>()
                .Resolver(ctx => ctx.Service<JobRepository>().GetAllJobsAsync());

            //descriptor.Field("job")
            //   .UsePaging<JobType>()
            //   .Resolver(ctx => ctx.Service<JobRepository>().GetJobByIdAsync());



        }
    }
}

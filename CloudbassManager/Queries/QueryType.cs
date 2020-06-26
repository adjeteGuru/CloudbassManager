using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudbassManager.Queries
{
    public class QueryType : ObjectType<Query>
    {
        //protected override void Configure(IObjectTypeDescriptor<Query> descriptor)
        //{
        //    base.Configure(descriptor);

        //    descriptor.Field(x => x.GetSchedule(default))
        //        .Argument("jobId", x => x.Type<NonNullType<IdType>>())
        //        .Type<JobType>();

        //    descriptor.Field(x=>x.GetSchedules(default))
        //        .Argument("")
        //}
    }
}

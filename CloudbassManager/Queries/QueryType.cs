using Cloudbass.Types;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudbassManager.Queries
{
    public class QueryType : ObjectType<Query>
    {
        protected override void Configure(IObjectTypeDescriptor<Query> descriptor)
        {
            //base.Configure(descriptor);

            descriptor.Field(q => q.GetUsers(default))

                .Type<NonNullType<ListType<NonNullType<UserType>>>>();

        }

    }
}

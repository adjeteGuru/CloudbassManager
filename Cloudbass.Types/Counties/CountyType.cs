using Cloudbass.Database.Models;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cloudbass.Types.Counties
{
    public class CountyType : ObjectType<County>
    {
        protected override void Configure(IObjectTypeDescriptor<County> descriptor)
        {
            base.Configure(descriptor);

            descriptor.Field(x => x.Id).Type<NonNullType<IdType>>();
            descriptor.Field(x => x.Name).Type<NonNullType<StringType>>();
        }
    }
}

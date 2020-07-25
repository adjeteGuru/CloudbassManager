using Cloudbass.Database.Models;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cloudbass.Types.Roles
{
    public class RoleType : ObjectType<Role>
    {
        protected override void Configure(IObjectTypeDescriptor<Role> descriptor)
        {
            base.Configure(descriptor);

            descriptor.Field(x => x.Id).Type<NonNullType<IdType>>();
            descriptor.Field(x => x.Name).Type<NonNullType<StringType>>();
        }
    }
}

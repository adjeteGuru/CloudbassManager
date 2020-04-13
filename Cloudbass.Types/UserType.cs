using Cloudbass.Database.Models;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cloudbass.Types
{
    public class UserType : ObjectType<User>
    {

        protected override void Configure(IObjectTypeDescriptor<User> descriptor)
        {
            descriptor.Field(u => u.Id)
                .Type<NonNullType<IdType>>();

            descriptor.Field(u => u.Name)
                .Type<NonNullType<StringType>>();

            descriptor.Field(u => u.Password)
               .Ignore();

            descriptor.Field(u => u.Email)
                .Type<StringType>();

            descriptor.Field(u => u.TokenVersion)
               .Type<NonNullType<StringType>>();

            descriptor.Field(u => u.Active)
                .Type<NonNullType<BooleanType>>();

            descriptor.Field(u => u.CreatedAt)
               .Type<NonNullType<DateType>>();

            descriptor.Field(u => u.Salt)
                .Type<NonNullType<StringType>>();



        }

    }
}

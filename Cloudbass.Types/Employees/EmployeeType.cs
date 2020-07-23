using Cloudbass.DataAccess.Resolvers;
using Cloudbass.Database.Models;
using Cloudbass.Types.Roles;
using Cloudbass.Types.Counties;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Text;
using Cloudbass.DataAccess.Repositories;
using GreenDonut;
using HotChocolate.Resolvers;

namespace Cloudbass.Types.Employees
{
    public class EmployeeType : ObjectType<Employee>
    {
        protected override void Configure(IObjectTypeDescriptor<Employee> descriptor)
        {
            base.Configure(descriptor);

            descriptor.Field(x => x.Id).Type<IdType>();
            descriptor.Field(x => x.FullName).Type<StringType>();
            descriptor.Field(x => x.PostNominals).Type<StringType>();
            descriptor.Field(x => x.Alergy).Type<StringType>();
            descriptor.Field(x => x.Bared).Type<StringType>();
            descriptor.Field(x => x.Email).Type<StringType>();
            descriptor.Field(x => x.NextOfKin).Type<StringType>();
            //descriptor.Field(x => x.CountyId).Type<IdType>();
            descriptor.Field(x => x.Photo).Type<StringType>();

            descriptor.Field(x => x.CanDo)
                .Type<ListType<RoleType>>();

            descriptor.Field("county").Type<NonNullType<CountyType>>().Resolver(ctx =>
            {
                var countyRepository = ctx.Service<CountyRepository>();

                IDataLoader dataLoader = ctx.BatchDataLoader<Guid, County>(
                    "CountyById",
                    countyRepository.GetCountiesByIdAsync);

                return dataLoader.LoadAsync(ctx.Parent<Employee>().CountyId);
            });



            //descriptor.Field<CountyResolver>(x => x.GetCounty(default, default));

            //descriptor.Field<UserResolver>(x => x.GetUsers(default, default));
        }
    }
}

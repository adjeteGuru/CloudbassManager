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
using Cloudbass.Types.Jobs;

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
            descriptor.Field(x => x.Photo).Type<StringType>();


            //Below is the technique of queueing up ids called batching
            descriptor.Field("county").Type<NonNullType<CountyType>>().Resolver(ctx =>
            {
                var countyRepository = ctx.Service<CountyRepository>();

                //1- (BatchDataLoader)it waits until all the counties ids are queued.
                IDataLoader dataLoader = ctx.BatchDataLoader<Guid, County>(
                    "CountyById",

                    //2- Then it fires of the GetCountiesByIdAsync method only when all the ids are collected.
                    countyRepository.GetCountiesByIdAsync);

                return dataLoader.LoadAsync(ctx.Parent<Employee>().CountyId);
            });

            descriptor.Ignore(t => t.Id);



            //descriptor.Field<CountyResolver>(x => x.GetCounty(default, default));

            //descriptor.Field<UserResolver>(x => x.GetUsers(default, default));
        }
    }
}

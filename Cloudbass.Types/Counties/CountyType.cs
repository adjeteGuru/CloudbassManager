using Cloudbass.DataAccess.Repositories;
using Cloudbass.Database.Models;
using Cloudbass.Types.Employees;
using GreenDonut;
using HotChocolate.Resolvers;
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

            //
            descriptor.Field("employees")
              .Argument("countyId", a => a.Type<NonNullType<IdType>>())
              .Type<NonNullType<ListType<NonNullType<EmployeeType>>>>()
              .Resolver(ctx =>

              {

                  var employeeRepository = ctx.Service<EmployeeRepository>();

                  IDataLoader dataLoader = ctx.GroupDataLoader<Guid, Employee>(
                        "EmployeeByIds",
                        employeeRepository.GetEmployeesByCountyIdAsync);

                  return dataLoader.LoadAsync(ctx.Argument<Guid>("countyId"));

              });
        }
    }
}

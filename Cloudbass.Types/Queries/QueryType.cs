using Cloudbass.DataAccess.Repositories;
using Cloudbass.Database.Models;
using Cloudbass.Types.Counties;
using Cloudbass.Types.Crews;
using Cloudbass.Types.Employees;
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
            descriptor.Field("employee")
               .UsePaging<EmployeeType>()
               .Resolver(ctx => ctx.Service<EmployeeRepository>().GetAllEmployeesAsync());


            ////second
            descriptor.Field("employeesByCounty")
                .Argument("countys", x => x.Type<NonNullType<StringType>>())
                .Type<NonNullType<ListType<NonNullType<EmployeeType>>>>()
                .Resolver(ctx =>
                {
                    var employeeRepository = ctx.Service<EmployeeRepository>();

                    IDataLoader<string, Employee[]> dataloader = ctx.GroupDataLoader<string, Employee>(
                        "employeesByCounty",
                        employeeRepository.GetEmployeesByCounty);

                    return dataloader.LoadAsync(ctx.Argument<string>("countys"));
                });//end
        }
    }
}

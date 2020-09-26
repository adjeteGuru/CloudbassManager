using Cloudbass.DataAccess.Repositories;
using Cloudbass.Database.Models;
using Cloudbass.Types.Employees;
using Cloudbass.Types.Jobs;
using Cloudbass.Types.Roles;
using GreenDonut;
using HotChocolate.Resolvers;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cloudbass.Types.HasRoles
{
    public class HasRoleType : ObjectType<HasRole>
    {

        //NEW
        protected override void Configure(IObjectTypeDescriptor<HasRole> descriptor)
        {
            base.Configure(descriptor);

            descriptor.Field(x => x.Id).Type<IdType>();
            descriptor.Field(x => x.Rate).Type<FloatType>();


            //invoke the resolver to eradicate data fetching N + 1 problems
            descriptor.Field("whoIsInvolved").Type<NonNullType<EmployeeType>>().Resolver(ctx =>
            {
                var employeeRepository = ctx.Service<EmployeeRepository>();

                //Idea behind BatchLoader is that it waits until all the Employee ids are queued
                IDataLoader dataloader = ctx.BatchDataLoader<Guid, Employee>(

                    "GetEmployeeById",

                    //Then it fires of the GetEmployeesByIdAsync method only when all the ids are collected
                    employeeRepository.GetEmployeesByIdAsync);

                //Once the dictionary of Employees is returned with the passed in ids;
                //an Employee that belongs to a particular HasRole is returned from the field
                return dataloader.LoadAsync(ctx.Parent<HasRole>().EmployeeId);
            });


            //role
            descriptor.Field("roleUndertaken").Type<NonNullType<RoleType>>().Resolver(ctx =>
            {
                var roleRepository = ctx.Service<RoleRepository>();

                IDataLoader dataLoader = ctx.BatchDataLoader<Guid, Role>(

                    "GetRoleById",
                    roleRepository.GetRolesByIdAsync);

                return dataLoader.LoadAsync(ctx.Parent<HasRole>().RoleId);
            });

            descriptor.Ignore(t => t.EmployeeId);
            descriptor.Ignore(t => t.RoleId);
            descriptor.Ignore(t => t.Id);

        }

        //END
    }
}

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
        protected override void Configure(IObjectTypeDescriptor<HasRole> descriptor)
        {
            base.Configure(descriptor);

            descriptor.Field(x => x.Id).Type<IdType>();
            descriptor.Field(x => x.Rate).Type<FloatType>();
            descriptor.Field(x => x.TotalDays).Type<FloatType>();

            descriptor.Field(x => x.InvolvedIn)
                .Type<ListType<JobType>>();

            //invoke the resolver to allow data fetching with N+1 problems eradicated             
            descriptor.Field("employee").Type<NonNullType<EmployeeType>>().Resolver(ctx =>
            {
                var employeeRepository = ctx.Service<EmployeeRepository>();
                IDataLoader dataloader = ctx.BatchDataLoader<Guid, Employee>(
                    "EmployeeById",
                    employeeRepository.GetEmployeesByIdAsync);

                return dataloader.LoadAsync(ctx.Parent<HasRole>().EmployeeId);
            });



            descriptor.Field("role").Type<NonNullType<RoleType>>().Resolver(ctx =>
           {
               var roleRepository = ctx.Service<RoleRepository>();
               IDataLoader dataLoader = ctx.BatchDataLoader<Guid, Role>(
                   "RoleById",
                   roleRepository.GetRolesByIdAsync);

               return dataLoader.LoadAsync(ctx.Parent<HasRole>().RoleId);
           });

            descriptor.Ignore(t => t.EmployeeId);
            descriptor.Ignore(t => t.RoleId);
            descriptor.Ignore(t => t.Id);

        }
    }
}

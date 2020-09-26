using HotChocolate.Types;
using Cloudbass.Database.Models;
using Cloudbass.DataAccess.Resolvers;
using Cloudbass.DataAccess.Repositories.Contracts;
using System.Collections;
using HotChocolate;
using HotChocolate.Resolvers;
using System.Collections.Generic;
using System.Linq;
using Cloudbass.DataAccess.Repositories;
using System;
using GreenDonut;
using Cloudbass.Types.Schedules;
using Cloudbass.Types.Crews;
using Cloudbass.Types.HasRoles;
using Cloudbass.Types.Employees;

namespace Cloudbass.Types.Jobs
{
    public class JobType : ObjectType<Job>
    {

        protected override void Configure(IObjectTypeDescriptor<Job> descriptor)
        {
            descriptor.Field(x => x.Id).Type<IdType>();
            descriptor.Field(x => x.Name).Type<StringType>();
            descriptor.Field(x => x.Location).Type<StringType>();
            descriptor.Field(x => x.CreatedAt).Type<DateTimeType>();
            descriptor.Field(x => x.StartDate).Type<DateTimeType>();
            descriptor.Field(x => x.TXDate).Type<DateTimeType>();
            descriptor.Field(x => x.EndDate).Type<DateTimeType>();
            descriptor.Field(x => x.Coordinator).Type<StringType>();
            descriptor.Field(x => x.Paid).Type<BooleanType>();
            descriptor.Field(x => x.CommercialLead).Type<StringType>();
            descriptor.Field(x => x.JobRef).Type<StringType>();
            //descriptor.Field(x => x.CreatedBy).Type<StringType>();
            descriptor.Field(x => x.Status).Type<EnumType<Status>>();


            //this resolver eradicate N+1 problems  
            descriptor.Field("client").Type<NonNullType<ClientType>>().Resolver(ctx =>

            {
                var clientRepository = ctx.Service<ClientRepository>();

                IDataLoader dataloader = ctx.BatchDataLoader<Guid, Client>(
                    "GetClientsById",
                    clientRepository.GetClientsByIdAsync);

                return dataloader.LoadAsync(ctx.Parent<Job>().ClientId);

            });


            //schedule
            //descriptor.Field("schedules")
            //  .Argument("jobRef", a => a.Type<NonNullType<IdType>>())
            //  .Type<NonNullType<ListType<NonNullType<ScheduleType>>>>()
            //  .Resolver(ctx =>
            //  {
            //      var scheduleRepository = ctx.Service<ScheduleRepository>();

            //      IDataLoader userDataLoader =
            //          ctx.GroupDataLoader<Guid, Schedule>(
            //              "GetSchedulesByJobId",

            //              scheduleRepository.GetSchedulesByJobIdAsync);

            //      return userDataLoader.LoadAsync(ctx.Argument<Guid>("jobRef"));
            //  });

            descriptor.Field("schedules")
             .Argument("jobRef", a => a.Type<NonNullType<StringType>>())
             .Type<NonNullType<ListType<NonNullType<ScheduleType>>>>()
             .Resolver(ctx =>
             {
                 var scheduleRepository = ctx.Service<ScheduleRepository>();

                 IDataLoader userDataLoader =
                     ctx.GroupDataLoader<string, Schedule>(
                         "GetSchedulesByJobId",

                         scheduleRepository.GetSchedulesByJobIdAsync);

                 return userDataLoader.LoadAsync(ctx.Argument<string>("jobRef"));
             });



            // crew
            descriptor.Field("jobsCrew")
            .Argument("jobId", a => a.Type<NonNullType<IdType>>())
            .Type<NonNullType<ListType<NonNullType<CrewType>>>>()
            .Resolver(ctx =>
            {
                var crewRepository = ctx.Service<HasRoleRepository>();


                IDataLoader userDataLoader =
                    ctx.GroupDataLoader<Guid, Crew>(
                        "GetEmployeesByIJobd",
                        crewRepository.GetEmployeesByJobIdAsync);

                return userDataLoader.LoadAsync(ctx.Argument<Guid>("jobId"));

            });



            //this resolver allows to fetch Employee who has logged the job (with N+1 problems eradicated)
            descriptor.Field("createdBy").Type<NonNullType<EmployeeType>>().Resolver(ctx =>
            {
                var employeeRepository = ctx.Service<EmployeeRepository>();

                IDataLoader dataLoader = ctx.BatchDataLoader<string, Employee>(
                    "GetEmployeeByName",
                    employeeRepository.GetEmployeesByNameAsync);

                return dataLoader.LoadAsync(ctx.Parent<Job>().CreatedBy);
            });



            descriptor.Ignore(t => t.ClientId);
            descriptor.Ignore(t => t.Id);




        }


        //protected override void Configure(IObjectTypeDescriptor<Job> descriptor)
        //{
        //    descriptor.Field(x => x.Id).Type<IdType>();
        //    descriptor.Field(x => x.Name).Type<StringType>();
        //    descriptor.Field(x => x.Location).Type<StringType>();
        //    descriptor.Field(x => x.CreatedAt).Type<DateTimeType>();
        //    descriptor.Field(x => x.StartDate).Type<DateTimeType>();
        //    descriptor.Field(x => x.TXDate).Type<DateTimeType>();
        //    descriptor.Field(x => x.EndDate).Type<DateTimeType>();
        //    descriptor.Field(x => x.Coordinator).Type<StringType>();
        //    descriptor.Field(x => x.Paid).Type<BooleanType>();
        //    descriptor.Field(x => x.CommercialLead).Type<StringType>();
        //    //descriptor.Field(x => x.CreatedBy).Type<StringType>();
        //    descriptor.Field(x => x.Status).Type<EnumType<Status>>();

        //    descriptor.Field(x => x.CrewMembers)
        //        .Type<ListType<CrewType>>();

        //    //this resolver allows to fetch Employee who has logged the job (with N+1 problems eradicated) 
        //    descriptor.Field("client").Type<NonNullType<ClientType>>().Resolver(ctx =>

        //    {
        //        var clientRepository = ctx.Service<ClientRepository>();

        //        IDataLoader dataloader = ctx.BatchDataLoader<Guid, Client>(
        //            "GetClientsById",
        //            clientRepository.GetClientsByIdAsync);

        //        return dataloader.LoadAsync(ctx.Parent<Job>().ClientId);

        //    });


        //    //schedule
        //    descriptor.Field("schedules")
        //      .Argument("jobId", a => a.Type<NonNullType<IdType>>())
        //      .Type<NonNullType<ListType<NonNullType<ScheduleType>>>>()
        //      .Resolver(ctx =>
        //      {
        //          var scheduleRepository = ctx.Service<ScheduleRepository>();

        //          IDataLoader userDataLoader =
        //              ctx.GroupDataLoader<Guid, Schedule>(
        //                  "GetSchedulesByJobId",

        //                  scheduleRepository.GetSchedulesByJobIdAsync);

        //          return userDataLoader.LoadAsync(ctx.Argument<Guid>("jobId"));
        //      });


        //    ////employee
        //    descriptor.Field("employees")
        //    .Argument("jobId", a => a.Type<NonNullType<IdType>>())
        //    .Type<NonNullType<ListType<NonNullType<CrewType>>>>()
        //    .Resolver(ctx =>
        //    {
        //        var employeeRepository = ctx.Service<EmployeeRepository>();


        //        IDataLoader userDataLoader =
        //            ctx.GroupDataLoader<Guid, Crew>(

        //                "GetEmployeesByIJobd",

        //                employeeRepository.GetEmployeesByJobIdAsync);

        //        return userDataLoader.LoadAsync(ctx.Argument<Guid>("jobId"));

        //    });

        //    //this resolver allows to fetch Employee who has logged the job (with N+1 problems eradicated)
        //    descriptor.Field("crewMembers").Type<NonNullType<CrewType>>().Resolver(ctx =>
        //    {
        //        var employeeRepository = ctx.Service<CrewRepository>();

        //        IDataLoader dataLoader = ctx.BatchDataLoader<Guid, Crew>(
        //            "GetEmployeeInvolved",
        //            employeeRepository.GetCrewMembersByIdAsync);

        //        return dataLoader.LoadAsync(ctx.Parent<Job>().CrewMembers);
        //    });


        //    ////crew
        //    //descriptor.Field("crew")
        //    //.Argument("jobId", a => a.Type<NonNullType<IdType>>())
        //    //.Type<NonNullType<ListType<NonNullType<CrewType>>>>()
        //    //.Resolver(ctx =>
        //    //{
        //    //    var crewRepository = ctx.Service<CrewRepository>();


        //    //    IDataLoader userDataLoader =
        //    //        ctx.GroupDataLoader<Guid, Crew>(

        //    //            "crewMemberByIds",

        //    //            crewRepository.GetCrewMembersByJobIdAsync);

        //    //    return userDataLoader.LoadAsync(ctx.Argument<Guid>("jobId"));

        //    //});



        //    //this resolver allows to fetch Employee who has logged the job (with N+1 problems eradicated)
        //    descriptor.Field("createdBy").Type<NonNullType<EmployeeType>>().Resolver(ctx =>
        //    {
        //        var employeeRepository = ctx.Service<EmployeeRepository>();

        //        IDataLoader dataLoader = ctx.BatchDataLoader<string, Employee>(
        //            "GetEmployeeByName",
        //            employeeRepository.GetEmployeesByNameAsync);

        //        return dataLoader.LoadAsync(ctx.Parent<Job>().CreatedBy);
        //    });



        //}
    }


}

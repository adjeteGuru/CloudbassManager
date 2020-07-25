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
            //descriptor.Field(x => x.CreatedBy).Type<StringType>();
            descriptor.Field(x => x.Status).Type<EnumType<Status>>();

            descriptor.Field(x => x.CrewMembers)
                .Type<ListType<CrewType>>();

            //this resolver allows to fetch Employee who has logged the job (with N+1 problems eradicated) 
            descriptor.Field("client").Type<NonNullType<ClientType>>().Resolver(ctx =>

            {
                var clientRepository = ctx.Service<ClientRepository>();

                IDataLoader dataloader = ctx.BatchDataLoader<Guid, Client>(
                    "ClientById",
                    clientRepository.GetClientsByIdAsync);

                return dataloader.LoadAsync(ctx.Parent<Job>().ClientId);

            });





            //schedule
            descriptor.Field("schedules")
              .Argument("jobId", a => a.Type<NonNullType<IdType>>())
              .Type<NonNullType<ListType<NonNullType<ScheduleType>>>>()
              .Resolver(ctx =>
              {
                  var scheduleRepository = ctx.Service<ScheduleRepository>();

                  IDataLoader userDataLoader =
                      ctx.GroupDataLoader<Guid, Schedule>(
                          "ScheduleByIds",
                          scheduleRepository.GetSchedulesByJobIdAsync);

                  return userDataLoader.LoadAsync(ctx.Argument<Guid>("jobId"));
              });


            //hasRole
            //descriptor.Field("hasRoleInvolved")
            //.Argument("whohas", a => a.Type<NonNullType<IdType>>())
            //.Type<NonNullType<ListType<NonNullType<HasRoleType>>>>()
            //.Resolver(ctx =>
            //{
            //    var hasRoleRepository = ctx.Service<HasRoleRepository>();

            //    IDataLoader userDataLoader =
            //        ctx.GroupDataLoader<Guid, HasRole>(
            //            "hasRoleInvolved",
            //            hasRoleRepository.GetEmployeesInvoledInJob);

            //    return userDataLoader.LoadAsync(ctx.Argument<Guid>("whohas"));

            //});


            //crew
            descriptor.Field("crew")
            .Argument("jobId", a => a.Type<NonNullType<IdType>>())
            .Type<NonNullType<ListType<NonNullType<CrewType>>>>()
            .Resolver(ctx =>
            {
                var crewRepository = ctx.Service<CrewRepository>();


                IDataLoader userDataLoader =
                    ctx.GroupDataLoader<Guid, Crew>(

                        "crewMemberByIds",

                        crewRepository.GetCrewMembersByJobIdAsync);

                return userDataLoader.LoadAsync(ctx.Argument<Guid>("jobId"));

            });


            //trial
            descriptor.Field("findEmployee")
                .Argument("fullname", x => x.Type<NonNullType<StringType>>())
                .Type<NonNullType<ListType<NonNullType<EmployeeType>>>>()
                .Resolver(ctx =>
                {
                    var employeeRepository = ctx.Service<EmployeeRepository>();

                    IDataLoader dataLoader = ctx.GroupDataLoader<string, Employee>(
                        "findEmployee",
                        employeeRepository.GetEmployeesByFullName);
                    return dataLoader.LoadAsync(ctx.Argument<string>("fullname"));
                });



            //this resolver allows to fetch Employee who has logged the job (with N+1 problems eradicated)
            descriptor.Field("createdBy").Type<NonNullType<EmployeeType>>().Resolver(ctx =>
            {
                var employeeRepository = ctx.Service<EmployeeRepository>();

                IDataLoader dataLoader = ctx.BatchDataLoader<string, Employee>(
                    "ByName",
                    employeeRepository.GetEmployeesByNameAsync);

                return dataLoader.LoadAsync(ctx.Parent<Job>().CreatedBy);
            });


            //descriptor.Field("scheduleTo").Type<JobType>()
            //         .Resolver(async ctx =>
            //         {
            //             Guid? id = ctx.Parent<Job>().Id;
            //             if (id.HasValue)
            //             {
            //                 JobRepository jobRepository = ctx.Service<JobRepository>();

            //                 IDataLoader<Guid, Job> dataLoader = ctx.CacheDataLoader<Guid, Job>(
            //                      "JobById",
            //                      jobRepository.GetJobAsync);

            //                 return await dataLoader.LoadAsync(ctx.Parent<Job>().Id);
            //             }

            //             return null;

            //         });

            descriptor.Ignore(t => t.ClientId);
            descriptor.Ignore(t => t.Id);


            ////crew resolver
            //descriptor.Field("members").Type<NonNullType<CrewType>>()
            //    .Resolver(ctx =>
            //{
            //    var crewRepository = ctx.Service<CrewRepository>();
            //    IDataLoader dataloader = ctx.BatchDataLoader<int, Crew>(
            //        "CrewMemberById",
            //        crewRepository.GetCrewMembersByIdAsync);                   

            //    return dataloader.LoadAsync(ctx.Parent<Crew>().Id);
            //});


            ////first
            //descriptor.Field("employeesByJob")
            //    .Argument("job", x => x.Type<NonNullType<StringType>>())
            //    .Type<NonNullType<ListType<NonNullType<CrewType>>>>()
            //    .Resolver(ctx =>
            //    {
            //        var crewRepository = ctx.Service<CrewRepository>();

            //        IDataLoader<int, Crew[]> dataloader =
            //        ctx.GroupDataLoader<int, Crew>(
            //            "employeesByJob",
            //            crewRepository.GetEmployeesByJob);

            //        return dataloader.LoadAsync(ctx.Argument<Guid>("job"));
            //    });









            //descriptor.Field("schedule").Type<NonNullType<ScheduleType>>().Resolver(ctx =>
            //{
            //    ScheduleRepository scheduleRepository = ctx.Service<ScheduleRepository>();
            //    IDataLoader dataloader = ctx.BatchDataLoader<Guid, Schedule>(
            //        "ScheduleById",
            //        scheduleRepository.GetClientsAsync);

            //    return dataloader.LoadAsync(ctx.Parent<Job>().ClientId);

            //});

            //able to get the information of clients when we make a query about jobs
            // descriptor.Field<ClientResolver>(t => t.GetClient(default, default));

            // descriptor.Field<ScheduleResolver>(x => x.GetSchedules(default, default));

            //descriptor.Field<JobResolver>(x => x.GetJobForClient(clientId, default));

        }
    }

    //public class ScheduleResolver
    //{
    //    private readonly IScheduleRepository _scheduleRepository;
    //    private readonly IJobRepository _jobRepository;
    //    public ScheduleResolver([Service] IScheduleRepository scheduleRepository, [Service] IJobRepository jobRepository)
    //    {
    //        _scheduleRepository = scheduleRepository;
    //        _jobRepository = jobRepository;
    //    }

    //    public IEnumerable<Schedule> GetSchedules(Job job, IResolverContext ctx)
    //    {
    //        return _scheduleRepository.GetAll().Where(x => x.JobId == job.Id);
    //    }

    //    public Job GetJob(Schedule schedule, IResolverContext ctx)
    //    {
    //        return _jobRepository.GetAll().Where(x => x.Id == schedule.JobId).FirstOrDefault();
    //    }
    //}
}

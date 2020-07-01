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
            descriptor.Field(x => x.Status).Type<StringType>();

            //invoke the resolver to allow data fetching with N+1 problems eradicated             
            descriptor.Field("client").Type<NonNullType<ClientType>>().Resolver(ctx =>
            {
                ClientRepository clientRepository = ctx.Service<ClientRepository>();
                IDataLoader dataloader = ctx.BatchDataLoader<Guid, Client>(
                    "ClientById",
                    clientRepository.GetClientsAsync);

                return dataloader.LoadAsync(ctx.Parent<Job>().ClientId);
            });



            descriptor.Field("job").Type<JobType>().Resolver(async ctx =>
            {
                Guid? id = ctx.Parent<Job>().Id;
                if (id.HasValue)
                {
                    JobRepository jobRepository = ctx.Service<JobRepository>();

                    IDataLoader<Guid, Job> dataLoader = ctx.CacheDataLoader<Guid, Job>(
                        "JobById",
                        jobRepository.GetJobAsync);

                    return await dataLoader.LoadAsync(ctx.Parent<Job>().Id);
                }
                return null;
            });

            descriptor.Ignore(t => t.ClientId);
            descriptor.Ignore(t => t.Id);


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

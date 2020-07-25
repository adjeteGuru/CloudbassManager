using Cloudbass.DataAccess.Repositories;
using Cloudbass.DataAccess.Resolvers;
using Cloudbass.Database.Models;
using Cloudbass.Types.Jobs;
using GreenDonut;
using HotChocolate.Resolvers;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cloudbass.Types.Schedules
{
    public class ScheduleType : ObjectType<Schedule>
    {
        protected override void Configure(IObjectTypeDescriptor<Schedule> descriptor)
        {
            descriptor.Field(x => x.Id).Type<IdType>();
            descriptor.Field(x => x.Name).Type<StringType>();
            descriptor.Field(x => x.Description).Type<StringType>();
            descriptor.Field(x => x.StartDate).Type<DateTimeType>();
            descriptor.Field(x => x.EndDate).Type<DateTimeType>();
            //descriptor.Field(x => x.Status).Type<StringType>();
            descriptor.Field(x => x.Status).Type<EnumType<Status>>();

            //this resolver allows to fetch Employee who has logged the job (with N+1 problems eradicated) 
            descriptor.Field("job").Type<NonNullType<JobType>>().Resolver(ctx =>

            {
                var jobRepository = ctx.Service<JobRepository>();

                IDataLoader dataloader = ctx.BatchDataLoader<Guid, Job>(
                    "JobById",
                    jobRepository.GetJobsByIdAsync);

                return dataloader.LoadAsync(ctx.Parent<Schedule>().JobId);

            });


            //descriptor.Field<JobResolver>(x => x.GetJobOnSched(default, default));



        }
    }
}

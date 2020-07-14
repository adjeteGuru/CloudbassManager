using Cloudbass.DataAccess.Resolvers;
using Cloudbass.Database.Models;
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
            descriptor.Field(x => x.Status).Type<StringType>();


            //descriptor.Field<JobResolver>(x => x.GetJobOnSched(default, default));

        }
    }
}

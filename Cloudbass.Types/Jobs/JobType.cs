using HotChocolate.Types;
using Cloudbass.Database.Models;
using Cloudbass.DataAccess.Repositories;

namespace Cloudbass.Types.Jobs
{
    public class JobType : ObjectType<Job>
    {
        protected override void Configure(IObjectTypeDescriptor<Job> descriptor)
        {
            base.Configure(descriptor);
            descriptor.Field(x => x.Id).Type<NonNullType<IdType>>();
            descriptor.Field(x => x.Text).Type<NonNullType<StringType>>();
            descriptor.Field(x => x.Location).Type<NonNullType<StringType>>();
            descriptor.Field(x => x.CreatedAt).Type<NonNullType<DateTimeType>>();
            descriptor.Field(x => x.StartDate).Type<NonNullType<DateTimeType>>();
            descriptor.Field(x => x.EndDate).Type<NonNullType<DateTimeType>>();
            descriptor.Field(x => x.Coordinator).Type<NonNullType<StringType>>();
            descriptor.Field(x => x.Paid).Type<NonNullType<BooleanType>>();
            descriptor.Field(x => x.CommercialLead).Type<NonNullType<StringType>>();
            descriptor.Field(x => x.Status).Type<NonNullType<EnumType>>();
            //test
            // descriptor.Field(x => x.ClientId).Type<NonNullType<IdType>>();
            //descriptor.Field<JobRepository>(x => x.GetJobsForClient(default, default))
            //    .Type<ClientType>()
            //    .Argument("jobs", x => x.Type<NonNullType<StringType>>()).
            //    Name("");

        }
    }
}

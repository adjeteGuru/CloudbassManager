using HotChocolate.Types;
using Cloudbass.Database.Models;
using Cloudbass.DataAccess.Repositories;
using Cloudbass.Utilities.Resolvers;

namespace Cloudbass.Types.Jobs
{
    public class JobType : ObjectType<Job>
    {
        protected override void Configure(IObjectTypeDescriptor<Job> descriptor)
        {
            descriptor.Field(x => x.Id).Type<NonNullType<IdType>>();
            descriptor.Field(x => x.Text).Type<NonNullType<StringType>>();
            descriptor.Field(x => x.Location).Type<NonNullType<StringType>>();
            descriptor.Field(x => x.CreatedAt).Type<NonNullType<DateTimeType>>();
            descriptor.Field(x => x.StartDate).Type<NonNullType<DateTimeType>>();
            descriptor.Field(x => x.TXDate).Type<NonNullType<DateTimeType>>();
            descriptor.Field(x => x.EndDate).Type<NonNullType<DateTimeType>>();
            descriptor.Field(x => x.Coordinator).Type<NonNullType<StringType>>();
            descriptor.Field(x => x.Paid).Type<NonNullType<BooleanType>>();
            descriptor.Field(x => x.CommercialLead).Type<NonNullType<StringType>>();
            descriptor.Field(x => x.Status).Type<NonNullType<EnumType>>();
            descriptor.Field<ClientResolvers>(t => t.GetClient(default, default));

            //descriptor.Field<JobRepository>(x => x.GetJobsForClient(default, default))
            //   .Type<ClientType>()
            //   .Argument("clientId", x => x.Type<NonNullType<IntType>>())
            //   .Name("jobs");
            //test


        }
    }
}

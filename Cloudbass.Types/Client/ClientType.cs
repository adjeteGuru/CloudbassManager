
using Cloudbass.DataAccess.Repositories;

using Cloudbass.Database.Models;
using HotChocolate.Types;


namespace Cloudbass.Types
{
    //To allow the creation of graphQl request we need to create Types and 
    //this define which field the query want to return if user ask for them
    public class ClientType : ObjectType<Client>
    {

        protected override void Configure(IObjectTypeDescriptor<Client> descriptor)
        {
            base.Configure(descriptor);
            descriptor.Field(x => x.Id).Type<NonNullType<IdType>>();
            descriptor.Field(x => x.Name).Type<NonNullType<StringType>>();
            descriptor.Field(x => x.Tel).Type<NonNullType<StringType>>();
            descriptor.Field(x => x.Email).Type<NonNullType<StringType>>();
            descriptor.Field(x => x.ToContact).Type<NonNullType<StringType>>();

            //
            descriptor.Field<JobRepository>(x => x.GetJobsForClient(default, default))
                .Type<ClientType>()
                //.Type<StringType>()
                .Argument("clientId", x => x.Type<NonNullType<IntType>>())
                .Name("jobs");
        }
    }
}

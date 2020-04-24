
using Cloudbass.DataAccess.Repositories;
using Cloudbass.Database;
using Cloudbass.Database.Models;
using Cloudbass.Utilities.Resolvers;
using HotChocolate;
using HotChocolate.Resolvers;
using HotChocolate.Types;
using System.Collections.Generic;
using System.Linq;

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

            //invoke the resolver to allow data fetching
            descriptor.Field<JobResolvers>(t => t.GetJobs(default, default));
            //
            //descriptor.Field<JobRepository>(x => x.GetJobsForClient(default, default))
            //    .Type<ClientType>()
            //    .Argument("clientId", x => x.Type<NonNullType<IntType>>())
            //    .Name("jobs");
        }

        //public class JobResolvers
        //{
        //    private readonly CloudbassContext _db;
        //    public JobResolvers([Service] CloudbassContext db)
        //    {
        //        _db = db;
        //    }



        //    public IEnumerable<Job> GetJobs(Client client, IResolverContext ctx)
        //    {
        //        return _db.Jobs.Where(a => a.ClientId == client.Id);
        //    }
        //}
    }
}

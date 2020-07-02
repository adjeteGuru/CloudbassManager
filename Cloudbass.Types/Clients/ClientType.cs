using Cloudbass.DataAccess.Repositories;
using Cloudbass.DataAccess.Repositories.Contracts;
using Cloudbass.DataAccess.Resolvers;
using Cloudbass.Database.Models;
using Cloudbass.Types.Jobs;
using GreenDonut;
using HotChocolate;
using HotChocolate.Resolvers;
using HotChocolate.Types;
using System;
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
            descriptor.Field(x => x.Id).Type<IdType>();
            descriptor.Field(x => x.Name).Type<StringType>();
            descriptor.Field(x => x.Tel).Type<StringType>();
            descriptor.Field(x => x.Email).Type<StringType>();
            descriptor.Field(x => x.ToContact).Type<StringType>();
            descriptor.Field(x => x.Address).Type<StringType>();

            //invoke the resolver to allow data fetching          

            //descriptor.Field<JobResolver>(t => t.GetJobs(default, default));
            //descriptor.Field<JobResolver>(x=>x.GetJobForClient())

            //descriptor.Field("jobs").Type<JobType>().Resolver(ctx =>
            //{
            //    JobRepository jobRepository = ctx.Service<JobRepository>();
            //    IDataLoader dataLoader = ctx.BatchDataLoader<Guid, Job>(
            //        "JobByClientId",
            //        jobRepository.GetJobsByClientIdAsync);

            //    return dataLoader.LoadAsync(ctx.Parent<Client>().Id);

            //});





        }
    }

    //public class JobResolver
    //{
    //    private readonly IJobRepository _jobRepository;
    //    public JobResolver([Service] IJobRepository jobRepository)
    //    {
    //        _jobRepository = jobRepository;
    //    }

    //public IEnumerable<Job> GetJobs(Client client, IResolverContext ctx)
    //{
    //    return _jobRepository.GetAll().Where(x => x.ClientId == client.Id);
    //}

    //public IQueryable<Job> GetJob(Schedule schedule, IResolverContext ctx)
    //{
    //   /* yield*/ return _jobRepository.GetAll().Where(x => x.Id == schedule.JobId).FirstOrDefault();
    //}
    //}
}


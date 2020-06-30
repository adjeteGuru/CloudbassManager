﻿using Cloudbass.DataAccess.Repositories.Contracts;
using Cloudbass.Database.Models;
using HotChocolate;
using HotChocolate.Resolvers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cloudbass.DataAccess.Resolvers
{
    public class JobResolver
    {
        private readonly IJobRepository _jobRepository;

        public JobResolver([Service] IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public Job GetJobOnSched(Schedule schedule, IResolverContext ctx)
        {
            return _jobRepository.GetAllJobs().Where(x => x.Id == schedule.JobId).FirstOrDefault();
        }

        public Job GetJobOnCrew(Crew crew, IResolverContext ctx)
        {
            return _jobRepository.GetAllJobs().Where(x => x.Id == crew.JobId).FirstOrDefault();
        }

        public Job GetJobForClient(Guid clientId, int lastJob, IResolverContext ctx)
        {
            return _jobRepository.GetAllJobs()
                .Where(x => x.ClientId == clientId)
                .OrderByDescending(x => x.CreatedAt)
                .Take(lastJob)
                .FirstOrDefault();

        }

        public IEnumerable<Job> GetJobs(Client client, IResolverContext ctx)
        {
            return _jobRepository.GetAllJobs().Where(x => x.ClientId == client.Id);
        }


    }
}

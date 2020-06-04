using Cloudbass.DataAccess.Repositories.Contracts;
using Cloudbass.Database.Models;
using HotChocolate;
using HotChocolate.Resolvers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cloudbass.DataAccess.Resolvers
{
    public class ScheduleResolver
    {
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IJobRepository _jobRepository;
        public ScheduleResolver([Service] IScheduleRepository scheduleRepository, [Service] IJobRepository jobRepository)
        {
            _scheduleRepository = scheduleRepository;
            _jobRepository = jobRepository;
        }

        public IEnumerable<Schedule> GetSchedules(Job job, IResolverContext ctx)
        {
            return _scheduleRepository.GetAll().Where(x => x.JobId == job.Id);
        }

        public Job GetJob(Schedule schedule, IResolverContext ctx)
        {
            return _jobRepository.GetAll().Where(x => x.Id == schedule.JobId).FirstOrDefault();
        }
    }
}


﻿using Cloudbass.DataAccess.Repositories.Contracts;
using Cloudbass.DataAccess.Repositories.Contracts.Inputs;
using Cloudbass.Database;
using Cloudbass.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cloudbass.DataAccess.Repositories
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly CloudbassContext _db;
        public ScheduleRepository(CloudbassContext db)
        {
            _db = db;
        }
        public Schedule CreateSchedule(CreateScheduleInput input)
        {
            throw new NotImplementedException();
        }

        public Schedule DeleteSchedule(DeleteScheduleInput input)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Schedule> GetAll()
        {
            return _db.Schedules.AsQueryable();
        }

        public Schedule GetSchedule(int id)
        {
            return _db.Schedules.FirstOrDefault(x => x.Id == id);
        }

        //public Schedule GetSchedulesForJob(Guid jobId)
        //{
        //    return _db.Schedules.Where(x => x.JobId == jobId);
        //}

        public Schedule UpdateSchedule(UpdateJobInput input, int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Schedule> GetSchedulesForJob(Guid jobId)
        {
            return _db.Schedules.Where(x => x.JobId == jobId);
        }
    }
}

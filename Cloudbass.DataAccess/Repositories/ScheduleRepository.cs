using Cloudbass.DataAccess.Repositories.Contracts;
using Cloudbass.DataAccess.Repositories.Contracts.Inputs;
using Cloudbass.Database;
using Cloudbass.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cloudbass.DataAccess.Repositories
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly CloudbassContext _db;
        public ScheduleRepository(CloudbassContext db)
        {
            _db = db;
        }


        public async Task<Schedule> CreateScheduleAsync(Schedule schedule, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Schedule>> GetAllSchedulesAsync()
        {
            return await _db.Schedules.AsNoTracking().ToListAsync();
        }

        public async Task<Schedule> GetScheduleByIdAsync(Guid id)
        {
            return await _db.Schedules.FindAsync(id);
        }

        public async Task<IReadOnlyDictionary<Guid, Schedule>> GetSchedulesByIdAsync(
            IReadOnlyList<Guid> ids, CancellationToken cancellationToken)
        {
            var list = await _db.Schedules.AsQueryable()
                 .Where(x => ids.Contains(x.Id))
                 .ToListAsync(cancellationToken)
                 .ConfigureAwait(false);
            return list.ToDictionary(x => x.Id);
        }


        public async Task<ILookup<Guid, Schedule>> GetSchedulesByJobIdAsync(
            IReadOnlyList<Guid> jobIds, CancellationToken cancellationToken)
        {
            var schedule = await _db.Schedules
                 .Where(x => jobIds.Contains(x.JobId))
                 .ToListAsync(cancellationToken);
            return schedule.ToLookup(x => x.JobId);
        }



        public async Task<Schedule> UpdateScheduleAsync(Schedule schedule, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }



        //public Schedule CreateSchedule(CreateScheduleInput input)
        //{
        //    throw new NotImplementedException();
        //}

        //public Schedule DeleteSchedule(DeleteScheduleInput input)
        //{
        //    throw new NotImplementedException();
        //}


        //public Schedule GetSchedule(Guid id)
        //{
        //    return _db.Schedules.FirstOrDefault(x => x.Id == id);
        //}

        //public Schedule GetSchedulesForJob(Guid jobId)
        //{
        //    return _db.Schedules.Where(x => x.JobId == jobId);
        //}

        //public Schedule UpdateSchedule(UpdateJobInput input, Guid id)
        //{
        //    throw new NotImplementedException();
        //}

        //public IEnumerable<Schedule> GetSchedulesForJob(Guid jobId)
        //{
        //    return _db.Schedules.Where(x => x.JobId == jobId);
        //}
    }
}

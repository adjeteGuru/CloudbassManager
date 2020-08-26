﻿using Cloudbass.DataAccess.Repositories.Contracts;
using Cloudbass.Database;
using Cloudbass.Database.Models;
using HotChocolate;
using HotChocolate.Execution;
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

        public async Task<Schedule> DeleteScheduleAsync(Schedule schedule, CancellationToken cancellationToken)
        {
            var scheduleToDelete = await _db.Schedules.FindAsync(schedule.Id);

            if (scheduleToDelete == null)
            {
                throw new QueryException(
                   ErrorBuilder.New()
                       .SetMessage("Schedule not found in database.")
                       .SetCode("SCHEDULE_NOT_FOUND")
                       .Build());
            }

            _db.Schedules.Remove(scheduleToDelete);

            await _db.SaveChangesAsync();
            return scheduleToDelete;
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
            var scheduleToUpdate = await _db.Schedules.FindAsync(schedule.Id);
            _db.Schedules.Update(scheduleToUpdate);
            await _db.SaveChangesAsync();
            return scheduleToUpdate;
        }

    }
}

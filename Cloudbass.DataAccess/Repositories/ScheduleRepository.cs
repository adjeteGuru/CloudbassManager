using Cloudbass.DataAccess.Repositories.Contracts;
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
            //check dupication of the new entry
            var checkSchedule = await _db.Schedules
                .FirstOrDefaultAsync(x => x.Name == schedule.Name && x.StartDate == schedule.StartDate);

            if (checkSchedule != null)
            {
                // throw error if the new name is already taken
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("Name \"" + schedule.Name + "\" is already taken")
                        .SetCode("NAME_EXIST")
                        .Build());
            }


            var addedSchedule = await _db.Schedules.AddAsync(schedule).ConfigureAwait(false);

            await _db.SaveChangesAsync();

            return addedSchedule.Entity;
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


        //public async Task<ILookup<Guid, Schedule>> GetSchedulesByJobIdAsync(
        //    IReadOnlyList<Guid> jobIds, CancellationToken cancellationToken)
        //{
        //    var schedule = await _db.Schedules
        //         .Where(x => jobIds.Contains(x.JobId))
        //         .ToListAsync(cancellationToken);
        //    return schedule.ToLookup(x => x.JobId);
        //}

        public async Task<ILookup<string, Schedule>> GetSchedulesByJobIdAsync(
       IReadOnlyList<string> jobIds, CancellationToken cancellationToken)
        {
            var schedule = await _db.Schedules
                 .Where(x => jobIds.Contains(x.JobRef))
                 .ToListAsync(cancellationToken);
            return schedule.ToLookup(x => x.JobRef);
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

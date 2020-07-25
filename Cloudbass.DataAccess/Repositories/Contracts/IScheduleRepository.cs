using Cloudbass.DataAccess.Repositories.Contracts.Inputs;
using Cloudbass.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cloudbass.DataAccess.Repositories.Contracts
{
    public interface IScheduleRepository
    {
        //IEnumerable<Schedule> GetSchedulesForJob(Guid jobId);

        //public Schedule CreateSchedule(CreateScheduleInput input);
        //public Schedule DeleteSchedule(DeleteScheduleInput input);
        //public Schedule UpdateSchedule(UpdateJobInput input, Guid id);


        //
        //Task<IEnumerable<Schedule>> GetSchedulesAsync();

        //Task<ILookup<string, Schedule>> GetSchedulesByCounty(
        //IReadOnlyList<string> counties);

        Task<IEnumerable<Schedule>> GetAllSchedulesAsync();
        Task<Schedule> GetScheduleByIdAsync(Guid id);
        Task<IReadOnlyDictionary<Guid, Schedule>> GetSchedulesByIdAsync(
           IReadOnlyList<Guid> ids,
           CancellationToken cancellationToken);

        Task<ILookup<Guid, Schedule>> GetSchedulesByJobIdAsync(
       IReadOnlyList<Guid> jobIds, CancellationToken cancellationToken);

        Task<Schedule> CreateScheduleAsync(Schedule schedule, CancellationToken cancellationToken);
        Task<Schedule> UpdateScheduleAsync(Schedule schedule, CancellationToken cancellationToken);

    }
}

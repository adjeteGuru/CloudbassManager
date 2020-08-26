
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
        Task<IEnumerable<Schedule>> GetAllSchedulesAsync();
        Task<Schedule> GetScheduleByIdAsync(Guid id);
        Task<IReadOnlyDictionary<Guid, Schedule>> GetSchedulesByIdAsync(
           IReadOnlyList<Guid> ids,
           CancellationToken cancellationToken);

        Task<ILookup<Guid, Schedule>> GetSchedulesByJobIdAsync(
       IReadOnlyList<Guid> jobIds, CancellationToken cancellationToken);

        Task<Schedule> CreateScheduleAsync(Schedule schedule, CancellationToken cancellationToken);
        Task<Schedule> UpdateScheduleAsync(Schedule schedule, CancellationToken cancellationToken);

        Task<Schedule> DeleteScheduleAsync(Schedule schedule, CancellationToken cancellationToken);

    }
}

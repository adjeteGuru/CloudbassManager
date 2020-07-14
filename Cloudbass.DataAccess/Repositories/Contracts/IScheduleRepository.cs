using Cloudbass.DataAccess.Repositories.Contracts.Inputs;
using Cloudbass.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudbass.DataAccess.Repositories.Contracts
{
    public interface IScheduleRepository
    {
        IEnumerable<Schedule> GetSchedulesForJob(Guid jobId);
        Task<IEnumerable<Schedule>> GetAllSchedulesAsync();
        public Schedule CreateSchedule(CreateScheduleInput input);
        public Schedule DeleteSchedule(DeleteScheduleInput input);
        public Schedule UpdateSchedule(UpdateJobInput input, int id);


    }
}

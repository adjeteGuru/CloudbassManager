using Cloudbass.DataAccess.Repositories.Contracts.Inputs;
using Cloudbass.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cloudbass.DataAccess.Repositories.Contracts
{
    public interface IScheduleRepository
    {
        public Schedule GetSchedule(int id);
        IQueryable<Schedule> GetAll();
        public Schedule CreateSchedule(CreateScheduleInput input);
        public Schedule DeleteSchedule(DeleteScheduleInput input);
        public Schedule UpdateSchedule(UpdateJobInput input, int id);


    }
}

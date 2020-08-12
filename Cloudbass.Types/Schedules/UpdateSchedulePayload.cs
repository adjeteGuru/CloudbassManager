using Cloudbass.Database.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cloudbass.Types.Schedules
{
    public class UpdateSchedulePayload
    {
        public UpdateSchedulePayload(Schedule schedule)
        {
            Schedule = schedule;
        }

        public Schedule Schedule { get; set; }
    }
}

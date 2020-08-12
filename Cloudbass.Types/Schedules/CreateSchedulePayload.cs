using Cloudbass.Database.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cloudbass.Types.Schedules
{
    public class CreateSchedulePayload
    {
        public CreateSchedulePayload(Schedule schedule)
        {
            Schedule = schedule;
        }

        public Schedule Schedule { get; set; }
    }
}

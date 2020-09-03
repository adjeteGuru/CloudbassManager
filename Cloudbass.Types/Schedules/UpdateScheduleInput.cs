using Cloudbass.Database.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cloudbass.Types.Schedules
{
    public class UpdateScheduleInput
    {
        public UpdateScheduleInput(string name, string description, DateTime? startDate, DateTime? endDate, Guid jobId, Status status)
        {
            Name = name;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
            JobId = jobId;
            Status = status;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Guid JobId { get; set; }
        public Status Status { get; set; }


    }
}

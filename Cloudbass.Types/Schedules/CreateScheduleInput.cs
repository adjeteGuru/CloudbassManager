using Cloudbass.Database.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cloudbass.Types.Schedules
{
    public class CreateScheduleInput
    {
        public CreateScheduleInput(string name, string description, DateTime? startDate, DateTime? endDate, Guid jobId, Status status)
        {
            Name = name;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
            JobId = jobId;
            Status = status;
        }

        public string Name { get; }
        public string Description { get; }
        public DateTime? StartDate { get; }
        public DateTime? EndDate { get; }
        public Guid JobId { get; }
        public Status Status { get; }
    }
}

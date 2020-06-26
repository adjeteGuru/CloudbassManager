using Cloudbass.Database.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cloudbass.DataAccess.Repositories.Contracts.Inputs
{
    public class UpdateScheduleInput
    {
        public UpdateScheduleInput(Guid jobId, string name, string description, DateTime startDate, DateTime endDate, Status status)
        {
            Name = name;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
            Status = status;
            JobId = jobId;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid JobId { get; set; }
        public Status Status { get; set; }
    }
}

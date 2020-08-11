using Cloudbass.Database.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cloudbass.Types.Jobs
{
    public class CreateJobInput
    {
        public CreateJobInput(string name, string description, string location, DateTime? createdAt, DateTime? startDate, DateTime? tXDate,
            DateTime? endDate, bool paid, string coordinator, string commercialLead, Guid clientId, Status status, string createdBy)
        {
            Name = name;
            Description = description;
            Location = location;
            CreatedAt = DateTime.UtcNow;
            StartDate = startDate;
            EndDate = endDate;
            TXDate = tXDate;
            Paid = paid;
            Coordinator = coordinator;
            CommercialLead = commercialLead;
            ClientId = clientId;
            Status = status;
            CreatedBy = createdBy;
        }
        public string Name { get; }

        public string Description { get; }

        public string Location { get; }

        public DateTime? CreatedAt { get; }

        public DateTime? StartDate { get; }

        public DateTime? TXDate { get; }

        public DateTime? EndDate { get; }
        public bool Paid { get; }

        public string Coordinator { get; }

        public string CommercialLead { get; }

        public Guid ClientId { get; }
        public string CreatedBy { get; }
        public Status Status { get; }

    }
}

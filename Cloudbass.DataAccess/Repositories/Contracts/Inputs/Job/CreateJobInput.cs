using Cloudbass.Database.Models;
using System;

namespace Cloudbass.DataAccess.Repositories.Contracts
{
    public class CreateJobInput
    {
        public CreateJobInput(string name, string description, string location, DateTime? createdAt, DateTime? startDate, DateTime? tXDate,
            DateTime? endDate, bool paid, string coordinator, string commercialLead, int clientId, Status status)
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

        }
        public string Name { get; set; }

        public string Description { get; set; }

        public string Location { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? TXDate { get; set; }

        public DateTime? EndDate { get; set; }
        public bool Paid { get; set; }

        public string Coordinator { get; set; }

        public string CommercialLead { get; set; }

        public int ClientId { get; set; }

        public Status Status { get; set; }

    }
}
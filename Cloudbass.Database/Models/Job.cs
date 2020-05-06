using System;
using System.Collections.Generic;
using System.Text;

namespace Cloudbass.Database.Models
{
    public enum Status
    {
        InQuotation, Active, Cancelled, Completed, Pending
    }
    public class Job
    {
        public Guid Id { get; set; }

        public string Text { get; set; }

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

        public Client Client { get; set; }
        public Status Status { get; set; }

        public ICollection<Schedule> Schedules { get; set; }


    }
}

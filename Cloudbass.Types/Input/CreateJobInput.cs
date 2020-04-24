using Cloudbass.Database.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cloudbass.Types.Input
{
    public class CreateJobInput
    {
        //private string text;

        //private string description;

        //private string location;

        //private DateTime? createdAt;

        //private DateTime? startDate;

        //private DateTime? tXDate;

        //private DateTime? endDate;
        //private bool paid;

        //private string coordinator;

        //private string commercialLead;

        //private int clientId;

        //private Status status;

        public CreateJobInput(string text, string description, string location, DateTime? createdAt, DateTime? startDate, DateTime? tXDate,
            DateTime? endDate, bool paid, string coordinator, string commercialLead, int clientId, Status status)
        {
            Text = text;
            Description = description;
            Location = location;
            CreatedAt = createdAt;
            StartDate = startDate;
            EndDate = endDate;
            TXDate = tXDate;
            Paid = paid;
            Coordinator = coordinator;
            CommercialLead = commercialLead;
            ClientId = clientId;
            Status = status;


        }
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

        public Status Status { get; set; }

    }
}

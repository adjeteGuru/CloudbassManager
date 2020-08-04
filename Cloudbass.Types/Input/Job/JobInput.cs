using Cloudbass.Database.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cloudbass.Types.Input.Job
{
    public class JobInput
    {
        //public string Name { get; set; }

        //public string Description { get; set; }

        //public string Location { get; set; }

        //public DateTime? CreatedAt { get; set; }

        //public DateTime? StartDate { get; set; }

        //public DateTime? TXDate { get; set; }

        //public DateTime? EndDate { get; set; }
        ////public bool? Paid { get; set; } = false;

        //public string Coordinator { get; set; }

        //public string CommercialLead { get; set; }

        //public Guid ClientId { get; set; }

        //public Client Client { get; set; }
        //public Status Status { get; set; }

        //public string CreatedBy { get; set; }


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

        public Guid ClientId { get; set; }

        public Client Client { get; set; }
        public Status Status { get; set; }

        public string CreatedBy { get; set; }
    }
}

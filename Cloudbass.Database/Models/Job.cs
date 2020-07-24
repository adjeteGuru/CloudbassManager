﻿using HotChocolate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cloudbass.Database.Models
{
    public enum Status
    {
        InQuotation, Active, Cancelled, Completed
    }
    public class Job
    {
        public Guid Id { get; set; }

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

        [GraphQLIgnore]
        public List<Schedule> Schedules { get; set; } = new List<Schedule>();

        [GraphQLIgnore]
        public List<Crew> CrewMembers { get; set; } = new List<Crew>();


        //public List<Schedule> Schedules { get; set; }

        //[GraphQLIgnore]
        //public ICollection<Crew> Crews { get; set; }

        //[GraphQLIgnore]
        //public List<Crew> EngagedIn { get; } = new List<Crew>();



        //public IReadOnlyList<HasRole> InvolvedIn { get; set; }


    }
}

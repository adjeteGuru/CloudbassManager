using HotChocolate;
using System;
using System.Collections.Generic;

namespace Cloudbass.Database.Models
{
    public class HasRole
    {

        public int Id { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public Nullable<decimal> TotalDays { get; set; }
        public Nullable<decimal> Rate { get; set; }

        //public ICollection<Crew> CrewMembers { get; set; }
        [GraphQLIgnore]
        public List<Crew> CrewMembers { get; } = new List<Crew>();
    }
}
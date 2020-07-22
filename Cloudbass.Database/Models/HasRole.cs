using HotChocolate;
using System;
using System.Collections.Generic;

namespace Cloudbass.Database.Models
{
    public class HasRole
    {

        public Guid Id { get; set; }

        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public Guid RoleId { get; set; }
        public Role Role { get; set; }
        public Nullable<decimal> TotalDays { get; set; }
        public Nullable<decimal> Rate { get; set; }


        [GraphQLIgnore]
        public List<Crew> CrewMembers { get; } = new List<Crew>();

        //public ICollection<Crew> CrewMembers { get; set; }

        public IReadOnlyList<Job> InvolvedIn { get; set; }

        //public IReadOnlyList<Role> CanDo { get; set; }
    }
}
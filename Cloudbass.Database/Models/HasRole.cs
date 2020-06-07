using System;
using System.Collections.Generic;

namespace Cloudbass.Database.Models
{
    public class HasRole
    {
        //public int EmployeeId { get; set; }
        //public Employee Employee { get; set; }
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public Nullable<decimal> TotalDays { get; set; }
        public Nullable<decimal> Rate { get; set; }

        public ICollection<Crew> Crews { get; set; }
    }
}
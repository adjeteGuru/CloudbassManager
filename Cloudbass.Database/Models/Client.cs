using System;
using System.Collections.Generic;
using System.Text;

namespace Cloudbass.Database.Models
{
    public class Client
    {

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Tel { get; set; }

        public string Email { get; set; }


        public string ToContact { get; set; }


        public string Address { get; set; }
        public ICollection<Job> Jobs { get; set; }
    }
}

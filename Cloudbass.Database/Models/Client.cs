using HotChocolate;
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


        [GraphQLIgnore]
        public List<Job> Jobs { get; set; } = new List<Job>();
        //public ICollection<Job> Jobs { get; set; }
    }
}

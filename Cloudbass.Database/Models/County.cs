using HotChocolate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cloudbass.Database.Models
{
    public class County
    {
        //[BsonId]
        public Guid Id { get; set; }
        public string Name { get; set; }

        //[GraphQLIgnore]
        //public List<Employee> Employees { get; } = new List<Employee>();
        public ICollection<Employee> Employees { get; set; }


    }
}

using HotChocolate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cloudbass.Database.Models
{
    public class Role
    {
        public Guid Id { get; set; }
        public string Name { get; set; }


        [GraphQLIgnore]
        public List<HasRole> HasRoles { get; set; } = new List<HasRole>();

        //public ICollection<HasRole> HasRoles { get; set; }

    }
}

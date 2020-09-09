using HotChocolate;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cloudbass.Database.Models
{
    public class Employee
    {
        //[BsonId]
        public Guid Id { get; set; }


        [GraphQLNonNullType]
        public string Email { get; set; }
        public string FullName { get; set; }


        public string PostNominals { get; set; }

        public string NextOfKin { get; set; }
        public string Alergy { get; set; }

        public string Bared { get; set; }

        public Uri? Photo { get; set; }
        public Guid CountyId { get; set; }
        public County County { get; set; }



        //[GraphQLIgnore]
        //public List<Crew> CrewMembers { get; } = new List<Crew>();
        public ICollection<HasRole> HasRoles { get; set; }

        //[GraphQLIgnore]
        //public List<Crew> JobInvoledIn { get; } = new List<Crew>();
        public ICollection<User> Users { get; set; }

        // public IReadOnlyList<Role> CanDo { get; set; }


    }
}

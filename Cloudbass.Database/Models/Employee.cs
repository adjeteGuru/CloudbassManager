using HotChocolate;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cloudbass.Database.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }


        [GraphQLNonNullType]
        public string Email { get; set; }
        public string FullName { get; set; }

        public string PostNominals { get; set; }

        public string NextOfKin { get; set; }
        public string Alergy { get; set; }

        public string Bared { get; set; }

        public Uri? Photo { get; set; }
        public int CountyId { get; set; }
        public County County { get; set; }

        // public ICollection<HasRole> HasRoles { get; set; }
        // public ICollection<User> Users { get; set; }

        // public ICollection<Category> Categories { get; set; }


    }
}

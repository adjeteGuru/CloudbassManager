﻿using HotChocolate;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cloudbass.Database.Models
{
    public class Employee
    {
        [Key]
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

        //public string Countys { get; set; }

        [GraphQLIgnore]
        public List<HasRole> HasRoles { get; set; } = new List<HasRole>();
        //public ICollection<HasRole> HasRoles { get; set; }

        [GraphQLIgnore]
        public List<User> Users { get; set; } = new List<User>();
        //public ICollection<User> Users { get; set; }

        // public IReadOnlyList<Role> CanDo { get; set; }


    }
}

using HotChocolate;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cloudbass.Database.Models
{
    public class User : BaseEntity
    {
        //USE THIS TO RESOLVE DEFAULT VALUE ISUES
        public User()
        {
            Active = true; // Default Value true
            IsAdmin = false;
            TokenVersion = 0; // Default Value 0
        }

        public Guid Id { get; set; }

        [GraphQLIgnore]

        public Guid EmployeeId { get; set; }

        [GraphQLNonNullType]
        public Employee Employee { get; set; }

        [GraphQLNonNullType]
        public string Name { get; set; }


        [GraphQLIgnore]
        public string Password { get; set; }

        public string Email { get; set; }

        [GraphQLNonNullType]

        [DefaultValue(false)]
        public bool Active { get; set; }


        [GraphQLIgnore]
        [DefaultValue(0)]
        public int TokenVersion { get; set; }


        [GraphQLIgnore]
        public string Salt { get; set; }


        [DefaultValue(false)]
        public bool IsAdmin { get; set; }



    }
}

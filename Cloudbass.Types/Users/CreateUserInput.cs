﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Cloudbass.Types.Users
{
    public class CreateUserInput
    {
        public CreateUserInput(
            //string name,
            string permisionLevel,
            string email,
            string password,
            bool? active,
             string fullName,
             Uri? photo,
             string postNominals,
            string nextOfKin,
             string alergy,
             string bared,
             bool? isAdmin,
             Guid countyId

            )
        {
            //Name = name;
            PermisionLevel = permisionLevel;
            Email = email;
            Password = password;
            Active = active;
            FullName = fullName;
            Photo = photo;
            PostNominals = postNominals;
            NextOfKin = nextOfKin;
            Alergy = alergy;
            Bared = bared;
            IsAdmin = isAdmin;
            CountyId = countyId;
        }

        //public string Name { get; }
        public string PermisionLevel { get; }

        public string FullName { get; }

        //#nullable enable
        public Uri? Photo { get; }

        public string Email { get; }

        public string Password { get; }

        public bool? Active { get; }
        public bool? IsAdmin { get; set; }
        public string PostNominals { get; }
        public string NextOfKin { get; }
        public string Bared { get; }
        public string Alergy { get; }
        public Guid CountyId { get; }

    }
}

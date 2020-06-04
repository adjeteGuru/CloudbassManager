using System;
using System.Collections.Generic;
using System.Text;

namespace Cloudbass.Types.Input
{
    public class CreateUserInput
    {
        public CreateUserInput(
            string name,
            string email,
            string password,
            bool? active
            // string fullName,
            // Uri? photo,
            // string postNominals,
            //string nextOfKin,
            // string alergy,
            // string bared
            )
        {
            Name = name;
            Email = email;
            Password = password;
            Active = active;
            //FullName = fullName;
            //Photo = photo;
            //PostNominals = postNominals;
            //NextOfKin = nextOfKin;
            //Alergy = alergy;
            //Bared = bared;
        }

        public string Name { get; }

        //        public string FullName { get; }

        //#nullable enable
        //        public Uri? Photo { get; }

        public string Email { get; }

        public string Password { get; }

        public bool? Active { get; }
        //public string PostNominals { get; }
        //public string NextOfKin { get; }
        //public string Bared { get; }
        //public string Alergy { get; }

    }
}

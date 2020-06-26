using System;
using System.Collections.Generic;
using System.Text;

namespace Cloudbass.DataAccess.Repositories.Contracts.Inputs
{
    public class UpdateClientInput
    {
        public UpdateClientInput(string name, string email, string tel, string toContact, string address)
        {
            Name = name;
            Email = email;
            Tel = tel;
            ToContact = toContact;
            Address = address;
        }


        public string Name { get; set; }

        public string Tel { get; set; }

        public string Email { get; set; }

        public string ToContact { get; set; }

        public string Address { get; set; }
    }
}

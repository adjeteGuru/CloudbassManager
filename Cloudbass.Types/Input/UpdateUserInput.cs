using System;
using System.Collections.Generic;
using System.Text;

namespace Cloudbass.Types.Input
{
    public class UpdateUserInput
    {
        public UpdateUserInput(
            string? name,
            string? email,
            string? password,
            bool? active)
        {
            Name = name;
            Email = email;
            Password = password;
            Active = active;
        }

        public string? Name { get; }

        public string? Email { get; }

        public string? Password { get; }

        public bool? Active { get; }

    }
}

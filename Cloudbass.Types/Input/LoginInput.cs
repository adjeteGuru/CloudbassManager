using System;
using System.Collections.Generic;
using System.Text;

namespace Cloudbass.Types.Input
{
    public class LoginInput
    {
        public LoginInput(
            string name,
            string password)
        {
            Name = name;
            Password = password;
        }

        public string Name { get; }

        public string Password { get; }

    }
}

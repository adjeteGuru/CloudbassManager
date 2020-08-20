using System;
using System.Collections.Generic;
using System.Text;

namespace Cloudbass.Types.Users
{
    public class LoginInput
    {
        public LoginInput(
            string email,
            string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; }

        public string Password { get; }

    }
}

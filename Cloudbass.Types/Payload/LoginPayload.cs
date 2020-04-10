using Cloudbass.Database.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cloudbass.Types.Payload
{
    public class LoginPayload
    {
        public LoginPayload(
            User user,
            string accessToken)
        {
            User = user;
            AccessToken = accessToken;
        }

        public User User { get; }

        public string AccessToken { get; }

    }
}

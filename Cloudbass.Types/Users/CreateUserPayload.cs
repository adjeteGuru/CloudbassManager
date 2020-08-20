using Cloudbass.Database.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cloudbass.Types.Users
{
    public class CreateUserPayload
    {
        public CreateUserPayload(User user)
        {
            User = user;
        }

        public User User { get; }

    }
}

using Cloudbass.Database.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cloudbass.Types.Payload
{
    public class UpdateUserPayload
    {
        public UpdateUserPayload(User user)
        {
            User = user;
        }

        public User User { get; }

    }
}

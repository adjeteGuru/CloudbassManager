using Cloudbass.Database.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cloudbass.Types.Roles
{
    public class UpdateRolePayload
    {
        public UpdateRolePayload(Role role)
        {
            Role = role;
        }
        public Role Role { get; set; }
    }
}

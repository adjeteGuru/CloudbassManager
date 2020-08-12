using Cloudbass.Database.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cloudbass.Types.HasRoles
{
    public class UpdateHasRolePayload
    {
        public UpdateHasRolePayload(HasRole hasRole)
        {
            HasRole = hasRole;
        }

        public HasRole HasRole { get; set; }
    }
}

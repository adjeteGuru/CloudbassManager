using System;
using System.Collections.Generic;
using System.Text;

namespace Cloudbass.Types.Roles
{
    public class CreateRoleInput
    {
        public CreateRoleInput(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}

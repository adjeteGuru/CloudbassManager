using System;
using System.Collections.Generic;
using System.Text;

namespace Cloudbass.Types.HasRoles
{
    public class UpdateHasRoleInput
    {
        public UpdateHasRoleInput(Guid employeeId, Guid roleId, decimal totalDays, decimal rate)
        {
            EmployeeId = employeeId;
            RoleId = roleId;
            TotalDays = totalDays;
            Rate = rate;
        }

        public Guid EmployeeId { get; set; }
        public Guid RoleId { get; set; }
        public decimal TotalDays { get; set; }
        public decimal Rate { get; set; }
    }
}

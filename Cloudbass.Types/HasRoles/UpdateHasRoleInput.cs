using System;
using System.Collections.Generic;
using System.Text;

namespace Cloudbass.Types.HasRoles
{
    public class UpdateHasRoleInput
    {
        public UpdateHasRoleInput(Guid employeeId, Guid roleId, decimal rate)
        {
            EmployeeId = employeeId;
            RoleId = roleId;
            Rate = rate;
            //SetRate(rate);
        }

        public Guid EmployeeId { get; set; }
        public Guid RoleId { get; set; }
        public decimal? Rate { get; set; }

        //private decimal? rate;

        //public decimal? GetRate()
        //{
        //    return rate;
        //}

        //public void SetRate(decimal? value)
        //{
        //    rate = value;
        //}
    }
}

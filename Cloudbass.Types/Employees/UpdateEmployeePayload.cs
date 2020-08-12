using Cloudbass.Database.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cloudbass.Types.Employees
{
    public class UpdateEmployeePayload
    {
        public UpdateEmployeePayload(Employee employee)
        {
            Employee = employee;
        }

        public Employee Employee { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Cloudbass.Types.Crews
{
    public class DeleteCrewInput
    {
        public Guid EmployeeId { get; set; }
        public Guid JobId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Cloudbass.Types.Crews
{
    public class UpdateCrewInput
    {
        public UpdateCrewInput(Guid employeeId, Guid jobId)
        {
            EmployeeId = employeeId;
            JobId = jobId;
        }

        public Guid EmployeeId { get; }
        public Guid JobId { get; }
    }
}

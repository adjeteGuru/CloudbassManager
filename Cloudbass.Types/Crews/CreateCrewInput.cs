using System;
using System.Collections.Generic;
using System.Text;

namespace Cloudbass.Types.Crews
{
    public class CreateCrewInput
    {
        public CreateCrewInput(Guid hasRoleId, Guid jobId)
        {
            HasRoleId = hasRoleId;
            JobId = jobId;
        }

        public Guid HasRoleId { get; }
        public Guid JobId { get; }
    }
}

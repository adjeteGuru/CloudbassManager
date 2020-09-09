using System;
using System.Collections.Generic;
using System.Text;

namespace Cloudbass.Types.Crews
{
    public class UpdateCrewInput
    {
        public UpdateCrewInput(Guid hasRoleId, Guid jobId)
        {
            HasRoleId = hasRoleId;
            JobId = jobId;
        }

        public Guid HasRoleId { get; }
        public Guid JobId { get; }
    }
}

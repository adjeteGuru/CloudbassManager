using System;
using System.Collections.Generic;
using System.Text;

namespace Cloudbass.Types.Crews
{
    public class CreateCrewInput
    {
        public CreateCrewInput(Guid hasRoleId, Guid jobId, decimal? totalDays)
        {
            HasRoleId = hasRoleId;
            JobId = jobId;
            TotalDays = totalDays;
        }

        public Guid HasRoleId { get; }
        public Guid JobId { get; }
        public decimal? TotalDays { get; }
    }
}

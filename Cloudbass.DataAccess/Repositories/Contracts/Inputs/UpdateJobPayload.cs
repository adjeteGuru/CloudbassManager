using Cloudbass.Database.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cloudbass.DataAccess.Repositories.Contracts.Inputs
{
    public class UpdateJobPayload
    {
        public UpdateJobPayload(Job job)
        {
            Job = job;
        }

        public Job Job { get; set; }
    }
}

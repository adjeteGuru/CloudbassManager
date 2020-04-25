using Cloudbass.Database.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cloudbass.Types.Payload
{
    public class CreateJobPayload
    {
        public CreateJobPayload(Job job)
        {
            Job = job;
        }

        public Job Job
        {
            get;
        }
    }
}

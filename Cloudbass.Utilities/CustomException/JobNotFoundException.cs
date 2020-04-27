using System;
using System.Collections.Generic;
using System.Text;

namespace Cloudbass.Utilities.CustomException
{
    public class JobNotFoundException : Exception
    {
        public Guid JobId { get; set; }
    }
}

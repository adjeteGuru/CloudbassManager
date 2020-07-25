using System;
using System.Collections.Generic;
using System.Text;

namespace Cloudbass.Database.Models
{
    public class Schedule
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid JobId { get; set; }
        public Status Status { get; set; }
        public Job Job { get; set; }

    }
}

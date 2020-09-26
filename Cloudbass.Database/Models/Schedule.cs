using System;
using System.Collections.Generic;
using System.Text;

namespace Cloudbass.Database.Models
{
    public class Schedule
    {
        //[BsonId]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; } = null;
        public DateTime? EndDate { get; set; } = null;
        public Guid JobId { get; set; }
        public string JobRef { get; set; }
        public Status Status { get; set; }
        public Job Job { get; set; }

    }
}

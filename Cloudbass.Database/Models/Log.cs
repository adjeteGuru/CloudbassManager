using System;
using System.Collections.Generic;
using System.Text;

namespace Cloudbass.Database.Models
{
    public class Log
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string MutationName { get; set; }
        public int MutatedId { get; set; }
        public string Info { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
